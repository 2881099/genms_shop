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

		#region enum _
		public enum _ {
			/// <summary>
			/// 主键
			/// </summary>
			F_Id = 1, 
			/// <summary>
			/// 创建日期
			/// </summary>
			F_CreatorTime, 
			/// <summary>
			/// 创建用户主键
			/// </summary>
			F_CreatorUserId, 
			/// <summary>
			/// 删除标志
			/// </summary>
			F_DeleteMark, 
			/// <summary>
			/// 删除时间
			/// </summary>
			F_DeleteTime, 
			/// <summary>
			/// 删除用户
			/// </summary>
			F_DeleteUserId, 
			/// <summary>
			/// 描述
			/// </summary>
			F_Description, 
			/// <summary>
			/// 有效标志
			/// </summary>
			F_EnabledMark, 
			/// <summary>
			/// 编码
			/// </summary>
			F_EnCode, 
			/// <summary>
			/// 名称
			/// </summary>
			F_FullName, 
			/// <summary>
			/// 最后修改时间
			/// </summary>
			F_LastModifyTime, 
			/// <summary>
			/// 最后修改用户
			/// </summary>
			F_LastModifyUserId, 
			/// <summary>
			/// 层次
			/// </summary>
			F_Layers, 
			/// <summary>
			/// 父级
			/// </summary>
			F_ParentId, 
			/// <summary>
			/// 简拼
			/// </summary>
			F_SimpleSpelling, 
			/// <summary>
			/// 排序码
			/// </summary>
			F_SortCode
		}
		#endregion

		public static int Update(Sys_AreaInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Sys_AreaInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
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
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<Sys_AreaInfo> DeleteAsync(string F_Id) {
			var item = await dal.DeleteAsync(F_Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<Sys_AreaInfo> GetItemAsync(string F_Id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL_Sys_Area_", F_Id), itemCacheTimeout, () => Select.WhereF_Id(F_Id).ToOneAsync());
		public static Task<int> UpdateAsync(Sys_AreaInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Sys_AreaInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

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
		internal static Task RemoveCacheAsync(Sys_AreaInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
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

		public partial class SelectBuild : SelectBuild<Sys_AreaInfo, SelectBuild> {
			/// <summary>
			/// 主键，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_Id(params string[] F_Id) => this.Where1Or(@"a.[F_Id] = {0}", F_Id);
			public SelectBuild WhereF_IdLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_Id] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereF_CreatorTimeRange(DateTime? begin) => base.Where(@"a.[F_CreatorTime] >= {0}", begin);
			public SelectBuild WhereF_CreatorTimeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereF_CreatorTimeRange(begin) : base.Where(@"a.[F_CreatorTime] between {0} and {1}", begin, end);
			/// <summary>
			/// 创建用户主键，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_CreatorUserId(params string[] F_CreatorUserId) => this.Where1Or(@"a.[F_CreatorUserId] = {0}", F_CreatorUserId);
			public SelectBuild WhereF_CreatorUserIdLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_CreatorUserId] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 删除标志，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_DeleteMark(params bool?[] F_DeleteMark) => this.Where1Or(@"a.[F_DeleteMark] = {0}", F_DeleteMark);
			public SelectBuild WhereF_DeleteTimeRange(DateTime? begin) => base.Where(@"a.[F_DeleteTime] >= {0}", begin);
			public SelectBuild WhereF_DeleteTimeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereF_DeleteTimeRange(begin) : base.Where(@"a.[F_DeleteTime] between {0} and {1}", begin, end);
			/// <summary>
			/// 删除用户，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_DeleteUserId(params string[] F_DeleteUserId) => this.Where1Or(@"a.[F_DeleteUserId] = {0}", F_DeleteUserId);
			public SelectBuild WhereF_DeleteUserIdLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_DeleteUserId] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereF_DescriptionLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_Description] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 有效标志，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_EnabledMark(params bool?[] F_EnabledMark) => this.Where1Or(@"a.[F_EnabledMark] = {0}", F_EnabledMark);
			/// <summary>
			/// 编码，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_EnCode(params string[] F_EnCode) => this.Where1Or(@"a.[F_EnCode] = {0}", F_EnCode);
			public SelectBuild WhereF_EnCodeLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_EnCode] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 名称，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_FullName(params string[] F_FullName) => this.Where1Or(@"a.[F_FullName] = {0}", F_FullName);
			public SelectBuild WhereF_FullNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_FullName] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild WhereF_LastModifyTimeRange(DateTime? begin) => base.Where(@"a.[F_LastModifyTime] >= {0}", begin);
			public SelectBuild WhereF_LastModifyTimeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereF_LastModifyTimeRange(begin) : base.Where(@"a.[F_LastModifyTime] between {0} and {1}", begin, end);
			/// <summary>
			/// 最后修改用户，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_LastModifyUserId(params string[] F_LastModifyUserId) => this.Where1Or(@"a.[F_LastModifyUserId] = {0}", F_LastModifyUserId);
			public SelectBuild WhereF_LastModifyUserIdLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_LastModifyUserId] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 层次，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_Layers(params int?[] F_Layers) => this.Where1Or(@"a.[F_Layers] = {0}", F_Layers);
			public SelectBuild WhereF_LayersRange(int? begin) => base.Where(@"a.[F_Layers] >= {0}", begin);
			public SelectBuild WhereF_LayersRange(int? begin, int? end) => end == null ? this.WhereF_LayersRange(begin) : base.Where(@"a.[F_Layers] between {0} and {1}", begin, end);
			/// <summary>
			/// 父级，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_ParentId(params string[] F_ParentId) => this.Where1Or(@"a.[F_ParentId] = {0}", F_ParentId);
			public SelectBuild WhereF_ParentIdLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_ParentId] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 简拼，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_SimpleSpelling(params string[] F_SimpleSpelling) => this.Where1Or(@"a.[F_SimpleSpelling] = {0}", F_SimpleSpelling);
			public SelectBuild WhereF_SimpleSpellingLike(string pattern, bool isNotLike = false) => this.Where($@"a.[F_SimpleSpelling] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			/// <summary>
			/// 排序码，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereF_SortCode(params int?[] F_SortCode) => this.Where1Or(@"a.[F_SortCode] = {0}", F_SortCode);
			public SelectBuild WhereF_SortCodeRange(int? begin) => base.Where(@"a.[F_SortCode] >= {0}", begin);
			public SelectBuild WhereF_SortCodeRange(int? begin, int? end) => end == null ? this.WhereF_SortCodeRange(begin) : base.Where(@"a.[F_SortCode] between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}