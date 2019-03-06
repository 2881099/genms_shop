using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using es.Model;

namespace es.BLL {

	public partial class Tb_alltype {

		internal static readonly es.DAL.Tb_alltype dal = new es.DAL.Tb_alltype();
		internal static readonly int itemCacheTimeout;

		static Tb_alltype() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Tb_alltype"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static Tb_alltypeInfo Delete(int Id) {
			var item = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			TestFieldBool1111, 
			TestFieldBoolNullable, 
			TestFieldByte, 
			TestFieldByteNullable, 
			TestFieldBytes, 
			TestFieldDateTime, 
			TestFieldDateTimeNullable, 
			TestFieldDateTimeNullableOffset, 
			TestFieldDateTimeOffset, 
			TestFieldDecimal, 
			TestFieldDecimalNullable, 
			TestFieldDouble, 
			TestFieldDoubleNullable, 
			TestFieldEnum1, 
			TestFieldEnum1Nullable, 
			TestFieldEnum2, 
			TestFieldEnum2Nullable, 
			TestFieldFloat, 
			TestFieldFloatNullable, 
			TestFieldGuid, 
			TestFieldGuidNullable, 
			TestFieldInt, 
			TestFieldIntNullable, 
			TestFieldLong, 
			TestFieldSByte, 
			TestFieldSByteNullable, 
			TestFieldShort, 
			TestFieldShortNullable, 
			TestFieldString, 
			TestFieldTimeSpan, 
			TestFieldTimeSpanNullable, 
			TestFieldUInt, 
			TestFieldUIntNullable, 
			TestFieldULong, 
			TestFieldULongNullable, 
			TestFieldUShort, 
			TestFieldUShortNullable, 
			TestFielLongNullable
		}
		#endregion

