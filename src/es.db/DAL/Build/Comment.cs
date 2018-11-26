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

	public partial class Comment : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[comment]";
			internal static readonly string Field = "a.[id], a.[goods_id], a.[content], a.[create_time], a.[nickname], a.[update_time]";
			internal static readonly string Sort = "a.[id]";
			internal static readonly string Delete = "DELETE FROM [dbo].[comment] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[goods_id], [content], [create_time], [nickname], [update_time]";
			internal static readonly string InsertValues = "@goods_id, @content, @create_time, @nickname, @update_time";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[comment](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(CommentInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Id }, 
				new SqlParameter { ParameterName = "@goods_id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Goods_id }, 
				new SqlParameter { ParameterName = "@content", SqlDbType = SqlDbType.NVarChar, Size = -1, Value = item.Content }, 
				new SqlParameter { ParameterName = "@create_time", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Create_time }, 
				new SqlParameter { ParameterName = "@nickname", SqlDbType = SqlDbType.NVarChar, Size = 64, Value = item.Nickname }, 
				new SqlParameter { ParameterName = "@update_time", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Update_time }
			};
		}
		public CommentInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as CommentInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			CommentInfo item = new CommentInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 5; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Goods_id = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Content = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Create_time = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Nickname = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Update_time = dr.GetDateTime(dataIndex);
			return item;
		}
		private void CopyItemAllField(CommentInfo item, CommentInfo newitem) {
			item.Id = newitem.Id;
			item.Goods_id = newitem.Goods_id;
			item.Content = newitem.Content;
			item.Create_time = newitem.Create_time;
			item.Nickname = newitem.Nickname;
			item.Update_time = newitem.Update_time;
		}
		#endregion

		public CommentInfo Delete(int? Id) {
			CommentInfo item = null;
			SqlHelper.ExecuteReader(dr => { item = BLL.Comment.dal.GetItem(dr); }, string.Concat(TSQL.Delete, @"[id] = @id"),
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}
		public List<CommentInfo> DeleteByGoods_id(int? Goods_id) {
			var items = new List<CommentInfo>();
			SqlHelper.ExecuteReader(dr => { items.Add(BLL.Comment.dal.GetItem(dr)); }, string.Concat(TSQL.Delete, @"[goods_id] = @goods_id"),
				new SqlParameter { ParameterName = "@goods_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Goods_id });
			return items;
		}

		public SqlUpdateBuild Update(CommentInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<CommentInfo> { item }, false);
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("goods_id") == false) sub.SetGoods_id(item.Goods_id);
			if (ignore.ContainsKey("content") == false) sub.SetContent(item.Content);
			if (ignore.ContainsKey("create_time") == false) sub.SetCreate_time(item.Create_time);
			if (ignore.ContainsKey("nickname") == false) sub.SetNickname(item.Nickname);
			if (ignore.ContainsKey("update_time") == false) sub.SetUpdate_time(item.Update_time);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<CommentInfo> _dataSource;
			protected bool _isRefershDataSource;
			protected Dictionary<string, CommentInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			protected Dictionary<string, Action<CommentInfo, CommentInfo>> _setAs = new Dictionary<string, Action<CommentInfo, CommentInfo>>();
			public SqlUpdateBuild(List<CommentInfo> dataSource, bool isRefershDataSource) {
				_dataSource = dataSource;
				_isRefershDataSource = isRefershDataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"[id] IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 es.DAL.Comment.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " OUTPUT ", TSQL.Field.Replace(@"a.[", @"INSERTED.["), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
					BLL.Comment.RemoveCache(_dataSource);
					return affrows;
				}
				var newitems = new List<CommentInfo>();
				SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Comment.dal.GetItem(dr)); }, sql, _parameters.ToArray());
				BLL.Comment.RemoveCache(_dataSource.Concat(newitems));
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
					await BLL.Comment.RemoveCacheAsync(_dataSource);
					return affrows;
				}
				var newitems = new List<CommentInfo>();
				await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Comment.dal.GetItemAsync(dr)); }, sql, _parameters.ToArray());
				await BLL.Comment.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("es.DAL.Comment.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetGoods_id(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Goods_id") == false) _setAs.Add("Goods_id", (olditem, newitem) => olditem.Goods_id = newitem.Goods_id);
				return this.Set("[goods_id]", string.Concat("@goods_id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@goods_id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetContent(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Content") == false) _setAs.Add("Content", (olditem, newitem) => olditem.Content = newitem.Content);
				return this.Set("[content]", string.Concat("@content_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@content_", _parameters.Count), SqlDbType = SqlDbType.NVarChar, Size = -1, Value = value });
			}
			public SqlUpdateBuild SetCreate_time(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("Create_time") == false) _setAs.Add("Create_time", (olditem, newitem) => olditem.Create_time = newitem.Create_time);
				return this.Set("[create_time]", string.Concat("@create_time_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@create_time_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetNickname(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Nickname") == false) _setAs.Add("Nickname", (olditem, newitem) => olditem.Nickname = newitem.Nickname);
				return this.Set("[nickname]", string.Concat("@nickname_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@nickname_", _parameters.Count), SqlDbType = SqlDbType.NVarChar, Size = 64, Value = value });
			}
			public SqlUpdateBuild SetUpdate_time(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("Update_time") == false) _setAs.Add("Update_time", (olditem, newitem) => olditem.Update_time = newitem.Update_time);
				return this.Set("[update_time]", string.Concat("@update_time_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@update_time_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
		}
		#endregion

		public CommentInfo Insert(CommentInfo item) {
			CommentInfo newitem = null;
			SqlHelper.ExecuteReader(dr => { newitem = GetItem(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		public List<CommentInfo> Insert(IEnumerable<CommentInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<CommentInfo>();
			List<CommentInfo> newitems = new List<CommentInfo>();
			SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Comment.dal.GetItem(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		public (string sql, SqlParameter[] parms) InsertMakeParam(IEnumerable<CommentInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new SqlParameter[itemsArr.Length * 5];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 5 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<CommentInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as CommentInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			CommentInfo item = new CommentInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = await dr.GetFieldValueAsync<int>(dataIndex); if (item.Id == null) { dataIndex += 5; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Goods_id = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Content = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Create_time = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Nickname = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Update_time = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			return (item, dataIndex);
		}
		async public Task<CommentInfo> DeleteAsync(int? Id) {
			CommentInfo item = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { item = await BLL.Comment.dal.GetItemAsync(dr); }, string.Concat(TSQL.Delete, @"[id] = @id"),
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}
		async public Task<List<CommentInfo>> DeleteByGoods_idAsync(int? Goods_id) {
			var items = new List<CommentInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { items.Add(await BLL.Comment.dal.GetItemAsync(dr)); }, string.Concat(TSQL.Delete, @"[goods_id] = @goods_id"),
				new SqlParameter { ParameterName = "@goods_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Goods_id });
			return items;
		}
		async public Task<CommentInfo> InsertAsync(CommentInfo item) {
			CommentInfo newitem = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { newitem = await GetItemAsync(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		async public Task<List<CommentInfo>> InsertAsync(IEnumerable<CommentInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<CommentInfo>();
			List<CommentInfo> newitems = new List<CommentInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Comment.dal.GetItemAsync(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		#endregion
	}
}