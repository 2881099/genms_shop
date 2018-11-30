using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using es.BLL;
using es.Model;

namespace es.Module.Admin.Controllers {
	[Route("[controller]")]
	public class CommentController : BaseController {
		public CommentController(ILogger<CommentController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] int?[] Goods_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Comment.Select
				.Where(!string.IsNullOrEmpty(key), "a.content ilike {0} or a.nickname ilike {0}", string.Concat("%", key, "%"));
			if (Goods_id.Length > 0) select.WhereGoods_id(Goods_id);
			var items = await select.Count(out var count)
				.LeftJoin(a => a.Obj_goods.Id == a.Goods_id).Page(page, limit).ToListAsync();
			ViewBag.items = items;
			ViewBag.count = count;
			return View();
		}

		[HttpGet(@"add")]
		public ActionResult Edit() {
			return View();
		}
		[HttpGet(@"edit")]
		async public Task<ActionResult> Edit([FromQuery] int Id) {
			CommentInfo item = await Comment.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Goods_id, [FromForm] string Content, [FromForm] string Nickname) {
			CommentInfo item = new CommentInfo();
			item.Goods_id = Goods_id;
			item.Content = Content;
			item.Create_time = DateTime.Now;
			item.Nickname = Nickname;
			item.Update_time = DateTime.Now;
			item = await Comment.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] int? Goods_id, [FromForm] string Content, [FromForm] string Nickname) {
			CommentInfo item = await Comment.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Goods_id = Goods_id;
			item.Content = Content;
			item.Create_time = DateTime.Now;
			item.Nickname = Nickname;
			item.Update_time = DateTime.Now;
			int affrows = await Comment.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			var dels = new List<object>();
			foreach (int id2 in id)
				dels.Add(await Comment.DeleteAsync(id2));
			if (dels.Count > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{dels.Count}").SetData("dels", dels);
			return APIReturn.失败;
		}
	}
}
