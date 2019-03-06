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

	public partial class Goods {

		internal static readonly es.DAL.Goods dal = new es.DAL.Goods();
		internal static readonly int itemCacheTimeout;

		static Goods() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Goods"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static GoodsInfo Delete(int Id) {
			var item = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<GoodsInfo> DeleteByCategory_id(int Category_id) {
			var items = dal.DeleteByCategory_id(Category_id);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return items;
		}

		#region enum _
		public enum _ {
			/// <summary>
			/// 产品id（自增）
			/// </summary>
			Id = 1, 
			/// <summary>
			/// 分类id
			/// </summary>
			Category_id, 
			/// <summary>
			/// 产品介绍
			/// </summary>
			Content, 
			/// <summary>
			/// 创建时间
			/// </summary>
			Create_time, 
			/// <summary>
			/// 图片
			/// </summary>
			Imgs, 
			/// <summary>
			/// 库存
			/// </summary>
			Stock, 
			/// <summary>
			/// 标题
			/// </summary>
			Title, 
			/// <summary>
			/// 更新时间
			/// </summary>
			Update_time
		}
		#endregion

		public static int Update(GoodsInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(GoodsInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static es.DAL.Goods.SqlUpdateBuild UpdateDiy(int Id) => new es.DAL.Goods.SqlUpdateBuild(new List<GoodsInfo> { new GoodsInfo { Id = Id } }, false);
		public static es.DAL.Goods.SqlUpdateBuild UpdateDiy(List<GoodsInfo> dataSource) => new es.DAL.Goods.SqlUpdateBuild(dataSource, true);
		public static es.DAL.Goods.SqlUpdateBuild UpdateDiyDangerous => new es.DAL.Goods.SqlUpdateBuild();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Goods.Insert(GoodsInfo item)
		/// </summary>
		[Obsolete]
		public static GoodsInfo Insert(int? Category_id, string Content, string Imgs, int? Stock, string Title) {
			return Insert(new GoodsInfo {
				Category_id = Category_id, 
				Content = Content, 
				Imgs = Imgs, 
				Stock = Stock, 
				Title = Title});
		}
		public static GoodsInfo Insert(GoodsInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			if (item.Update_time == null) item.Update_time = DateTime.Now;
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<GoodsInfo> Insert(IEnumerable<GoodsInfo> items) {
			foreach (var item in items) if (item != null && item.Create_time == null) item.Create_time = DateTime.Now;
			foreach (var item in items) if (item != null && item.Update_time == null) item.Update_time = DateTime.Now;
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(GoodsInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<GoodsInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL:Goods:", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static GoodsInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("es_BLL:Goods:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne());

		public static List<GoodsInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<GoodsInfo> GetItemsByCategory_id(params int?[] Category_id) => Select.WhereCategory_id(Category_id).ToList();
		public static List<GoodsInfo> GetItemsByCategory_id(int?[] Category_id, int limit) => Select.WhereCategory_id(Category_id).Limit(limit).ToList();
		public static SelectBuild SelectByCategory_id(params int?[] Category_id) => Select.WhereCategory_id(Category_id);
		public static SelectBuild SelectByTag(params TagInfo[] tags) => Select.WhereTag(tags);
		public static SelectBuild SelectByTag_id(params int[] tag_ids) => Select.WhereTag_id(tag_ids);

		#region async
		async public static Task<List<GoodsInfo>> DeleteByCategory_idAsync(int Category_id) {
			var items = await dal.DeleteByCategory_idAsync(Category_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return items;
		}
		async public static Task<GoodsInfo> DeleteAsync(int Id) {
			var item = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<GoodsInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL:Goods:", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync());
		public static Task<int> UpdateAsync(GoodsInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(GoodsInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		/// <summary>
		/// 适用字段较少的表；避规后续改表风险，字段数较大请改用 Goods.Insert(GoodsInfo item)
		/// </summary>
		[Obsolete]
		public static Task<GoodsInfo> InsertAsync(int? Category_id, string Content, string Imgs, int? Stock, string Title) {
			return InsertAsync(new GoodsInfo {
				Category_id = Category_id, 
				Content = Content, 
				Imgs = Imgs, 
				Stock = Stock, 
				Title = Title});
		}
		async public static Task<GoodsInfo> InsertAsync(GoodsInfo item) {
			if (item.Create_time == null) item.Create_time = DateTime.Now;
			if (item.Update_time == null) item.Update_time = DateTime.Now;
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<GoodsInfo>> InsertAsync(IEnumerable<GoodsInfo> items) {
			foreach (var item in items) if (item != null && item.Create_time == null) item.Create_time = DateTime.Now;
			foreach (var item in items) if (item != null && item.Update_time == null) item.Update_time = DateTime.Now;
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		internal static Task RemoveCacheAsync(GoodsInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<GoodsInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL:Goods:", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<GoodsInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<GoodsInfo>> GetItemsByCategory_idAsync(params int?[] Category_id) => Select.WhereCategory_id(Category_id).ToListAsync();
		public static Task<List<GoodsInfo>> GetItemsByCategory_idAsync(int?[] Category_id, int limit) => Select.WhereCategory_id(Category_id).Limit(limit).ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<GoodsInfo, SelectBuild> {
			public SelectBuild WhereCategory_id(params int?[] Category_id) => this.Where1Or(@"a.[category_id] = {0}", Category_id);
			public SelectBuild WhereCategory_id(Category.SelectBuild select, bool isNotIn = false) => this.Where($@"a.[category_id] {(isNotIn ? "NOT IN" : "IN")} ({select.ToString(@"[id]")})");
			public SelectBuild WhereTag(params TagInfo[] tags) => WhereTag(tags?.ToArray(), null);
			public SelectBuild WhereTag_id(params int[] tag_ids) => WhereTag_id(tag_ids?.ToArray(), null);
			public SelectBuild WhereTag(TagInfo[] tags, Action<Goods_tag.SelectBuild> subCondition) => WhereTag_id(tags?.Where<TagInfo>(a => a != null).Select<TagInfo, int>(a => a.Id.Value).ToArray(), subCondition);
			public SelectBuild WhereTag_id(int[] tag_ids, Action<Goods_tag.SelectBuild> subCondition) {
				if (tag_ids == null || tag_ids.Length == 0) return this;
				Goods_tag.SelectBuild subConditionSelect = Goods_tag.Select.Where(string.Format(@"[goods_id] = a . [id] AND [tag_id] IN ('{0}')", string.Join("','", tag_ids.Select(a => string.Concat(a).Replace("'", "''")))));
				subCondition?.Invoke(subConditionSelect);
				var subConditionSql = subConditionSelect.ToString(@"[goods_id]").Replace("] a \r\nWHERE (", "] WHERE (");
				if (subCondition != null) subConditionSql = subConditionSql.Replace("a.[", "[dbo].[goods_tag].[");
				return base.Where($"EXISTS({subConditionSql})");
			}
			/// <summary>
			/// 产品id（自增），多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereId(params int[] Id) => this.Where1Or(@"a.[id] = {0}", Id);
			public SelectBuild WhereIdRange(int? begin) => base.Where(@"a.[id] >= {0}", begin);
			public SelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[id] between {0} and {1}", begin, end);
			public SelectBuild WhereContentLike(string pattern, bool isNotLike = false) => this.Where($@"a.[content] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where(@"a.[create_time] >= {0}", begin);
			public SelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereCreate_timeRange(begin) : base.Where(@"a.[create_time] between {0} and {1}", begin, end);
			public SelectBuild WhereImgsLike(string pattern, bool isNotLike = false) => this.Where($@"a.[imgs] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			/// <summary>
			/// 库存，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereStock(params int?[] Stock) => this.Where1Or(@"a.[stock] = {0}", Stock);
			public SelectBuild WhereStockRange(int? begin) => base.Where(@"a.[stock] >= {0}", begin);
			public SelectBuild WhereStockRange(int? begin, int? end) => end == null ? this.WhereStockRange(begin) : base.Where(@"a.[stock] between {0} and {1}", begin, end);
			/// <summary>
			/// 标题，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or(@"a.[title] = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.[title] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereUpdate_timeRange(DateTime? begin) => base.Where(@"a.[update_time] >= {0}", begin);
			public SelectBuild WhereUpdate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereUpdate_timeRange(begin) : base.Where(@"a.[update_time] between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}