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

	public partial class V_testddd {

		internal static readonly es.DAL.V_testddd dal = new es.DAL.V_testddd();
		internal static readonly int itemCacheTimeout;

		static V_testddd() {
			if (!int.TryParse(SqlHelper.CacheStrategy["Timeout_V_testddd"], out itemCacheTimeout))
				int.TryParse(SqlHelper.CacheStrategy["Timeout"], out itemCacheTimeout);
		}
		public static List<V_testdddInfo> GetItems() => Select.ToList();
		public static V_testdddSelectBuild Select => new V_testdddSelectBuild(dal);
		public static V_testdddSelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		public static Task<List<V_testdddInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion
	}
	public partial class V_testdddSelectBuild : SelectBuild<V_testdddInfo, V_testdddSelectBuild> {
		public V_testdddSelectBuild WhereCategory_id(params int?[] Category_id) => this.Where1Or(@"a.[category_id] = {0}", Category_id);
		public V_testdddSelectBuild WhereCategory_idRange(int? begin) => base.Where(@"a.[category_id] >= {0}", begin);
		public V_testdddSelectBuild WhereCategory_idRange(int? begin, int? end) => end == null ? this.WhereCategory_idRange(begin) : base.Where(@"a.[category_id] between {0} and {1}", begin, end);
		public V_testdddSelectBuild WhereContent(params string[] Content) => this.Where1Or(@"a.[content] = {0}", Content);
		public V_testdddSelectBuild WhereContentLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[content] {opt} {{0}}", pattern);
		}
		public V_testdddSelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where(@"a.[create_time] >= {0}", begin);
		public V_testdddSelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereCreate_timeRange(begin) : base.Where(@"a.[create_time] between {0} and {1}", begin, end);
		public V_testdddSelectBuild WhereId(params int?[] Id) => this.Where1Or(@"a.[id] = {0}", Id);
		public V_testdddSelectBuild WhereIdRange(int? begin) => base.Where(@"a.[id] >= {0}", begin);
		public V_testdddSelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[id] between {0} and {1}", begin, end);
		public V_testdddSelectBuild WhereImgsLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[imgs] {opt} {{0}}", pattern);
		}
		public V_testdddSelectBuild WhereName(params string[] Name) => this.Where1Or(@"a.[name] = {0}", Name);
		public V_testdddSelectBuild WhereNameLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[name] {opt} {{0}}", pattern);
		}
		public V_testdddSelectBuild WhereStock(params int?[] Stock) => this.Where1Or(@"a.[stock] = {0}", Stock);
		public V_testdddSelectBuild WhereStockRange(int? begin) => base.Where(@"a.[stock] >= {0}", begin);
		public V_testdddSelectBuild WhereStockRange(int? begin, int? end) => end == null ? this.WhereStockRange(begin) : base.Where(@"a.[stock] between {0} and {1}", begin, end);
		public V_testdddSelectBuild WhereTitle(params string[] Title) => this.Where1Or(@"a.[title] = {0}", Title);
		public V_testdddSelectBuild WhereTitleLike(string pattern, bool isNotLike = false) {
			var opt = isNotLike ? "LIKE" : "NOT LIKE";
			return this.Where($@"a.[title] {opt} {{0}}", pattern);
		}
		public V_testdddSelectBuild WhereUpdate_timeRange(DateTime? begin) => base.Where(@"a.[update_time] >= {0}", begin);
		public V_testdddSelectBuild WhereUpdate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereUpdate_timeRange(begin) : base.Where(@"a.[update_time] between {0} and {1}", begin, end);
		public V_testdddSelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
	}
}