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
	public class TestController : BaseController {
		public TestController(ILogger<TestController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Test.Select;
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
			TestInfo item = await Test.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] int? Id, [FromForm] bool F_bit, [FromForm] int? F_ShortCode, [FromForm] byte? F_tinyint) {
			TestInfo item = new TestInfo();
			item.Id = Id;
			item.F_bit = F_bit;
			item.F_ShortCode = F_ShortCode;
			item.F_tinyint = F_tinyint;
			item = await Test.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] bool F_bit, [FromForm] int? F_ShortCode, [FromForm] byte? F_tinyint) {
			TestInfo item = await Test.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.F_bit = F_bit;
			item.F_ShortCode = F_ShortCode;
			item.F_tinyint = F_tinyint;
			int affrows = await Test.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			var dels = new List<object>();
			foreach (int id2 in id)
				dels.Add(await Test.DeleteAsync(id2));
			if (dels.Count > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{dels.Count}").SetData("dels", dels);
			return APIReturn.失败;
		}
	}
}
