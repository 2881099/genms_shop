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

	public partial class Tag {

		internal static readonly es.DAL.Tag dal = new es.DAL.Tag();
		internal static readonly int itemCacheTimeout;

		static Tag() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Tag"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static TagInfo Delete(int Id) {
			var item = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}

		#region enum _
		public enum _ {
			/// <summary>
			/// 标签id（自增）
			/// </summary>
			Id = 1, 
			/// <summary>
			/// 标签名
			/// </summary>
			Name
		}
		#endregion

		public static int Update(TagInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TagInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static es.DAL.Tag.SqlUpdateBuild UpdateDiy(int Id) => new es.DAL.Tag.SqlUpdateBuild(new List<TagInfo> { new TagInfo { Id = Id } }, false);
		public static es.DAL.Tag.SqlUpdateBuild UpdateDiy(List<TagInfo> dataSource) => new es.DAL.Tag.SqlUpdateBuild(dataSource, true);
		public static es.DAL.Tag.SqlUpdateBuild UpdateDiyDangerous => new es.DAL.Tag.SqlUpdateBuild();

		public static TagInfo Insert(string Name) {
			return Insert(new TagInfo {
				Name = Name});
		}
		public static TagInfo Insert(TagInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<TagInfo> Insert(IEnumerable<TagInfo> items) {
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(TagInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TagInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Tag_", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TagInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("es_BLL_Tag_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne());

		public static List<TagInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static SelectBuild SelectByGoods(params GoodsInfo[] goodss) => Select.WhereGoods(goodss);
		public static SelectBuild SelectByGoods_id(params int[] goods_ids) => Select.WhereGoods_id(goods_ids);

		#region async
		async public static Task<TagInfo> DeleteAsync(int Id) {
			var item = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<TagInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL_Tag_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync());
		public static Task<int> UpdateAsync(TagInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TagInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<TagInfo> InsertAsync(string Name) {
			return InsertAsync(new TagInfo {
				Name = Name});
		}
		async public static Task<TagInfo> InsertAsync(TagInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<TagInfo>> InsertAsync(IEnumerable<TagInfo> items) {
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		internal static Task RemoveCacheAsync(TagInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TagInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Tag_", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TagInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TagInfo, SelectBuild> {
			public SelectBuild WhereGoods(params GoodsInfo[] goodss) => WhereGoods(goodss?.ToArray(), null);
			public SelectBuild WhereGoods_id(params int[] goods_ids) => WhereGoods_id(goods_ids?.ToArray(), null);
			public SelectBuild WhereGoods(GoodsInfo[] goodss, Action<Goods_tag.SelectBuild> subCondition) => WhereGoods_id(goodss?.Where<GoodsInfo>(a => a != null).Select<GoodsInfo, int>(a => a.Id.Value).ToArray(), subCondition);
			public SelectBuild WhereGoods_id(int[] goods_ids, Action<Goods_tag.SelectBuild> subCondition) {
				if (goods_ids == null || goods_ids.Length == 0) return this;
				Goods_tag.SelectBuild subConditionSelect = Goods_tag.Select.Where(string.Format(@"[tag_id] = a . [id] AND [goods_id] IN ('{0}')", string.Join("','", goods_ids.Select(a => string.Concat(a).Replace("'", "''")))));
				subCondition?.Invoke(subConditionSelect);
				var subConditionSql = subConditionSelect.ToString(@"[tag_id]").Replace("] a \r\nWHERE (", "] WHERE (");
				if (subCondition != null) subConditionSql = subConditionSql.Replace("a.[", "[dbo].[goods_tag].[");
				return base.Where($"EXISTS({subConditionSql})");
			}
			/// <summary>
			/// 标签id（自增），多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereId(params int[] Id) => this.Where1Or(@"a.[id] = {0}", Id);
			public SelectBuild WhereIdRange(int? begin) => base.Where(@"a.[id] >= {0}", begin);
			public SelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[id] between {0} and {1}", begin, end);
			/// <summary>
			/// 标签名，多个参数等于 OR 查询
			/// </summary>
			public SelectBuild WhereName(params string[] Name) => this.Where1Or(@"a.[name] = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.[name] {(isNotLike ? "LIKE" : "NOT LIKE")} {{0}}", pattern);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}