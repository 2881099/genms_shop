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
	public class TagController : BaseController {
		public TagController(ILogger<TagController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromServices]IConfiguration cfg, [FromQuery] string key, [FromQuery] int[] Goods_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Tag.Select
				.Where(!string.IsNullOrEmpty(key), "a.name ilike {0}", string.Concat("%", key, "%"));
			if (Goods_id.Length > 0) select.WhereGoods_id(Goods_id);
			var items = await select.Count(out var count).Page(page, limit).ToListAsync();
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
			TagInfo item = await Tag.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] string Name, [FromForm] int[] mn_Goods) {
			TagInfo item = new TagInfo();
			item.Name = Name;
			item = await Tag.InsertAsync(item);
			//关联 Goods
			foreach (int mn_Goods_in in mn_Goods)
				item.FlagGoods(mn_Goods_in);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] string Name, [FromForm] int[] mn_Goods) {
			TagInfo item = await Tag.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Name = Name;
			int affrows = await Tag.UpdateAsync(item);
			//关联 Goods
			if (mn_Goods.Length == 0) {
				item.UnflagGoodsALL();
			} else {
				List<int> mn_Goods_list = mn_Goods.ToList();
				foreach (var Obj_goods in item.Obj_goodss) {
					int idx = mn_Goods_list.FindIndex(a => a == Obj_goods.Id);
					if (idx == -1) item.UnflagGoods(Obj_goods.Id);
					else mn_Goods_list.RemoveAt(idx);
				}
				mn_Goods_list.ForEach(a => item.FlagGoods(a));
			}
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			var dels = new List<object>();
			foreach (int id2 in id)
				dels.Add(await Tag.DeleteAsync(id2));
			if (dels.Count > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{dels.Count}").SetData("dels", dels);
			return APIReturn.失败;
		}
	}
}
