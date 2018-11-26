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

	public partial class Goods_tag {

		internal static readonly es.DAL.Goods_tag dal = new es.DAL.Goods_tag();
		internal static readonly int itemCacheTimeout;

		static Goods_tag() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Goods_tag"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static Goods_tagInfo Delete(int Goods_id, int Tag_id) {
			var item = dal.Delete(Goods_id, Tag_id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<Goods_tagInfo> DeleteByGoods_id(int Goods_id) {
			var items = dal.DeleteByGoods_id(Goods_id);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return items;
		}
		public static List<Goods_tagInfo> DeleteByTag_id(int Tag_id) {
			var items = dal.DeleteByTag_id(Tag_id);
			if (itemCacheTimeout > 0) RemoveCache(items);
			return items;
		}

		#region enum _
		public enum _ {
			/// <summary>
			/// 产品id
			/// </summary>
			Goods_id = 1, 
			/// <summary>
			/// 标签id
			/// </summary>
			Tag_id
		}
		#endregion

		public static int Update(Goods_tagInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(Goods_tagInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static es.DAL.Goods_tag.SqlUpdateBuild UpdateDiy(int Goods_id, int Tag_id) => new es.DAL.Goods_tag.SqlUpdateBuild(new List<Goods_tagInfo> { new Goods_tagInfo { Goods_id = Goods_id, Tag_id = Tag_id } }, false);
		public static es.DAL.Goods_tag.SqlUpdateBuild UpdateDiy(List<Goods_tagInfo> dataSource) => new es.DAL.Goods_tag.SqlUpdateBuild(dataSource, true);
		public static es.DAL.Goods_tag.SqlUpdateBuild UpdateDiyDangerous => new es.DAL.Goods_tag.SqlUpdateBuild();

		public static Goods_tagInfo Insert(int? Goods_id, int? Tag_id) {
			return Insert(new Goods_tagInfo {
				Goods_id = Goods_id, 
				Tag_id = Tag_id});
		}
		public static Goods_tagInfo Insert(Goods_tagInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<Goods_tagInfo> Insert(IEnumerable<Goods_tagInfo> items) {
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(Goods_tagInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<Goods_tagInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Goods_tag_", item.Goods_id, "_,_", item.Tag_id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static Goods_tagInfo GetItem(int Goods_id, int Tag_id) => SqlHelper.CacheShell(string.Concat("es_BLL_Goods_tag_", Goods_id, "_,_", Tag_id), itemCacheTimeout, () => Select.WhereGoods_id(Goods_id).WhereTag_id(Tag_id).ToOne());

		public static List<Goods_tagInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);
		public static List<Goods_tagInfo> GetItemsByGoods_id(params int?[] Goods_id) => Select.WhereGoods_id(Goods_id).ToList();
		public static List<Goods_tagInfo> GetItemsByGoods_id(int?[] Goods_id, int limit) => Select.WhereGoods_id(Goods_id).Limit(limit).ToList();
		public static SelectBuild SelectByGoods_id(params int?[] Goods_id) => Select.WhereGoods_id(Goods_id);
		public static List<Goods_tagInfo> GetItemsByTag_id(params int?[] Tag_id) => Select.WhereTag_id(Tag_id).ToList();
		public static List<Goods_tagInfo> GetItemsByTag_id(int?[] Tag_id, int limit) => Select.WhereTag_id(Tag_id).Limit(limit).ToList();
		public static SelectBuild SelectByTag_id(params int?[] Tag_id) => Select.WhereTag_id(Tag_id);

		#region async
		async public static Task<List<Goods_tagInfo>> DeleteByTag_idAsync(int Tag_id) {
			var items = await dal.DeleteByTag_idAsync(Tag_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return items;
		}
		async public static Task<List<Goods_tagInfo>> DeleteByGoods_idAsync(int Goods_id) {
			var items = await dal.DeleteByGoods_idAsync(Goods_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(items);
			return items;
		}
		async public static Task<Goods_tagInfo> DeleteAsync(int Goods_id, int Tag_id) {
			var item = await dal.DeleteAsync(Goods_id, Tag_id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<Goods_tagInfo> GetItemAsync(int Goods_id, int Tag_id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL_Goods_tag_", Goods_id, "_,_", Tag_id), itemCacheTimeout, () => Select.WhereGoods_id(Goods_id).WhereTag_id(Tag_id).ToOneAsync());
		public static Task<int> UpdateAsync(Goods_tagInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(Goods_tagInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<Goods_tagInfo> InsertAsync(int? Goods_id, int? Tag_id) {
			return InsertAsync(new Goods_tagInfo {
				Goods_id = Goods_id, 
				Tag_id = Tag_id});
		}
		async public static Task<Goods_tagInfo> InsertAsync(Goods_tagInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<Goods_tagInfo>> InsertAsync(IEnumerable<Goods_tagInfo> items) {
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		internal static Task RemoveCacheAsync(Goods_tagInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<Goods_tagInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Goods_tag_", item.Goods_id, "_,_", item.Tag_id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<Goods_tagInfo>> GetItemsAsync() => Select.ToListAsync();
		public static Task<List<Goods_tagInfo>> GetItemsByGoods_idAsync(params int?[] Goods_id) => Select.WhereGoods_id(Goods_id).ToListAsync();
		public static Task<List<Goods_tagInfo>> GetItemsByGoods_idAsync(int?[] Goods_id, int limit) => Select.WhereGoods_id(Goods_id).Limit(limit).ToListAsync();
		public static Task<List<Goods_tagInfo>> GetItemsByTag_idAsync(params int?[] Tag_id) => Select.WhereTag_id(Tag_id).ToListAsync();
		public static Task<List<Goods_tagInfo>> GetItemsByTag_idAsync(int?[] Tag_id, int limit) => Select.WhereTag_id(Tag_id).Limit(limit).ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<Goods_tagInfo, SelectBuild> {
			public SelectBuild WhereGoods_id(params int?[] Goods_id) => this.Where1Or(@"a.[goods_id] = {0}", Goods_id);
			public SelectBuild WhereGoods_id(Goods.SelectBuild select, bool isNotIn = false) => this.Where($@"a.[goods_id] {(isNotIn ? "NOT IN" : "IN")} ({select.ToString(@"[id]")})");
			public SelectBuild WhereTag_id(params int?[] Tag_id) => this.Where1Or(@"a.[tag_id] = {0}", Tag_id);
			public SelectBuild WhereTag_id(Tag.SelectBuild select, bool isNotIn = false) => this.Where($@"a.[tag_id] {(isNotIn ? "NOT IN" : "IN")} ({select.ToString(@"[id]")})");
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}