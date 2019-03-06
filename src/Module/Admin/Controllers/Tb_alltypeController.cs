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
	public class Tb_alltypeController : BaseController {
		public Tb_alltypeController(ILogger<Tb_alltypeController> logger) : base(logger) { }

		[HttpGet]
		async public Task<ActionResult> List([FromQuery] string key, [FromQuery] int limit = 20, [FromQuery] int page = 1) {
			var select = Tb_alltype.Select
				.Where(!string.IsNullOrEmpty(key), "a.testFieldString ilike {0}", string.Concat("%", key, "%"));
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
			Tb_alltypeInfo item = await Tb_alltype.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			ViewBag.item = item;
			return View();
		}

		/***************************************** POST *****************************************/
		[HttpPost(@"add")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Add([FromForm] bool TestFieldBool1111, [FromForm] bool TestFieldBoolNullable, [FromForm] byte? TestFieldByte, [FromForm] byte? TestFieldByteNullable, [FromForm] byte[] TestFieldBytes, [FromForm] DateTime? TestFieldDateTime, [FromForm] DateTime? TestFieldDateTimeNullable, [FromForm] DateTimeOffset? TestFieldDateTimeNullableOffset, [FromForm] DateTimeOffset? TestFieldDateTimeOffset, [FromForm] decimal? TestFieldDecimal, [FromForm] decimal? TestFieldDecimalNullable, [FromForm] double? TestFieldDouble, [FromForm] double? TestFieldDoubleNullable, [FromForm] int? TestFieldEnum1, [FromForm] int? TestFieldEnum1Nullable, [FromForm] long? TestFieldEnum2, [FromForm] long? TestFieldEnum2Nullable, [FromForm] float? TestFieldFloat, [FromForm] float? TestFieldFloatNullable, [FromForm] Guid? TestFieldGuid, [FromForm] Guid? TestFieldGuidNullable, [FromForm] int? TestFieldInt, [FromForm] int? TestFieldIntNullable, [FromForm] long? TestFieldLong, [FromForm] byte? TestFieldSByte, [FromForm] byte? TestFieldSByteNullable, [FromForm] short? TestFieldShort, [FromForm] short? TestFieldShortNullable, [FromForm] string TestFieldString, [FromForm] TimeSpan? TestFieldTimeSpan, [FromForm] TimeSpan? TestFieldTimeSpanNullable, [FromForm] int? TestFieldUInt, [FromForm] int? TestFieldUIntNullable, [FromForm] long? TestFieldULong, [FromForm] long? TestFieldULongNullable, [FromForm] short? TestFieldUShort, [FromForm] short? TestFieldUShortNullable, [FromForm] long? TestFielLongNullable) {
			Tb_alltypeInfo item = new Tb_alltypeInfo();
			item.TestFieldBool1111 = TestFieldBool1111;
			item.TestFieldBoolNullable = TestFieldBoolNullable;
			item.TestFieldByte = TestFieldByte;
			item.TestFieldByteNullable = TestFieldByteNullable;
			item.TestFieldBytes = TestFieldBytes;
			item.TestFieldDateTime = TestFieldDateTime;
			item.TestFieldDateTimeNullable = TestFieldDateTimeNullable;
			item.TestFieldDateTimeNullableOffset = TestFieldDateTimeNullableOffset;
			item.TestFieldDateTimeOffset = TestFieldDateTimeOffset;
			item.TestFieldDecimal = TestFieldDecimal;
			item.TestFieldDecimalNullable = TestFieldDecimalNullable;
			item.TestFieldDouble = TestFieldDouble;
			item.TestFieldDoubleNullable = TestFieldDoubleNullable;
			item.TestFieldEnum1 = TestFieldEnum1;
			item.TestFieldEnum1Nullable = TestFieldEnum1Nullable;
			item.TestFieldEnum2 = TestFieldEnum2;
			item.TestFieldEnum2Nullable = TestFieldEnum2Nullable;
			item.TestFieldFloat = TestFieldFloat;
			item.TestFieldFloatNullable = TestFieldFloatNullable;
			item.TestFieldGuid = TestFieldGuid;
			item.TestFieldGuidNullable = TestFieldGuidNullable;
			item.TestFieldInt = TestFieldInt;
			item.TestFieldIntNullable = TestFieldIntNullable;
			item.TestFieldLong = TestFieldLong;
			item.TestFieldSByte = TestFieldSByte;
			item.TestFieldSByteNullable = TestFieldSByteNullable;
			item.TestFieldShort = TestFieldShort;
			item.TestFieldShortNullable = TestFieldShortNullable;
			item.TestFieldString = TestFieldString;
			item.TestFieldTimeSpan = TestFieldTimeSpan;
			item.TestFieldTimeSpanNullable = TestFieldTimeSpanNullable;
			item.TestFieldUInt = TestFieldUInt;
			item.TestFieldUIntNullable = TestFieldUIntNullable;
			item.TestFieldULong = TestFieldULong;
			item.TestFieldULongNullable = TestFieldULongNullable;
			item.TestFieldUShort = TestFieldUShort;
			item.TestFieldUShortNullable = TestFieldUShortNullable;
			item.TestFielLongNullable = TestFielLongNullable;
			item = await Tb_alltype.InsertAsync(item);
			return APIReturn.成功.SetData("item", item.ToBson());
		}
		[HttpPost(@"edit")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Edit([FromQuery] int Id, [FromForm] bool TestFieldBool1111, [FromForm] bool TestFieldBoolNullable, [FromForm] byte? TestFieldByte, [FromForm] byte? TestFieldByteNullable, [FromForm] byte[] TestFieldBytes, [FromForm] DateTime? TestFieldDateTime, [FromForm] DateTime? TestFieldDateTimeNullable, [FromForm] DateTimeOffset? TestFieldDateTimeNullableOffset, [FromForm] DateTimeOffset? TestFieldDateTimeOffset, [FromForm] decimal? TestFieldDecimal, [FromForm] decimal? TestFieldDecimalNullable, [FromForm] double? TestFieldDouble, [FromForm] double? TestFieldDoubleNullable, [FromForm] int? TestFieldEnum1, [FromForm] int? TestFieldEnum1Nullable, [FromForm] long? TestFieldEnum2, [FromForm] long? TestFieldEnum2Nullable, [FromForm] float? TestFieldFloat, [FromForm] float? TestFieldFloatNullable, [FromForm] Guid? TestFieldGuid, [FromForm] Guid? TestFieldGuidNullable, [FromForm] int? TestFieldInt, [FromForm] int? TestFieldIntNullable, [FromForm] long? TestFieldLong, [FromForm] byte? TestFieldSByte, [FromForm] byte? TestFieldSByteNullable, [FromForm] short? TestFieldShort, [FromForm] short? TestFieldShortNullable, [FromForm] string TestFieldString, [FromForm] TimeSpan? TestFieldTimeSpan, [FromForm] TimeSpan? TestFieldTimeSpanNullable, [FromForm] int? TestFieldUInt, [FromForm] int? TestFieldUIntNullable, [FromForm] long? TestFieldULong, [FromForm] long? TestFieldULongNullable, [FromForm] short? TestFieldUShort, [FromForm] short? TestFieldUShortNullable, [FromForm] long? TestFielLongNullable) {
			Tb_alltypeInfo item = await Tb_alltype.GetItemAsync(Id);
			if (item == null) return APIReturn.记录不存在_或者没有权限;
			item.TestFieldBool1111 = TestFieldBool1111;
			item.TestFieldBoolNullable = TestFieldBoolNullable;
			item.TestFieldByte = TestFieldByte;
			item.TestFieldByteNullable = TestFieldByteNullable;
			item.TestFieldBytes = TestFieldBytes;
			item.TestFieldDateTime = TestFieldDateTime;
			item.TestFieldDateTimeNullable = TestFieldDateTimeNullable;
			item.TestFieldDateTimeNullableOffset = TestFieldDateTimeNullableOffset;
			item.TestFieldDateTimeOffset = TestFieldDateTimeOffset;
			item.TestFieldDecimal = TestFieldDecimal;
			item.TestFieldDecimalNullable = TestFieldDecimalNullable;
			item.TestFieldDouble = TestFieldDouble;
			item.TestFieldDoubleNullable = TestFieldDoubleNullable;
			item.TestFieldEnum1 = TestFieldEnum1;
			item.TestFieldEnum1Nullable = TestFieldEnum1Nullable;
			item.TestFieldEnum2 = TestFieldEnum2;
			item.TestFieldEnum2Nullable = TestFieldEnum2Nullable;
			item.TestFieldFloat = TestFieldFloat;
			item.TestFieldFloatNullable = TestFieldFloatNullable;
			item.TestFieldGuid = TestFieldGuid;
			item.TestFieldGuidNullable = TestFieldGuidNullable;
			item.TestFieldInt = TestFieldInt;
			item.TestFieldIntNullable = TestFieldIntNullable;
			item.TestFieldLong = TestFieldLong;
			item.TestFieldSByte = TestFieldSByte;
			item.TestFieldSByteNullable = TestFieldSByteNullable;
			item.TestFieldShort = TestFieldShort;
			item.TestFieldShortNullable = TestFieldShortNullable;
			item.TestFieldString = TestFieldString;
			item.TestFieldTimeSpan = TestFieldTimeSpan;
			item.TestFieldTimeSpanNullable = TestFieldTimeSpanNullable;
			item.TestFieldUInt = TestFieldUInt;
			item.TestFieldUIntNullable = TestFieldUIntNullable;
			item.TestFieldULong = TestFieldULong;
			item.TestFieldULongNullable = TestFieldULongNullable;
			item.TestFieldUShort = TestFieldUShort;
			item.TestFieldUShortNullable = TestFieldUShortNullable;
			item.TestFielLongNullable = TestFielLongNullable;
			int affrows = await Tb_alltype.UpdateAsync(item);
			if (affrows > 0) return APIReturn.成功.SetMessage($"更新成功，影响行数：{affrows}");
			return APIReturn.失败;
		}

		[HttpPost("del")]
		[ValidateAntiForgeryToken]
		async public Task<APIReturn> _Del([FromForm] int[] id) {
			var dels = new List<object>();
			foreach (int id2 in id)
				dels.Add(await Tb_alltype.DeleteAsync(id2));
			if (dels.Count > 0) return APIReturn.成功.SetMessage($"删除成功，影响行数：{dels.Count}").SetData("dels", dels);
			return APIReturn.失败;
		}
	}
}
