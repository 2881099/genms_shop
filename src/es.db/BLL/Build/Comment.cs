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

	public partial class Comment {

		internal static readonly es.DAL.Comment dal = new es.DAL.Comment();
		internal static readonly int itemCacheTimeout;

		static Comment() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Comment"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static CommentInfo Delete(int Id) {
			var item = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<CommentInfo> DeleteByGoods_id(int Goods_id) {
			var items = dal.DeleteByGoods_id(Goods_id);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return items;
		}

		public static int Update(CommentInfo item) => dal.Update(item).ExecuteNonQuery();
		public static es.DAL.Comment.SqlUpdateBuild UpdateDiy(int Id) => new es.DAL.Comment.SqlUpdateBuild(new List<CommentInfo> { new CommentInfo { Id = Id } }, false);
		public static es.DAL.Comment.SqlUpdateBuild UpdateDiy(List<CommentInfo> dataSource) => new es.DAL.Comment.SqlUpdateBuild(dataSource, true);
		public static es.DAL.Comment.SqlUpdateBuild UpdateDiyDangerous => new es.DAL.Comment.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Comment.Insert(CommentInfo item)
		/// </summary>
		[Obsolete]
		public static CommentInfo Insert(int? Goods_id, string Content, string Nickname) {
			return Insert(new CommentInfo {
				Goods_id = Goods_id, 
				Content = Content, 
				Nickname = Nickname});
		}
		public static CommentInfo Insert(CommentInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			if (item.Update_time == null) item.Update_time = DateTime.Now;
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<CommentInfo> Insert(IEnumerable<CommentInfo> items) {
			foreach (var item in items) if (item != null && item.Create_time == null) item.Create_time = DateTime.Now;
			foreach (var item in items) if (item != null && item.Update_time == null) item.Update_time = DateTime.Now;
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(CommentInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<CommentInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Comment_", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static CommentInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("es_BLL_Comment_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne());

		public static List<CommentInfo> GetItems() => Select.ToList();
		public static CommentSelectBuild Select => new CommentSelectBuild(dal);
		public static CommentSelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<CommentInfo> GetItemsByGoods_id(params int?[] Goods_id) => Select.WhereGoods_id(Goods_id).ToList();
		public static List<CommentInfo> GetItemsByGoods_id(int?[] Goods_id, int limit) => Select.WhereGoods_id(Goods_id).Limit(limit).ToList();
		public static CommentSelectBuild SelectByGoods_id(params int?[] Goods_id) => Select.WhereGoods_id(Goods_id);

		#region async
		async public static Task<List<CommentInfo>> DeleteByGoods_idAsync(int Goods_id) {
			var items = await dal.DeleteByGoods_idAsync(Goods_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return items;
		}
		async public static Task<CommentInfo> DeleteAsync(int Id) {
			var item = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<CommentInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL_Comment_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync());
		async public static Task<int> UpdateAsync(CommentInfo item) => await dal.Update(item).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Comment.Insert(CommentInfo item)
		/// </summary>
		[Obsolete]
		public static Task<CommentInfo> InsertAsync(int? Goods_id, string Content, string Nickname) {
			return InsertAsync(new CommentInfo {
				Goods_id = Goods_id, 
				Content = Content, 
				Nickname = Nickname});
		}
		async public static Task<CommentInfo> InsertAsync(CommentInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			if (item.Update_time == null) item.Update_time = DateTime.Now;
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<CommentInfo>> InsertAsync(IEnumerable<CommentInfo> items) {
			foreach (var item in items) if (item != null && item.Create_time == null) item.Create_time = DateTime.Now;
			foreach (var item in items) if (item != null && item.Update_time == null) item.Update_time = DateTime.Now;
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		async internal static Task RemoveCacheAsync(CommentInfo item) => await RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<CommentInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Comment_", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<CommentInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<CommentInfo>> GetItemsByGoods_idAsync(params int?[] Goods_id) => Select.WhereGoods_id(Goods_id).ToListAsync();
		public static Task<List<CommentInfo>> GetItemsByGoods_idAsync(int?[] Goods_id, int limit) => Select.WhereGoods_id(Goods_id).Limit(limit).ToListAsync();
		#endregion
	}
	public partial class CommentSelectBuild : SelectBuild<CommentInfo, CommentSelectBuild> {
		public CommentSelectBuild WhereGoods_id(params int?[] Goods_id) {
			return this.Where1Or(@"a.[goods_id] = {0}", Goods_id);
		}
		public CommentSelectBuild WhereGoods_id(GoodsSelectBuild select, bool isNotIn = false) {
			var opt = isNotIn ? "NOT IN" : "IN";
			return this.Where($@"a.[goods_id] {opt} ({select.ToString(@"[id]")})");
		}
		public CommentSelectBuild WhereId(params int[] Id) => this.Where1Or(@"a.[id] = {0}", Id);
		public CommentSelectBuild WhereIdRange(int? begin) => base.Where(@"a.[id] >= {0}", begin);
		public CommentSelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[id] between {0} and {1}", begin, end);
		public CommentSelectBuild WhereContent(params string[] Content) => this.Where1Or(@"a.[content] = {0}", Content);
		public CommentSelectBuild WhereContentLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[content] {opt} {{0}}", pattern);
		}
		public CommentSelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where(@"a.[create_time] >= {0}", begin);
		public CommentSelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereCreate_timeRange(begin) : base.Where(@"a.[create_time] between {0} and {1}", begin, end);
		public CommentSelectBuild WhereNickname(params string[] Nickname) => this.Where1Or(@"a.[nickname] = {0}", Nickname);
		public CommentSelectBuild WhereNicknameLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[nickname] {opt} {{0}}", pattern);
		}
		public CommentSelectBuild WhereUpdate_timeRange(DateTime? begin) => base.Where(@"a.[update_time] >= {0}", begin);
		public CommentSelectBuild WhereUpdate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereUpdate_timeRange(begin) : base.Where(@"a.[update_time] between {0} and {1}", begin, end);
		public CommentSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}