		public static int Update(Tb_alltypeInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Tb_alltypeInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static es.DAL.Tb_alltype.SqlUpdateBuild UpdateDiy(int Id) => new es.DAL.Tb_alltype.SqlUpdateBuild(new List<Tb_alltypeInfo> { new Tb_alltypeInfo { Id = Id } }, false);
		public static es.DAL.Tb_alltype.SqlUpdateBuild UpdateDiy(List<Tb_alltypeInfo> dataSource) => new es.DAL.Tb_alltype.SqlUpdateBuild(dataSource, true);
		public static es.DAL.Tb_alltype.SqlUpdateBuild UpdateDiyDangerous => new es.DAL.Tb_alltype.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Tb_alltype.Insert(Tb_alltypeInfo item)
		/// </summary>
		[Obsolete]
		public static Tb_alltypeInfo Insert(bool? TestFieldBool1111, bool? TestFieldBoolNullable, byte? TestFieldByte, byte? TestFieldByteNullable, byte[] TestFieldBytes, DateTime? TestFieldDateTime, DateTime? TestFieldDateTimeNullable, DateTimeOffset? TestFieldDateTimeNullableOffset, DateTimeOffset? TestFieldDateTimeOffset, decimal? TestFieldDecimal, decimal? TestFieldDecimalNullable, double? TestFieldDouble, double? TestFieldDoubleNullable, int? TestFieldEnum1, int? TestFieldEnum1Nullable, long? TestFieldEnum2, long? TestFieldEnum2Nullable, float? TestFieldFloat, float? TestFieldFloatNullable, Guid? TestFieldGuid, Guid? TestFieldGuidNullable, int? TestFieldInt, int? TestFieldIntNullable, long? TestFieldLong, byte? TestFieldSByte, byte? TestFieldSByteNullable, short? TestFieldShort, short? TestFieldShortNullable, string TestFieldString, TimeSpan? TestFieldTimeSpan, TimeSpan? TestFieldTimeSpanNullable, int? TestFieldUInt, int? TestFieldUIntNullable, long? TestFieldULong, long? TestFieldULongNullable, short? TestFieldUShort, short? TestFieldUShortNullable, long? TestFielLongNullable) {
			return Insert(new Tb_alltypeInfo {
				TestFieldBool1111 = TestFieldBool1111, 
				TestFieldBoolNullable = TestFieldBoolNullable, 
				TestFieldByte = TestFieldByte, 
				TestFieldByteNullable = TestFieldByteNullable, 
				TestFieldBytes = TestFieldBytes, 
				TestFieldDateTime = TestFieldDateTime, 
				TestFieldDateTimeNullable = TestFieldDateTimeNullable, 
				TestFieldDateTimeNullableOffset = TestFieldDateTimeNullableOffset, 
				TestFieldDateTimeOffset = TestFieldDateTimeOffset, 
				TestFieldDecimal = TestFieldDecimal, 
				TestFieldDecimalNullable = TestFieldDecimalNullable, 
				TestFieldDouble = TestFieldDouble, 
				TestFieldDoubleNullable = TestFieldDoubleNullable, 
				TestFieldEnum1 = TestFieldEnum1, 
				TestFieldEnum1Nullable = TestFieldEnum1Nullable, 
				TestFieldEnum2 = TestFieldEnum2, 
				TestFieldEnum2Nullable = TestFieldEnum2Nullable, 
				TestFieldFloat = TestFieldFloat, 
				TestFieldFloatNullable = TestFieldFloatNullable, 
				TestFieldGuid = TestFieldGuid, 
				TestFieldGuidNullable = TestFieldGuidNullable, 
				TestFieldInt = TestFieldInt, 
				TestFieldIntNullable = TestFieldIntNullable, 
				TestFieldLong = TestFieldLong, 
				TestFieldSByte = TestFieldSByte, 
				TestFieldSByteNullable = TestFieldSByteNullable, 
				TestFieldShort = TestFieldShort, 
				TestFieldShortNullable = TestFieldShortNullable, 
				TestFieldString = TestFieldString, 
				TestFieldTimeSpan = TestFieldTimeSpan, 
				TestFieldTimeSpanNullable = TestFieldTimeSpanNullable, 
				TestFieldUInt = TestFieldUInt, 
				TestFieldUIntNullable = TestFieldUIntNullable, 
				TestFieldULong = TestFieldULong, 
				TestFieldULongNullable = TestFieldULongNullable, 
				TestFieldUShort = TestFieldUShort, 
				TestFieldUShortNullable = TestFieldUShortNullable, 
				TestFielLongNullable = TestFielLongNullable});
		}
		public static Tb_alltypeInfo Insert(Tb_alltypeInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<Tb_alltypeInfo> Insert(IEnumerable<Tb_alltypeInfo> items) {
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(Tb_alltypeInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Tb_alltypeInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL:Tb_alltype:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Tb_alltypeInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("es_BLL:Tb_alltype:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne());

		public static List<Tb_alltypeInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<Tb_alltypeInfo> DeleteAsync(int Id) {
			var item = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<Tb_alltypeInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL:Tb_alltype:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync());
		public static Task<int> UpdateAsync(Tb_alltypeInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Tb_alltypeInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Tb_alltype.Insert(Tb_alltypeInfo item)
		/// </summary>
		[Obsolete]
		public static Task<Tb_alltypeInfo> InsertAsync(bool? TestFieldBool1111, bool? TestFieldBoolNullable, byte? TestFieldByte, byte? TestFieldByteNullable, byte[] TestFieldBytes, DateTime? TestFieldDateTime, DateTime? TestFieldDateTimeNullable, DateTimeOffset? TestFieldDateTimeNullableOffset, DateTimeOffset? TestFieldDateTimeOffset, decimal? TestFieldDecimal, decimal? TestFieldDecimalNullable, double? TestFieldDouble, double? TestFieldDoubleNullable, int? TestFieldEnum1, int? TestFieldEnum1Nullable, long? TestFieldEnum2, long? TestFieldEnum2Nullable, float? TestFieldFloat, float? TestFieldFloatNullable, Guid? TestFieldGuid, Guid? TestFieldGuidNullable, int? TestFieldInt, int? TestFieldIntNullable, long? TestFieldLong, byte? TestFieldSByte, byte? TestFieldSByteNullable, short? TestFieldShort, short? TestFieldShortNullable, string TestFieldString, TimeSpan? TestFieldTimeSpan, TimeSpan? TestFieldTimeSpanNullable, int? TestFieldUInt, int? TestFieldUIntNullable, long? TestFieldULong, long? TestFieldULongNullable, short? TestFieldUShort, short? TestFieldUShortNullable, long? TestFielLongNullable) {
			return InsertAsync(new Tb_alltypeInfo {
				TestFieldBool1111 = TestFieldBool1111, 
				TestFieldBoolNullable = TestFieldBoolNullable, 
				TestFieldByte = TestFieldByte, 
				TestFieldByteNullable = TestFieldByteNullable, 
				TestFieldBytes = TestFieldBytes, 
				TestFieldDateTime = TestFieldDateTime, 
				TestFieldDateTimeNullable = TestFieldDateTimeNullable, 
				TestFieldDateTimeNullableOffset = TestFieldDateTimeNullableOffset, 
				TestFieldDateTimeOffset = TestFieldDateTimeOffset, 
				TestFieldDecimal = TestFieldDecimal, 
				TestFieldDecimalNullable = TestFieldDecimalNullable, 
				TestFieldDouble = TestFieldDouble, 
				TestFieldDoubleNullable = TestFieldDoubleNullable, 
				TestFieldEnum1 = TestFieldEnum1, 
				TestFieldEnum1Nullable = TestFieldEnum1Nullable, 
				TestFieldEnum2 = TestFieldEnum2, 
				TestFieldEnum2Nullable = TestFieldEnum2Nullable, 
				TestFieldFloat = TestFieldFloat, 
				TestFieldFloatNullable = TestFieldFloatNullable, 
				TestFieldGuid = TestFieldGuid, 
				TestFieldGuidNullable = TestFieldGuidNullable, 
				TestFieldInt = TestFieldInt, 
				TestFieldIntNullable = TestFieldIntNullable, 
				TestFieldLong = TestFieldLong, 
				TestFieldSByte = TestFieldSByte, 
				TestFieldSByteNullable = TestFieldSByteNullable, 
				TestFieldShort = TestFieldShort, 
				TestFieldShortNullable = TestFieldShortNullable, 
				TestFieldString = TestFieldString, 
				TestFieldTimeSpan = TestFieldTimeSpan, 
				TestFieldTimeSpanNullable = TestFieldTimeSpanNullable, 
				TestFieldUInt = TestFieldUInt, 
				TestFieldUIntNullable = TestFieldUIntNullable, 
				TestFieldULong = TestFieldULong, 
				TestFieldULongNullable = TestFieldULongNullable, 
				TestFieldUShort = TestFieldUShort, 
				TestFieldUShortNullable = TestFieldUShortNullable, 
				TestFielLongNullable = TestFielLongNullable});
		}
		async public static Task<Tb_alltypeInfo> InsertAsync(Tb_alltypeInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<Tb_alltypeInfo>> InsertAsync(IEnumerable<Tb_alltypeInfo> items) {
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		internal static Task RemoveCacheAsync(Tb_alltypeInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Tb_alltypeInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL:Tb_alltype:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Tb_alltypeInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Tb_alltypeInfo, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or(@"a.[Id] = {0}", Id);
			public SelectBuild WhereIdRange(int? begin) => base.Where(@"a.[Id] >= {0}", begin);
			public SelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[Id] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldBool1111(params bool?[] TestFieldBool1111) => this.Where1Or(@"a.[testFieldBool1111] = {0}", TestFieldBool1111);
			public SelectBuild WhereTestFieldBoolNullable(params bool?[] TestFieldBoolNullable) => this.Where1Or(@"a.[testFieldBoolNullable] = {0}", TestFieldBoolNullable);
			public SelectBuild WhereTestFieldByte(params byte?[] TestFieldByte) => this.Where1Or(@"a.[testFieldByte] = {0}", TestFieldByte);
			public SelectBuild WhereTestFieldByteRange(byte? begin) => base.Where(@"a.[testFieldByte] >= {0}", begin);
			public SelectBuild WhereTestFieldByteRange(byte? begin, byte? end) => end == null ? this.WhereTestFieldByteRange(begin) : base.Where(@"a.[testFieldByte] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldByteNullable(params byte?[] TestFieldByteNullable) => this.Where1Or(@"a.[testFieldByteNullable] = {0}", TestFieldByteNullable);
			public SelectBuild WhereTestFieldByteNullableRange(byte? begin) => base.Where(@"a.[testFieldByteNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldByteNullableRange(byte? begin, byte? end) => end == null ? this.WhereTestFieldByteNullableRange(begin) : base.Where(@"a.[testFieldByteNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDateTimeRange(DateTime? begin) => base.Where(@"a.[testFieldDateTime] >= {0}", begin);
			public SelectBuild WhereTestFieldDateTimeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereTestFieldDateTimeRange(begin) : base.Where(@"a.[testFieldDateTime] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDateTimeNullableRange(DateTime? begin) => base.Where(@"a.[testFieldDateTimeNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldDateTimeNullableRange(DateTime? begin, DateTime? end) => end == null ? this.WhereTestFieldDateTimeNullableRange(begin) : base.Where(@"a.[testFieldDateTimeNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDateTimeNullableOffsetRange(DateTimeOffset? begin) => base.Where(@"a.[testFieldDateTimeNullableOffset] >= {0}", begin);
			public SelectBuild WhereTestFieldDateTimeNullableOffsetRange(DateTimeOffset? begin, DateTimeOffset? end) => end == null ? this.WhereTestFieldDateTimeNullableOffsetRange(begin) : base.Where(@"a.[testFieldDateTimeNullableOffset] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDateTimeOffsetRange(DateTimeOffset? begin) => base.Where(@"a.[testFieldDateTimeOffset] >= {0}", begin);
			public SelectBuild WhereTestFieldDateTimeOffsetRange(DateTimeOffset? begin, DateTimeOffset? end) => end == null ? this.WhereTestFieldDateTimeOffsetRange(begin) : base.Where(@"a.[testFieldDateTimeOffset] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDecimal(params decimal?[] TestFieldDecimal) => this.Where1Or(@"a.[testFieldDecimal] = {0}", TestFieldDecimal);
			public SelectBuild WhereTestFieldDecimalRange(decimal? begin) => base.Where(@"a.[testFieldDecimal] >= {0}", begin);
			public SelectBuild WhereTestFieldDecimalRange(decimal? begin, decimal? end) => end == null ? this.WhereTestFieldDecimalRange(begin) : base.Where(@"a.[testFieldDecimal] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDecimalNullable(params decimal?[] TestFieldDecimalNullable) => this.Where1Or(@"a.[testFieldDecimalNullable] = {0}", TestFieldDecimalNullable);
			public SelectBuild WhereTestFieldDecimalNullableRange(decimal? begin) => base.Where(@"a.[testFieldDecimalNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldDecimalNullableRange(decimal? begin, decimal? end) => end == null ? this.WhereTestFieldDecimalNullableRange(begin) : base.Where(@"a.[testFieldDecimalNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDouble(params double?[] TestFieldDouble) => this.Where1Or(@"a.[testFieldDouble] = {0}", TestFieldDouble);
			public SelectBuild WhereTestFieldDoubleRange(double? begin) => base.Where(@"a.[testFieldDouble] >= {0}", begin);
			public SelectBuild WhereTestFieldDoubleRange(double? begin, double? end) => end == null ? this.WhereTestFieldDoubleRange(begin) : base.Where(@"a.[testFieldDouble] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldDoubleNullable(params double?[] TestFieldDoubleNullable) => this.Where1Or(@"a.[testFieldDoubleNullable] = {0}", TestFieldDoubleNullable);
			public SelectBuild WhereTestFieldDoubleNullableRange(double? begin) => base.Where(@"a.[testFieldDoubleNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldDoubleNullableRange(double? begin, double? end) => end == null ? this.WhereTestFieldDoubleNullableRange(begin) : base.Where(@"a.[testFieldDoubleNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldEnum1(params int?[] TestFieldEnum1) => this.Where1Or(@"a.[testFieldEnum1] = {0}", TestFieldEnum1);
			public SelectBuild WhereTestFieldEnum1Range(int? begin) => base.Where(@"a.[testFieldEnum1] >= {0}", begin);
			public SelectBuild WhereTestFieldEnum1Range(int? begin, int? end) => end == null ? this.WhereTestFieldEnum1Range(begin) : base.Where(@"a.[testFieldEnum1] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldEnum1Nullable(params int?[] TestFieldEnum1Nullable) => this.Where1Or(@"a.[testFieldEnum1Nullable] = {0}", TestFieldEnum1Nullable);
			public SelectBuild WhereTestFieldEnum1NullableRange(int? begin) => base.Where(@"a.[testFieldEnum1Nullable] >= {0}", begin);
			public SelectBuild WhereTestFieldEnum1NullableRange(int? begin, int? end) => end == null ? this.WhereTestFieldEnum1NullableRange(begin) : base.Where(@"a.[testFieldEnum1Nullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldEnum2(params long?[] TestFieldEnum2) => this.Where1Or(@"a.[testFieldEnum2] = {0}", TestFieldEnum2);
			public SelectBuild WhereTestFieldEnum2Range(long? begin) => base.Where(@"a.[testFieldEnum2] >= {0}", begin);
			public SelectBuild WhereTestFieldEnum2Range(long? begin, long? end) => end == null ? this.WhereTestFieldEnum2Range(begin) : base.Where(@"a.[testFieldEnum2] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldEnum2Nullable(params long?[] TestFieldEnum2Nullable) => this.Where1Or(@"a.[testFieldEnum2Nullable] = {0}", TestFieldEnum2Nullable);
			public SelectBuild WhereTestFieldEnum2NullableRange(long? begin) => base.Where(@"a.[testFieldEnum2Nullable] >= {0}", begin);
			public SelectBuild WhereTestFieldEnum2NullableRange(long? begin, long? end) => end == null ? this.WhereTestFieldEnum2NullableRange(begin) : base.Where(@"a.[testFieldEnum2Nullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldFloat(params float?[] TestFieldFloat) => this.Where1Or(@"a.[testFieldFloat] = {0}", TestFieldFloat);
			public SelectBuild WhereTestFieldFloatRange(float? begin) => base.Where(@"a.[testFieldFloat] >= {0}", begin);
			public SelectBuild WhereTestFieldFloatRange(float? begin, float? end) => end == null ? this.WhereTestFieldFloatRange(begin) : base.Where(@"a.[testFieldFloat] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldFloatNullable(params float?[] TestFieldFloatNullable) => this.Where1Or(@"a.[testFieldFloatNullable] = {0}", TestFieldFloatNullable);
			public SelectBuild WhereTestFieldFloatNullableRange(float? begin) => base.Where(@"a.[testFieldFloatNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldFloatNullableRange(float? begin, float? end) => end == null ? this.WhereTestFieldFloatNullableRange(begin) : base.Where(@"a.[testFieldFloatNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldGuid(params Guid?[] TestFieldGuid) => this.Where1Or(@"a.[testFieldGuid] = {0}", TestFieldGuid);
			public SelectBuild WhereTestFieldGuidNullable(params Guid?[] TestFieldGuidNullable) => this.Where1Or(@"a.[testFieldGuidNullable] = {0}", TestFieldGuidNullable);
			public SelectBuild WhereTestFieldInt(params int?[] TestFieldInt) => this.Where1Or(@"a.[testFieldInt] = {0}", TestFieldInt);
			public SelectBuild WhereTestFieldIntRange(int? begin) => base.Where(@"a.[testFieldInt] >= {0}", begin);
			public SelectBuild WhereTestFieldIntRange(int? begin, int? end) => end == null ? this.WhereTestFieldIntRange(begin) : base.Where(@"a.[testFieldInt] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldIntNullable(params int?[] TestFieldIntNullable) => this.Where1Or(@"a.[testFieldIntNullable] = {0}", TestFieldIntNullable);
			public SelectBuild WhereTestFieldIntNullableRange(int? begin) => base.Where(@"a.[testFieldIntNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldIntNullableRange(int? begin, int? end) => end == null ? this.WhereTestFieldIntNullableRange(begin) : base.Where(@"a.[testFieldIntNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldLong(params long?[] TestFieldLong) => this.Where1Or(@"a.[testFieldLong] = {0}", TestFieldLong);
			public SelectBuild WhereTestFieldLongRange(long? begin) => base.Where(@"a.[testFieldLong] >= {0}", begin);
			public SelectBuild WhereTestFieldLongRange(long? begin, long? end) => end == null ? this.WhereTestFieldLongRange(begin) : base.Where(@"a.[testFieldLong] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldSByte(params byte?[] TestFieldSByte) => this.Where1Or(@"a.[testFieldSByte] = {0}", TestFieldSByte);
			public SelectBuild WhereTestFieldSByteRange(byte? begin) => base.Where(@"a.[testFieldSByte] >= {0}", begin);
			public SelectBuild WhereTestFieldSByteRange(byte? begin, byte? end) => end == null ? this.WhereTestFieldSByteRange(begin) : base.Where(@"a.[testFieldSByte] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldSByteNullable(params byte?[] TestFieldSByteNullable) => this.Where1Or(@"a.[testFieldSByteNullable] = {0}", TestFieldSByteNullable);
			public SelectBuild WhereTestFieldSByteNullableRange(byte? begin) => base.Where(@"a.[testFieldSByteNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldSByteNullableRange(byte? begin, byte? end) => end == null ? this.WhereTestFieldSByteNullableRange(begin) : base.Where(@"a.[testFieldSByteNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldShort(params short?[] TestFieldShort) => this.Where1Or(@"a.[testFieldShort] = {0}", TestFieldShort);
			public SelectBuild WhereTestFieldShortRange(short? begin) => base.Where(@"a.[testFieldShort] >= {0}", begin);
			public SelectBuild WhereTestFieldShortRange(short? begin, short? end) => end == null ? this.WhereTestFieldShortRange(begin) : base.Where(@"a.[testFieldShort] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldShortNullable(params short?[] TestFieldShortNullable) => this.Where1Or(@"a.[testFieldShortNullable] = {0}", TestFieldShortNullable);
			public SelectBuild WhereTestFieldShortNullableRange(short? begin) => base.Where(@"a.[testFieldShortNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldShortNullableRange(short? begin, short? end) => end == null ? this.WhereTestFieldShortNullableRange(begin) : base.Where(@"a.[testFieldShortNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldString(params string[] TestFieldString) => this.Where1Or(@"a.[testFieldString] = {0}", TestFieldString);
			public SelectBuild WhereTestFieldStringLike(string pattern, bool isNotLike = false) => this.Where($@"a.[testFieldString] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereTestFieldTimeSpanRange(TimeSpan? begin) => base.Where(@"a.[testFieldTimeSpan] >= {0}", begin);
			public SelectBuild WhereTestFieldTimeSpanRange(TimeSpan? begin, TimeSpan? end) => end == null ? this.WhereTestFieldTimeSpanRange(begin) : base.Where(@"a.[testFieldTimeSpan] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldTimeSpanNullableRange(TimeSpan? begin) => base.Where(@"a.[testFieldTimeSpanNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldTimeSpanNullableRange(TimeSpan? begin, TimeSpan? end) => end == null ? this.WhereTestFieldTimeSpanNullableRange(begin) : base.Where(@"a.[testFieldTimeSpanNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldUInt(params int?[] TestFieldUInt) => this.Where1Or(@"a.[testFieldUInt] = {0}", TestFieldUInt);
			public SelectBuild WhereTestFieldUIntRange(int? begin) => base.Where(@"a.[testFieldUInt] >= {0}", begin);
			public SelectBuild WhereTestFieldUIntRange(int? begin, int? end) => end == null ? this.WhereTestFieldUIntRange(begin) : base.Where(@"a.[testFieldUInt] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldUIntNullable(params int?[] TestFieldUIntNullable) => this.Where1Or(@"a.[testFieldUIntNullable] = {0}", TestFieldUIntNullable);
			public SelectBuild WhereTestFieldUIntNullableRange(int? begin) => base.Where(@"a.[testFieldUIntNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldUIntNullableRange(int? begin, int? end) => end == null ? this.WhereTestFieldUIntNullableRange(begin) : base.Where(@"a.[testFieldUIntNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldULong(params long?[] TestFieldULong) => this.Where1Or(@"a.[testFieldULong] = {0}", TestFieldULong);
			public SelectBuild WhereTestFieldULongRange(long? begin) => base.Where(@"a.[testFieldULong] >= {0}", begin);
			public SelectBuild WhereTestFieldULongRange(long? begin, long? end) => end == null ? this.WhereTestFieldULongRange(begin) : base.Where(@"a.[testFieldULong] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldULongNullable(params long?[] TestFieldULongNullable) => this.Where1Or(@"a.[testFieldULongNullable] = {0}", TestFieldULongNullable);
			public SelectBuild WhereTestFieldULongNullableRange(long? begin) => base.Where(@"a.[testFieldULongNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldULongNullableRange(long? begin, long? end) => end == null ? this.WhereTestFieldULongNullableRange(begin) : base.Where(@"a.[testFieldULongNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldUShort(params short?[] TestFieldUShort) => this.Where1Or(@"a.[testFieldUShort] = {0}", TestFieldUShort);
			public SelectBuild WhereTestFieldUShortRange(short? begin) => base.Where(@"a.[testFieldUShort] >= {0}", begin);
			public SelectBuild WhereTestFieldUShortRange(short? begin, short? end) => end == null ? this.WhereTestFieldUShortRange(begin) : base.Where(@"a.[testFieldUShort] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFieldUShortNullable(params short?[] TestFieldUShortNullable) => this.Where1Or(@"a.[testFieldUShortNullable] = {0}", TestFieldUShortNullable);
			public SelectBuild WhereTestFieldUShortNullableRange(short? begin) => base.Where(@"a.[testFieldUShortNullable] >= {0}", begin);
			public SelectBuild WhereTestFieldUShortNullableRange(short? begin, short? end) => end == null ? this.WhereTestFieldUShortNullableRange(begin) : base.Where(@"a.[testFieldUShortNullable] between {0} and {1}", begin, end);
			public SelectBuild WhereTestFielLongNullable(params long?[] TestFielLongNullable) => this.Where1Or(@"a.[testFielLongNullable] = {0}", TestFielLongNullable);
			public SelectBuild WhereTestFielLongNullableRange(long? begin) => base.Where(@"a.[testFielLongNullable] >= {0}", begin);
			public SelectBuild WhereTestFielLongNullableRange(long? begin, long? end) => end == null ? this.WhereTestFielLongNullableRange(begin) : base.Where(@"a.[testFielLongNullable] between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}