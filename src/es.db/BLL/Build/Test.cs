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

	public partial class Test {

		internal static readonly es.DAL.Test dal = new es.DAL.Test();
		internal static readonly int itemCacheTimeout;

		static Test() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_Test"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}

		#region delete, update, insert

		public static TestInfo Delete(int Id) {
			var item = dal.Delete(Id);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}

		#region enum _
		public enum _ {
			Id = 1, 
			F_ShortCode
		}
		#endregion

		public static int Update(TestInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => Update(item, new[] { ignore1, ignore2, ignore3 });
		public static int Update(TestInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQuery();
		public static es.DAL.Test.SqlUpdateBuild UpdateDiy(int Id) => new es.DAL.Test.SqlUpdateBuild(new List<TestInfo> { new TestInfo { Id = Id } }, false);
		public static es.DAL.Test.SqlUpdateBuild UpdateDiy(List<TestInfo> dataSource) => new es.DAL.Test.SqlUpdateBuild(dataSource, true);
		public static es.DAL.Test.SqlUpdateBuild UpdateDiyDangerous => new es.DAL.Test.SqlUpdateBuild();

		public static TestInfo Insert(int? Id, int? F_ShortCode) {
			return Insert(new TestInfo {
				Id = Id, 
				F_ShortCode = F_ShortCode});
		}
		public static TestInfo Insert(TestInfo item) {
			item = dal.Insert(item);
			if (itemCacheTimeout > 0) RemoveCache(item);
			return item;
		}
		public static List<TestInfo> Insert(IEnumerable<TestInfo> items) {
			var newitems = dal.Insert(items);
			if (itemCacheTimeout > 0) RemoveCache(newitems);
			return newitems;
		}
		internal static void RemoveCache(TestInfo item) => RemoveCache(item == null ? null : new [] { item });
		internal static void RemoveCache(IEnumerable<TestInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Test_", item.Id);
			}
			if (SqlHelper.Instance.CurrentThreadTransaction != null) SqlHelper.Instance.PreRemove(keys);
			else SqlHelper.CacheRemove(keys);
		}
		#endregion

		public static TestInfo GetItem(int Id) => SqlHelper.CacheShell(string.Concat("es_BLL_Test_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOne());

		public static List<TestInfo> GetItems() => Select.ToList();
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		async public static Task<TestInfo> DeleteAsync(int Id) {
			var item = await dal.DeleteAsync(Id);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<TestInfo> GetItemAsync(int Id) => await SqlHelper.CacheShellAsync(string.Concat("es_BLL_Test_", Id), itemCacheTimeout, () => Select.WhereId(Id).ToOneAsync());
		public static Task<int> UpdateAsync(TestInfo item, _ ignore1 = 0, _ ignore2 = 0, _ ignore3 = 0) => UpdateAsync(item, new[] { ignore1, ignore2, ignore3 });
		public static Task<int> UpdateAsync(TestInfo item, _[] ignore) => dal.Update(item, ignore?.Where(a => a > 0).Select(a => Enum.GetName(typeof(_), a)).ToArray()).ExecuteNonQueryAsync();

		public static Task<TestInfo> InsertAsync(int? Id, int? F_ShortCode) {
			return InsertAsync(new TestInfo {
				Id = Id, 
				F_ShortCode = F_ShortCode});
		}
		async public static Task<TestInfo> InsertAsync(TestInfo item) {
			item = await dal.InsertAsync(item);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(item);
			return item;
		}
		async public static Task<List<TestInfo>> InsertAsync(IEnumerable<TestInfo> items) {
			var newitems = await dal.InsertAsync(items);
			if (itemCacheTimeout > 0) await RemoveCacheAsync(newitems);
			return newitems;
		}
		internal static Task RemoveCacheAsync(TestInfo item) => RemoveCacheAsync(item == null ? null : new [] { item });
		async internal static Task RemoveCacheAsync(IEnumerable<TestInfo> items) {
			if (itemCacheTimeout <= 0 || items == null || items.Any() == false) return;
			var keys = new string[items.Count() * 1];
			var keysIdx = 0;
			foreach (var item in items) {
				keys[keysIdx++] = string.Concat("es_BLL_Test_", item.Id);
			}
			await SqlHelper.CacheRemoveAsync(keys);
		}

		public static Task<List<TestInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<TestInfo, SelectBuild> {
			public SelectBuild WhereId(params int[] Id) => this.Where1Or(@"a.[id] = {0}", Id);
			public SelectBuild WhereIdRange(int? begin) => base.Where(@"a.[id] >= {0}", begin);
			public SelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[id] between {0} and {1}", begin, end);
			public SelectBuild WhereF_ShortCode(params int?[] F_ShortCode) => this.Where1Or(@"a.[F_ShortCode] = {0}", F_ShortCode);
			public SelectBuild WhereF_ShortCodeRange(int? begin) => base.Where(@"a.[F_ShortCode] >= {0}", begin);
			public SelectBuild WhereF_ShortCodeRange(int? begin, int? end) => end == null ? this.WhereF_ShortCodeRange(begin) : base.Where(@"a.[F_ShortCode] between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}