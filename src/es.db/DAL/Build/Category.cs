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

	public partial class Category : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[category]";
			internal static readonly string Field = "a.[id], a.[parent_id], a.[create_time], a.[name]";
			internal static readonly string Sort = "a.[id]";
			internal static readonly string Delete = "DELETE FROM [dbo].[category] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[parent_id], [create_time], [name]";
			internal static readonly string InsertValues = "@parent_id, @create_time, @name";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[category](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(CategoryInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Id }, 
				new SqlParameter { ParameterName = "@parent_id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Parent_id }, 
				new SqlParameter { ParameterName = "@create_time", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Create_time }, 
				new SqlParameter { ParameterName = "@name", SqlDbType = SqlDbType.NVarChar, Size = 128, Value = item.Name }
			};
		}
		public CategoryInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as CategoryInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			CategoryInfo item = new CategoryInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 3; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Parent_id = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Create_time = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			return item;
		}
		private void CopyItemAllField(CategoryInfo item, CategoryInfo newitem) {
			item.Id = newitem.Id;
			item.Parent_id = newitem.Parent_id;
			item.Create_time = newitem.Create_time;
			item.Name = newitem.Name;
		}
		#endregion

		public CategoryInfo Delete(int? Id) {
			CategoryInfo item = null;
			SqlHelper.ExecuteReader(dr => { item = BLL.Category.dal.GetItem(dr); }, string.Concat(TSQL.Delete, @"[id] = @id"),
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}
		public List<CategoryInfo> DeleteByParent_id(int? Parent_id) {
			var items = new List<CategoryInfo>();
			SqlHelper.ExecuteReader(dr => { items.Add(BLL.Category.dal.GetItem(dr)); }, string.Concat(TSQL.Delete, @"[parent_id] = @parent_id"),
				new SqlParameter { ParameterName = "@parent_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Parent_id });
			return items;
		}

		public SqlUpdateBuild Update(CategoryInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<CategoryInfo> { item }, false);
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("parent_id") == false) sub.SetParent_id(item.Parent_id);
			if (ignore.ContainsKey("create_time") == false) sub.SetCreate_time(item.Create_time);
			if (ignore.ContainsKey("name") == false) sub.SetName(item.Name);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<CategoryInfo> _dataSource;
			protected bool _isRefershDataSource;
			protected Dictionary<string, CategoryInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			protected Dictionary<string, Action<CategoryInfo, CategoryInfo>> _setAs = new Dictionary<string, Action<CategoryInfo, CategoryInfo>>();
			public SqlUpdateBuild(List<CategoryInfo> dataSource, bool isRefershDataSource) {
				_dataSource = dataSource;
				_isRefershDataSource = isRefershDataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"[id] IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 es.DAL.Category.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " OUTPUT ", TSQL.Field.Replace(@"a.[", @"INSERTED.["), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
					BLL.Category.RemoveCache(_dataSource);
					return affrows;
				}
				var newitems = new List<CategoryInfo>();
				SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Category.dal.GetItem(dr)); }, sql, _parameters.ToArray());
				BLL.Category.RemoveCache(_dataSource.Concat(newitems));
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
					await BLL.Category.RemoveCacheAsync(_dataSource);
					return affrows;
				}
				var newitems = new List<CategoryInfo>();
				await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Category.dal.GetItemAsync(dr)); }, sql, _parameters.ToArray());
				await BLL.Category.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("es.DAL.Category.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetParent_id(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Parent_id") == false) _setAs.Add("Parent_id", (olditem, newitem) => olditem.Parent_id = newitem.Parent_id);
				return this.Set("[parent_id]", string.Concat("@parent_id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@parent_id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetCreate_time(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("Create_time") == false) _setAs.Add("Create_time", (olditem, newitem) => olditem.Create_time = newitem.Create_time);
				return this.Set("[create_time]", string.Concat("@create_time_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@create_time_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetName(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Name") == false) _setAs.Add("Name", (olditem, newitem) => olditem.Name = newitem.Name);
				return this.Set("[name]", string.Concat("@name_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@name_", _parameters.Count), SqlDbType = SqlDbType.NVarChar, Size = 128, Value = value });
			}
		}
		#endregion

		public CategoryInfo Insert(CategoryInfo item) {
			CategoryInfo newitem = null;
			SqlHelper.ExecuteReader(dr => { newitem = GetItem(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		public List<CategoryInfo> Insert(IEnumerable<CategoryInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<CategoryInfo>();
			List<CategoryInfo> newitems = new List<CategoryInfo>();
			SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Category.dal.GetItem(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		public (string sql, SqlParameter[] parms) InsertMakeParam(IEnumerable<CategoryInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new SqlParameter[itemsArr.Length * 3];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 3 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<CategoryInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as CategoryInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			CategoryInfo item = new CategoryInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = await dr.GetFieldValueAsync<int>(dataIndex); if (item.Id == null) { dataIndex += 3; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Parent_id = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Create_time = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = await dr.GetFieldValueAsync<string>(dataIndex);
			return (item, dataIndex);
		}
		async public Task<CategoryInfo> DeleteAsync(int? Id) {
			CategoryInfo item = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { item = await BLL.Category.dal.GetItemAsync(dr); }, string.Concat(TSQL.Delete, @"[id] = @id"),
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}
		async public Task<List<CategoryInfo>> DeleteByParent_idAsync(int? Parent_id) {
			var items = new List<CategoryInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { items.Add(await BLL.Category.dal.GetItemAsync(dr)); }, string.Concat(TSQL.Delete, @"[parent_id] = @parent_id"),
				new SqlParameter { ParameterName = "@parent_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Parent_id });
			return items;
		}
		async public Task<CategoryInfo> InsertAsync(CategoryInfo item) {
			CategoryInfo newitem = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { newitem = await GetItemAsync(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		async public Task<List<CategoryInfo>> InsertAsync(IEnumerable<CategoryInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<CategoryInfo>();
			List<CategoryInfo> newitems = new List<CategoryInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Category.dal.GetItemAsync(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		#endregion
	}
}