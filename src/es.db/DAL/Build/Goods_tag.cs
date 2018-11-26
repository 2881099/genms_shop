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

	public partial class Goods_tag : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[goods_tag]";
			internal static readonly string Field = "a.[goods_id], a.[tag_id]";
			internal static readonly string Sort = "a.[goods_id], a.[tag_id]";
			internal static readonly string Delete = "DELETE FROM [dbo].[goods_tag] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[goods_id], [tag_id]";
			internal static readonly string InsertValues = "@goods_id, @tag_id";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[goods_tag](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(Goods_tagInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@goods_id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Goods_id }, 
				new SqlParameter { ParameterName = "@tag_id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Tag_id }
			};
		}
		public Goods_tagInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Goods_tagInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			Goods_tagInfo item = new Goods_tagInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Goods_id = dr.GetInt32(dataIndex); if (item.Goods_id == null) { dataIndex += 1; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Tag_id = dr.GetInt32(dataIndex); if (item.Tag_id == null) { dataIndex += 0; return null; }
			return item;
		}
		private void CopyItemAllField(Goods_tagInfo item, Goods_tagInfo newitem) {
			item.Goods_id = newitem.Goods_id;
			item.Tag_id = newitem.Tag_id;
		}
		#endregion

		public Goods_tagInfo Delete(int? Goods_id, int? Tag_id) {
			Goods_tagInfo item = null;
			SqlHelper.ExecuteReader(dr => { item = BLL.Goods_tag.dal.GetItem(dr); }, string.Concat(TSQL.Delete, @"[goods_id] = @goods_id AND [tag_id] = @tag_id"),
				new SqlParameter { ParameterName = "@goods_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Goods_id }, 
				new SqlParameter { ParameterName = "@tag_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Tag_id });
			return item;
		}
		public List<Goods_tagInfo> DeleteByGoods_id(int? Goods_id) {
			var items = new List<Goods_tagInfo>();
			SqlHelper.ExecuteReader(dr => { items.Add(BLL.Goods_tag.dal.GetItem(dr)); }, string.Concat(TSQL.Delete, @"[goods_id] = @goods_id"),
				new SqlParameter { ParameterName = "@goods_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Goods_id });
			return items;
		}
		public List<Goods_tagInfo> DeleteByTag_id(int? Tag_id) {
			var items = new List<Goods_tagInfo>();
			SqlHelper.ExecuteReader(dr => { items.Add(BLL.Goods_tag.dal.GetItem(dr)); }, string.Concat(TSQL.Delete, @"[tag_id] = @tag_id"),
				new SqlParameter { ParameterName = "@tag_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Tag_id });
			return items;
		}

		public SqlUpdateBuild Update(Goods_tagInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Goods_tagInfo> { item }, false);
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("goods_id") == false) sub.SetGoods_id(item.Goods_id);
			if (ignore.ContainsKey("tag_id") == false) sub.SetTag_id(item.Tag_id);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Goods_tagInfo> _dataSource;
			protected bool _isRefershDataSource;
			protected Dictionary<string, Goods_tagInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			protected Dictionary<string, Action<Goods_tagInfo, Goods_tagInfo>> _setAs = new Dictionary<string, Action<Goods_tagInfo, Goods_tagInfo>>();
			public SqlUpdateBuild(List<Goods_tagInfo> dataSource, bool isRefershDataSource) {
				_dataSource = dataSource;
				_isRefershDataSource = isRefershDataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Goods_id}_{a.Tag_id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"[goods_id] IN ({0})", _dataSource.Select(a => a.Goods_id).Distinct())
						.Where(@"[tag_id] IN ({0})", _dataSource.Select(a => a.Tag_id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 es.DAL.Goods_tag.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " OUTPUT ", TSQL.Field.Replace(@"a.[", @"INSERTED.["), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
					BLL.Goods_tag.RemoveCache(_dataSource);
					return affrows;
				}
				var newitems = new List<Goods_tagInfo>();
				SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Goods_tag.dal.GetItem(dr)); }, sql, _parameters.ToArray());
				BLL.Goods_tag.RemoveCache(_dataSource.Concat(newitems));
				foreach (var newitem in newitems) {
					if (_itemsDic.TryGetValue($"{newitem.Goods_id}_{newitem.Tag_id}", out var olditem)) foreach (var a in _setAs.Values) a(olditem, newitem);
					else {
						_dataSource.Add(newitem);
						_itemsDic.Add($"{newitem.Goods_id}_{newitem.Tag_id}", newitem);
					}
				}
				return newitems.Count;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
					await BLL.Goods_tag.RemoveCacheAsync(_dataSource);
					return affrows;
				}
				var newitems = new List<Goods_tagInfo>();
				await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Goods_tag.dal.GetItemAsync(dr)); }, sql, _parameters.ToArray());
				await BLL.Goods_tag.RemoveCacheAsync(_dataSource);
				foreach (var newitem in newitems) {
					if (_itemsDic.TryGetValue($"{newitem.Goods_id}_{newitem.Tag_id}", out var olditem)) foreach (var a in _setAs.Values) a(olditem, newitem);
					else {
						_dataSource.Add(newitem);
						_itemsDic.Add($"{newitem.Goods_id}_{newitem.Tag_id}", newitem);
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
				if (value.IndexOf('\'') != -1) throw new Exception("es.DAL.Goods_tag.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetGoods_id(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Goods_id") == false) _setAs.Add("Goods_id", (olditem, newitem) => olditem.Goods_id = newitem.Goods_id);
				return this.Set("[goods_id]", string.Concat("@goods_id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@goods_id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTag_id(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Tag_id") == false) _setAs.Add("Tag_id", (olditem, newitem) => olditem.Tag_id = newitem.Tag_id);
				return this.Set("[tag_id]", string.Concat("@tag_id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@tag_id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
		}
		#endregion

		public Goods_tagInfo Insert(Goods_tagInfo item) {
			Goods_tagInfo newitem = null;
			SqlHelper.ExecuteReader(dr => { newitem = GetItem(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		public List<Goods_tagInfo> Insert(IEnumerable<Goods_tagInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<Goods_tagInfo>();
			List<Goods_tagInfo> newitems = new List<Goods_tagInfo>();
			SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Goods_tag.dal.GetItem(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		public (string sql, SqlParameter[] parms) InsertMakeParam(IEnumerable<Goods_tagInfo> items) {
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
		async public Task<Goods_tagInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Goods_tagInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			Goods_tagInfo item = new Goods_tagInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Goods_id = await dr.GetFieldValueAsync<int>(dataIndex); if (item.Goods_id == null) { dataIndex += 1; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Tag_id = await dr.GetFieldValueAsync<int>(dataIndex); if (item.Tag_id == null) { dataIndex += 0; return (null, dataIndex); }
			return (item, dataIndex);
		}
		async public Task<Goods_tagInfo> DeleteAsync(int? Goods_id, int? Tag_id) {
			Goods_tagInfo item = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { item = await BLL.Goods_tag.dal.GetItemAsync(dr); }, string.Concat(TSQL.Delete, @"[goods_id] = @goods_id AND [tag_id] = @tag_id"),
				new SqlParameter { ParameterName = "@goods_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Goods_id }, 
				new SqlParameter { ParameterName = "@tag_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Tag_id });
			return item;
		}
		async public Task<List<Goods_tagInfo>> DeleteByGoods_idAsync(int? Goods_id) {
			var items = new List<Goods_tagInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { items.Add(await BLL.Goods_tag.dal.GetItemAsync(dr)); }, string.Concat(TSQL.Delete, @"[goods_id] = @goods_id"),
				new SqlParameter { ParameterName = "@goods_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Goods_id });
			return items;
		}
		async public Task<List<Goods_tagInfo>> DeleteByTag_idAsync(int? Tag_id) {
			var items = new List<Goods_tagInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { items.Add(await BLL.Goods_tag.dal.GetItemAsync(dr)); }, string.Concat(TSQL.Delete, @"[tag_id] = @tag_id"),
				new SqlParameter { ParameterName = "@tag_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Tag_id });
			return items;
		}
		async public Task<Goods_tagInfo> InsertAsync(Goods_tagInfo item) {
			Goods_tagInfo newitem = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { newitem = await GetItemAsync(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		async public Task<List<Goods_tagInfo>> InsertAsync(IEnumerable<Goods_tagInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<Goods_tagInfo>();
			List<Goods_tagInfo> newitems = new List<Goods_tagInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Goods_tag.dal.GetItemAsync(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		#endregion
	}
}