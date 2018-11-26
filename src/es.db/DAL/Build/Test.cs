using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using es.Model;

namespace es.DAL {

	public partial class Test : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[test]";
			internal static readonly string Field = "a.[id], a.[F_ShortCode]";
			internal static readonly string Sort = "a.[id]";
			internal static readonly string Delete = "DELETE FROM [dbo].[test] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[id], [F_ShortCode]";
			internal static readonly string InsertValues = "@id, @F_ShortCode";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[test](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(TestInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Id }, 
				new SqlParameter { ParameterName = "@F_ShortCode", SqlDbType = SqlDbType.Int, Size = 4, Value = item.F_ShortCode }
			};
		}
		public TestInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as TestInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			TestInfo item = new TestInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 1; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.F_ShortCode = dr.GetInt32(dataIndex);
			return item;
		}
		private void CopyItemAllField(TestInfo item, TestInfo newitem) {
			item.Id = newitem.Id;
			item.F_ShortCode = newitem.F_ShortCode;
		}
		#endregion

		public TestInfo Delete(int? Id) {
			TestInfo item = null;
			SqlHelper.ExecuteReader(dr => { item = BLL.Test.dal.GetItem(dr); }, string.Concat(TSQL.Delete, @"[id] = @id"),
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}

		public SqlUpdateBuild Update(TestInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<TestInfo> { item }, false);
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("id") == false) sub.SetId(item.Id);
			if (ignore.ContainsKey("F_ShortCode") == false) sub.SetF_ShortCode(item.F_ShortCode);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<TestInfo> _dataSource;
			protected bool _isRefershDataSource;
			protected Dictionary<string, TestInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			protected Dictionary<string, Action<TestInfo, TestInfo>> _setAs = new Dictionary<string, Action<TestInfo, TestInfo>>();
			public SqlUpdateBuild(List<TestInfo> dataSource, bool isRefershDataSource) {
				_dataSource = dataSource;
				_isRefershDataSource = isRefershDataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"[id] IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 es.DAL.Test.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " OUTPUT ", TSQL.Field.Replace(@"a.[", @"INSERTED.["), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
					BLL.Test.RemoveCache(_dataSource);
					return affrows;
				}
				var newitems = new List<TestInfo>();
				SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Test.dal.GetItem(dr)); }, sql, _parameters.ToArray());
				BLL.Test.RemoveCache(_dataSource.Concat(newitems));
				foreach (var newitem in newitems) {
					if (_itemsDic.TryGetValue($"{newitem.Id}", out var olditem)) foreach (var a in _setAs.Values) a(olditem, newitem);
					else {
						_dataSource.Add(newitem);
						_itemsDic.Add($"{newitem.Id}", newitem);
					}
				}
				return newitems.Count;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
					await BLL.Test.RemoveCacheAsync(_dataSource);
					return affrows;
				}
				var newitems = new List<TestInfo>();
				await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Test.dal.GetItemAsync(dr)); }, sql, _parameters.ToArray());
				await BLL.Test.RemoveCacheAsync(_dataSource);
				foreach (var newitem in newitems) {
					if (_itemsDic.TryGetValue($"{newitem.Id}", out var olditem)) foreach (var a in _setAs.Values) a(olditem, newitem);
					else {
						_dataSource.Add(newitem);
						_itemsDic.Add($"{newitem.Id}", newitem);
					}
				}
				return newitems.Count;
			}
			public SqlUpdateBuild Where(string filterFormat, params object[] values) {
				if (!string.IsNullOrEmpty(_where)) _where = string.Concat(_where, " AND ");
				_where = string.Concat(_where, "(", SqlHelper.Addslashes(filterFormat, values), ")");
				return this;
			}
			public SqlUpdateBuild WhereExists<T>(SelectBuild<T> select) {
				return this.Where($"EXISTS({select.ToString("1")})");
			}
			public SqlUpdateBuild WhereNotExists<T>(SelectBuild<T> select) {
				return this.Where($"NOT EXISTS({select.ToString("1")})");
			}

			public SqlUpdateBuild Set(string field, string value, params SqlParameter[] parms) {
				if (value.IndexOf('\'') != -1) throw new Exception("es.DAL.Test.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetId(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Id") == false) _setAs.Add("Id", (olditem, newitem) => olditem.Id = newitem.Id);
				return this.Set("[id]", string.Concat("@id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetIdIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Id") == false) _setAs.Add("Id", (olditem, newitem) => olditem.Id = newitem.Id);
				return this.Set("[id]", string.Concat("[id] + @id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetF_ShortCode(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_ShortCode") == false) _setAs.Add("F_ShortCode", (olditem, newitem) => olditem.F_ShortCode = newitem.F_ShortCode);
				return this.Set("[F_ShortCode]", string.Concat("@F_ShortCode_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_ShortCode_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetF_ShortCodeIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_ShortCode") == false) _setAs.Add("F_ShortCode", (olditem, newitem) => olditem.F_ShortCode = newitem.F_ShortCode);
				return this.Set("[F_ShortCode]", string.Concat("[F_ShortCode] + @F_ShortCode_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_ShortCode_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
		}
		#endregion

		public TestInfo Insert(TestInfo item) {
			TestInfo newitem = null;
			SqlHelper.ExecuteReader(dr => { newitem = GetItem(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		public List<TestInfo> Insert(IEnumerable<TestInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<TestInfo>();
			List<TestInfo> newitems = new List<TestInfo>();
			SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Test.dal.GetItem(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		public (string sql, SqlParameter[] parms) InsertMakeParam(IEnumerable<TestInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new SqlParameter[itemsArr.Length * 2];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 2 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<TestInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as TestInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			TestInfo item = new TestInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = await dr.GetFieldValueAsync<int>(dataIndex); if (item.Id == null) { dataIndex += 1; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_ShortCode = await dr.GetFieldValueAsync<int>(dataIndex);
			return (item, dataIndex);
		}
		async public Task<TestInfo> DeleteAsync(int? Id) {
			TestInfo item = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { item = await BLL.Test.dal.GetItemAsync(dr); }, string.Concat(TSQL.Delete, @"[id] = @id"),
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}
		async public Task<TestInfo> InsertAsync(TestInfo item) {
			TestInfo newitem = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { newitem = await GetItemAsync(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		async public Task<List<TestInfo>> InsertAsync(IEnumerable<TestInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<TestInfo>();
			List<TestInfo> newitems = new List<TestInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Test.dal.GetItemAsync(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		#endregion
	}
}