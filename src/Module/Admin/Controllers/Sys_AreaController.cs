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
	public class Sys_AreaController : BaseController {
		public Sys_AreaController(ILogger<Sys_AreaController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Sys_Area.Select
				.Where(!string.IsNullOrEmpty(key), "a.F_Id ilike {0} or a.F_CreatorUserId ilike {0} or a.F_DeleteUserId ilike {0} or a.F_Description ilike {0} or a.F_EnCode ilike {0} or a.F_FullName ilike {0} or a.F_LastModifyUserId ilike {0} or a.F_ParentId ilike {0} or a.F_SimpleSpelling ilike {0}", string.Concat("%", key, "%"));
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
		async public Task<ActionResult> Edit([FromQuery] string F_Id) {
			Sys_AreaInfo item = await Sys_Area.GetItemAsync(F_Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] string F_Id, [FromForm] DateTime? F_CreatorTime, [FromForm] string F_CreatorUserId, [FromForm] bool F_DeleteMark, [FromForm] DateTime? F_DeleteTime, [FromForm] string F_DeleteUserId, [FromForm] string F_Description, [FromForm] bool F_EnabledMark, [FromForm] string F_EnCode, [FromForm] string F_FullName, [FromForm] DateTime? F_LastModifyTime, [FromForm] string F_LastModifyUserId, [FromForm] int? F_Layers, [FromForm] string F_ParentId, [FromForm] string F_SimpleSpelling, [FromForm] int? F_SortCode) {
			Sys_AreaInfo item = new Sys_AreaInfo();
			item.F_Id = F_Id;
			item.F_CreatorTime = F_CreatorTime;
			item.F_CreatorUserId = F_CreatorUserId;
			item.F_DeleteMark = F_DeleteMark;
			item.F_DeleteTime = F_DeleteTime;
			item.F_DeleteUserId = F_DeleteUserId;
			item.F_Description = F_Description;
			item.F_EnabledMark = F_EnabledMark;
			item.F_EnCode = F_EnCode;
			item.F_FullName = F_FullName;
			item.F_LastModifyTime = F_LastModifyTime;
			item.F_LastModifyUserId = F_LastModifyUserId;
			item.F_Layers = F_Layers;
			item.F_ParentId = F_ParentId;
			item.F_SimpleSpelling = F_SimpleSpelling;
			item.F_SortCode = F_SortCode;
			item = await Sys_Area.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] string F_Id, [FromForm] DateTime? F_CreatorTime, [FromForm] string F_CreatorUserId, [FromForm] bool F_DeleteMark, [FromForm] DateTime? F_DeleteTime, [FromForm] string F_DeleteUserId, [FromForm] string F_Description, [FromForm] bool F_EnabledMark, [FromForm] string F_EnCode, [FromForm] string F_FullName, [FromForm] DateTime? F_LastModifyTime, [FromForm] string F_LastModifyUserId, [FromForm] int? F_Layers, [FromForm] string F_ParentId, [FromForm] string F_SimpleSpelling, [FromForm] int? F_SortCode) {
			Sys_AreaInfo item = await Sys_Area.GetItemAsync(F_Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.F_CreatorTime = F_CreatorTime;
			item.F_CreatorUserId = F_CreatorUserId;
			item.F_DeleteMark = F_DeleteMark;
			item.F_DeleteTime = F_DeleteTime;
			item.F_DeleteUserId = F_DeleteUserId;
			item.F_Description = F_Description;
			item.F_EnabledMark = F_EnabledMark;
			item.F_EnCode = F_EnCode;
			item.F_FullName = F_FullName;
			item.F_LastModifyTime = F_LastModifyTime;
			item.F_LastModifyUserId = F_LastModifyUserId;
			item.F_Layers = F_Layers;
			item.F_ParentId = F_ParentId;
			item.F_SimpleSpelling = F_SimpleSpelling;
			item.F_SortCode = F_SortCode;
			int affrows = await Sys_Area.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] string[] id) {
			var dels = new List<object>();
			foreach (string id2 in id)
				dels.Add(await Sys_Area.DeleteAsync(id2));
			if (dels.Count > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{dels.Count}").SetData("dels", dels);
			return APIReturn.失败;
		}
	}
}
