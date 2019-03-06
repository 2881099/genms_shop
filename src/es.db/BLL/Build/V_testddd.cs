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
		public static SelectBuild Select => new SelectBuild(dal);
		public static SelectBuild SelectAs(string alias = "a") => Select.As(alias);

		#region async
		public static Task<List<V_testdddInfo>> GetItemsAsync() => Select.ToListAsync();
		#endregion

		public partial class SelectBuild : SelectBuild<V_testdddInfo, SelectBuild> {
			public SelectBuild WhereCategory_id(params int?[] Category_id) => this.Where1Or(@"a.[category_id] = {0}", Category_id);
			public SelectBuild WhereCategory_idRange(int? begin) => base.Where(@"a.[category_id] >= {0}", begin);
			public SelectBuild WhereCategory_idRange(int? begin, int? end) => end == null ? this.WhereCategory_idRange(begin) : base.Where(@"a.[category_id] between {0} and {1}", begin, end);
			public SelectBuild WhereContentLike(string pattern, bool isNotLike = false) => this.Where($@"a.[content] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereCreate_timeRange(DateTime? begin) => base.Where(@"a.[create_time] >= {0}", begin);
			public SelectBuild WhereCreate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereCreate_timeRange(begin) : base.Where(@"a.[create_time] between {0} and {1}", begin, end);
			public SelectBuild WhereId(params int?[] Id) => this.Where1Or(@"a.[id] = {0}", Id);
			public SelectBuild WhereIdRange(int? begin) => base.Where(@"a.[id] >= {0}", begin);
			public SelectBuild WhereIdRange(int? begin, int? end) => end == null ? this.WhereIdRange(begin) : base.Where(@"a.[id] between {0} and {1}", begin, end);
			public SelectBuild WhereImgsLike(string pattern, bool isNotLike = false) => this.Where($@"a.[imgs] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereName(params string[] Name) => this.Where1Or(@"a.[name] = {0}", Name);
			public SelectBuild WhereNameLike(string pattern, bool isNotLike = false) => this.Where($@"a.[name] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereStock(params int?[] Stock) => this.Where1Or(@"a.[stock] = {0}", Stock);
			public SelectBuild WhereStockRange(int? begin) => base.Where(@"a.[stock] >= {0}", begin);
			public SelectBuild WhereStockRange(int? begin, int? end) => end == null ? this.WhereStockRange(begin) : base.Where(@"a.[stock] between {0} and {1}", begin, end);
			public SelectBuild WhereTitle(params string[] Title) => this.Where1Or(@"a.[title] = {0}", Title);
			public SelectBuild WhereTitleLike(string pattern, bool isNotLike = false) => this.Where($@"a.[title] {(isNotLike ? "NOT LIKE" : "LIKE")} {{0}}", pattern);
			public SelectBuild WhereUpdate_timeRange(DateTime? begin) => base.Where(@"a.[update_time] >= {0}", begin);
			public SelectBuild WhereUpdate_timeRange(DateTime? begin, DateTime? end) => end == null ? this.WhereUpdate_timeRange(begin) : base.Where(@"a.[update_time] between {0} and {1}", begin, end);
			public SelectBuild(IDAL dal) : base(dal, SqlHelper.Instance) { }
		}
	}
}