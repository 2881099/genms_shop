﻿using System;
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

		public static int Update(TagInfo item) => dal.Update(item).ExecuteNonQuery();
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
		public static TagSelectBuild Select => new TagSelectBuild(dal);
		public static TagSelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static TagSelectBuild SelectByGoods(params GoodsInfo[] goodss) => Select.WhereGoods(goodss);
		public static TagSelectBuild SelectByGoods_id(params int[] goods_ids) => Select.WhereGoods_id(goods_ids);

		#region async
		async public static Task<TagInfo> DeleteAsync(int Id) {
			var item = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<TagInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL_Tag_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync());
		async public static Task<int> UpdateAsync(TagInfo item) => await dal.Update(item).ExecuteNonQueryAsync();

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
		async internal static Task RemoveCacheAsync(TagInfo item) => await RemoveCacheAsync(item == null ? null : new [] { item });
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
	}
	public partial class TagSelectBuild : SelectBuild<TagInfo, TagSelectBuild> {
		public TagSelectBuild WhereGoods(params GoodsInfo[] goodss) => WhereGoods(goodss?.ToArray(), null);
		public TagSelectBuild WhereGoods_id(params int[] goods_ids) => WhereGoods_id(goods_ids?.ToArray(), null);
		public TagSelectBuild WhereGoods(GoodsInfo[] goodss, Action<Goods_tagSelectBuild> subCondition) => WhereGoods_id(goodss?.Where<GoodsInfo>(a => a != null).Select<GoodsInfo, int>(a => a.Id.Value).ToArray(), subCondition);
		public TagSelectBuild WhereGoods_id(int[] goods_ids, Action<Goods_tagSelectBuild> subCondition) {
			if (goods_ids == null || goods_ids.Length == 0) return this;
			Goods_tagSelectBuild subConditionSelect = Goods_tag.Select.Where(string.Format(@"[tag_id] = a . [id] AND [goods_id] IN ('{0}')", string.Join("','", goods_ids.Select(a => string.Concat(a).Replace("'", "''")))));
			if (subCondition != null) subCondition(subConditionSelect);
			var subConditionSql = subConditionSelect.ToString(@"[tag_id]").Replace("] a \r\nWHERE (", "] WHERE (");
			if (subCondition != null) subConditionSql = subConditionSql.Replace("a.[", "[dbo].[goods_tag].[");
			return base.Where($"EXISTS({subConditionSql})");
		}
		public TagSelectBuild WhereId(params int[] Id) => this.Where1Or(@"a.[id] = {0}", Id);
		public TagSelectBuild WhereIdRange(int? begin) => base.Where(@"a.[id] >= {0}", begin);
		public TagSelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[id] between {0} and {1}", begin, end);
		public TagSelectBuild WhereName(params string[] Name) => this.Where1Or(@"a.[name] = {0}", Name);
		public TagSelectBuild WhereNameLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[name] {opt} {{0}}", pattern);
		}
		public TagSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}