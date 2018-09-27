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
	public class GoodsController : BaseController {
		public GoodsController(ILogger<GoodsController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromServices]IConfiguration cfg, [FromQuery] string key, [FromQuery] int?[] Category_id, [FromQuery] int[] Tag_id, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Goods.Select
				.Where(!string.IsNullOrEmpty(key), "a.content ilike {0} or a.imgs ilike {0} or a.title ilike {0}", string.Concat("%", key, "%"));
			if (Category_id.Length > 0) select.WhereCategory_id(Category_id);
			if (Tag_id.Length > 0) select.WhereTag_id(Tag_id);
			var items = await select.Count(out var count)
				.LeftJoin<Category>("b", "b.id = a.category_id").Page(page, limit).ToListAsync();
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
			GoodsInfo item = await Goods.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Category_id, [FromForm] string Content, [FromForm] string Imgs, [FromForm] int? Stock, [FromForm] string Title, [FromForm] int[] mn_Tag) {
			GoodsInfo item = new GoodsInfo();
			item.Category_id = Category_id;
			item.Content = Content;
			item.Create_time = DateTime.Now;
			item.Imgs = Imgs;
			item.Stock = Stock;
			item.Title = Title;
			item.Update_time = DateTime.Now;
			item = await Goods.InsertAsync(item);
			//关联 Tag
			foreach (int mn_Tag_in in mn_Tag)
				item.FlagTag(mn_Tag_in);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] int? Category_id, [FromForm] string Content, [FromForm] string Imgs, [FromForm] int? Stock, [FromForm] string Title, [FromForm] int[] mn_Tag) {
			GoodsInfo item = await Goods.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.Category_id = Category_id;
			item.Content = Content;
			item.Create_time = DateTime.Now;
			item.Imgs = Imgs;
			item.Stock = Stock;
			item.Title = Title;
			item.Update_time = DateTime.Now;
			int affrows = await Goods.UpdateAsync(item);
			//关联 Tag
			if (mn_Tag.Length == 0) {
				item.UnflagTagALL();
			} else {
				List<int> mn_Tag_list = mn_Tag.ToList();
				foreach (var Obj_tag in item.Obj_tags) {
					int idx = mn_Tag_list.FindIndex(a => a == Obj_tag.Id);
					if (idx == -1) item.UnflagTag(Obj_tag.Id);
					else mn_Tag_list.RemoveAt(idx);
				}
				mn_Tag_list.ForEach(a => item.FlagTag(a));
			}
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			var dels = new List<object>();
			foreach (int id2 in id)
				dels.Add(await Goods.DeleteAsync(id2));
			if (dels.Count > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{dels.Count}").SetData("dels", dels);
			return APIReturn.失败;
		}
	}
}
