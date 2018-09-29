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

	public partial class Sys_Area {

		internal static readonly es.DAL.Sys_Area dal = new es.DAL.Sys_Area();
		internal static readonly int itemCacheTimeout;

		static Sys_Area() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Sys_Area"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static Sys_AreaInfo Delete(string F_Id) {
			var item = dal.Delete(F_Id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}

		public static int Update(Sys_AreaInfo item) => dal.Update(item).ExecuteNonQuery();
		public static es.DAL.Sys_Area.SqlUpdateBuild UpdateDiy(string F_Id) => new es.DAL.Sys_Area.SqlUpdateBuild(new List<Sys_AreaInfo> { new Sys_AreaInfo { F_Id = F_Id } }, false);
		public static es.DAL.Sys_Area.SqlUpdateBuild UpdateDiy(List<Sys_AreaInfo> dataSource) => new es.DAL.Sys_Area.SqlUpdateBuild(dataSource, true);
		public static es.DAL.Sys_Area.SqlUpdateBuild UpdateDiyDangerous => new es.DAL.Sys_Area.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Sys_Area.Insert(Sys_AreaInfo item)
		/// </summary>
		[Obsolete]
		public static Sys_AreaInfo Insert(string F_Id, DateTime? F_CreatorTime, string F_CreatorUserId, bool? F_DeleteMark, DateTime? F_DeleteTime, string F_DeleteUserId, string F_Description, bool? F_EnabledMark, string F_EnCode, string F_FullName, DateTime? F_LastModifyTime, string F_LastModifyUserId, int? F_Layers, string F_ParentId, string F_SimpleSpelling, int? F_SortCode) {
			return Insert(new Sys_AreaInfo {
				F_Id = F_Id, 
				F_CreatorTime = F_CreatorTime, 
				F_CreatorUserId = F_CreatorUserId, 
				F_DeleteMark = F_DeleteMark, 
				F_DeleteTime = F_DeleteTime, 
				F_DeleteUserId = F_DeleteUserId, 
				F_Description = F_Description, 
				F_EnabledMark = F_EnabledMark, 
				F_EnCode = F_EnCode, 
				F_FullName = F_FullName, 
				F_LastModifyTime = F_LastModifyTime, 
				F_LastModifyUserId = F_LastModifyUserId, 
				F_Layers = F_Layers, 
				F_ParentId = F_ParentId, 
				F_SimpleSpelling = F_SimpleSpelling, 
				F_SortCode = F_SortCode});
		}
		public static Sys_AreaInfo Insert(Sys_AreaInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<Sys_AreaInfo> Insert(IEnumerable<Sys_AreaInfo> items) {
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(Sys_AreaInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Sys_AreaInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Sys_Area_", item.F_Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Sys_AreaInfo GetItem(string F_Id) => SqlHelper.CacheShell(string.Concat("es_BLL_Sys_Area_", F_Id), itemCacheTimeout, () => Select.WhereF_Id(F_Id).ToOne());

		public static List<Sys_AreaInfo> GetItems() => Select.ToList();
		public static Sys_AreaSelectBuild Select => new Sys_AreaSelectBuild(dal);
		public static Sys_AreaSelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<Sys_AreaInfo> DeleteAsync(string F_Id) {
			var item = await dal.DeleteAsync(F_Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<Sys_AreaInfo> GetItemAsync(string F_Id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL_Sys_Area_", F_Id), itemCacheTimeout, () => Select.WhereF_Id(F_Id).ToOneAsync());
		async public static Task<int> UpdateAsync(Sys_AreaInfo item) => await dal.Update(item).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Sys_Area.Insert(Sys_AreaInfo item)
		/// </summary>
		[Obsolete]
		public static Task<Sys_AreaInfo> InsertAsync(string F_Id, DateTime? F_CreatorTime, string F_CreatorUserId, bool? F_DeleteMark, DateTime? F_DeleteTime, string F_DeleteUserId, string F_Description, bool? F_EnabledMark, string F_EnCode, string F_FullName, DateTime? F_LastModifyTime, string F_LastModifyUserId, int? F_Layers, string F_ParentId, string F_SimpleSpelling, int? F_SortCode) {
			return InsertAsync(new Sys_AreaInfo {
				F_Id = F_Id, 
				F_CreatorTime = F_CreatorTime, 
				F_CreatorUserId = F_CreatorUserId, 
				F_DeleteMark = F_DeleteMark, 
				F_DeleteTime = F_DeleteTime, 
				F_DeleteUserId = F_DeleteUserId, 
				F_Description = F_Description, 
				F_EnabledMark = F_EnabledMark, 
				F_EnCode = F_EnCode, 
				F_FullName = F_FullName, 
				F_LastModifyTime = F_LastModifyTime, 
				F_LastModifyUserId = F_LastModifyUserId, 
				F_Layers = F_Layers, 
				F_ParentId = F_ParentId, 
				F_SimpleSpelling = F_SimpleSpelling, 
				F_SortCode = F_SortCode});
		}
		async public static Task<Sys_AreaInfo> InsertAsync(Sys_AreaInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<Sys_AreaInfo>> InsertAsync(IEnumerable<Sys_AreaInfo> items) {
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		async internal static Task RemoveCacheAsync(Sys_AreaInfo item) => await RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Sys_AreaInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Sys_Area_", item.F_Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Sys_AreaInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion
	}
	public partial class Sys_AreaSelectBuild : SelectBuild<Sys_AreaInfo, Sys_AreaSelectBuild> {
		public Sys_AreaSelectBuild WhereF_Id(params string[] F_Id) => this.Where1Or(@"a.[F_Id] = {0}", F_Id);
		public Sys_AreaSelectBuild WhereF_IdLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_Id] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_CreatorTimeRange(DateTime? begin) => base.Where(@"a.[F_CreatorTime] >= {0}", begin);
		public Sys_AreaSelectBuild WhereF_CreatorTimeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereF_CreatorTimeRange(begin) : base.Where(@"a.[F_CreatorTime] between {0} and {1}", begin, end);
		public Sys_AreaSelectBuild WhereF_CreatorUserId(params string[] F_CreatorUserId) => this.Where1Or(@"a.[F_CreatorUserId] = {0}", F_CreatorUserId);
		public Sys_AreaSelectBuild WhereF_CreatorUserIdLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_CreatorUserId] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_DeleteMark(params bool?[] F_DeleteMark) => this.Where1Or(@"a.[F_DeleteMark] = {0}", F_DeleteMark);
		public Sys_AreaSelectBuild WhereF_DeleteTimeRange(DateTime? begin) => base.Where(@"a.[F_DeleteTime] >= {0}", begin);
		public Sys_AreaSelectBuild WhereF_DeleteTimeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereF_DeleteTimeRange(begin) : base.Where(@"a.[F_DeleteTime] between {0} and {1}", begin, end);
		public Sys_AreaSelectBuild WhereF_DeleteUserId(params string[] F_DeleteUserId) => this.Where1Or(@"a.[F_DeleteUserId] = {0}", F_DeleteUserId);
		public Sys_AreaSelectBuild WhereF_DeleteUserIdLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_DeleteUserId] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_DescriptionLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_Description] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_EnabledMark(params bool?[] F_EnabledMark) => this.Where1Or(@"a.[F_EnabledMark] = {0}", F_EnabledMark);
		public Sys_AreaSelectBuild WhereF_EnCode(params string[] F_EnCode) => this.Where1Or(@"a.[F_EnCode] = {0}", F_EnCode);
		public Sys_AreaSelectBuild WhereF_EnCodeLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_EnCode] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_FullName(params string[] F_FullName) => this.Where1Or(@"a.[F_FullName] = {0}", F_FullName);
		public Sys_AreaSelectBuild WhereF_FullNameLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_FullName] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_LastModifyTimeRange(DateTime? begin) => base.Where(@"a.[F_LastModifyTime] >= {0}", begin);
		public Sys_AreaSelectBuild WhereF_LastModifyTimeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereF_LastModifyTimeRange(begin) : base.Where(@"a.[F_LastModifyTime] between {0} and {1}", begin, end);
		public Sys_AreaSelectBuild WhereF_LastModifyUserId(params string[] F_LastModifyUserId) => this.Where1Or(@"a.[F_LastModifyUserId] = {0}", F_LastModifyUserId);
		public Sys_AreaSelectBuild WhereF_LastModifyUserIdLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_LastModifyUserId] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_Layers(params int?[] F_Layers) => this.Where1Or(@"a.[F_Layers] = {0}", F_Layers);
		public Sys_AreaSelectBuild WhereF_LayersRange(int? begin) => base.Where(@"a.[F_Layers] >= {0}", begin);
		public Sys_AreaSelectBuild WhereF_LayersRange(int? begin, int? end) => end == null ? this.WhereF_LayersRange(begin) : base.Where(@"a.[F_Layers] between {0} and {1}", begin, end);
		public Sys_AreaSelectBuild WhereF_ParentId(params string[] F_ParentId) => this.Where1Or(@"a.[F_ParentId] = {0}", F_ParentId);
		public Sys_AreaSelectBuild WhereF_ParentIdLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_ParentId] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_SimpleSpelling(params string[] F_SimpleSpelling) => this.Where1Or(@"a.[F_SimpleSpelling] = {0}", F_SimpleSpelling);
		public Sys_AreaSelectBuild WhereF_SimpleSpellingLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[F_SimpleSpelling] {opt} {{0}}", pattern);
		}
		public Sys_AreaSelectBuild WhereF_SortCode(params int?[] F_SortCode) => this.Where1Or(@"a.[F_SortCode] = {0}", F_SortCode);
		public Sys_AreaSelectBuild WhereF_SortCodeRange(int? begin) => base.Where(@"a.[F_SortCode] >= {0}", begin);
		public Sys_AreaSelectBuild WhereF_SortCodeRange(int? begin, int? end) => end == null ? this.WhereF_SortCodeRange(begin) : base.Where(@"a.[F_SortCode] between {0} and {1}", begin, end);
		public Sys_AreaSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}