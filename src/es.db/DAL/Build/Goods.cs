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

	public partial class Goods : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[goods]";
			internal static readonly string Field = "a.[id], a.[category_id], a.[content], a.[create_time], a.[imgs], a.[stock], a.[title], a.[update_time]";
			internal static readonly string Sort = "a.[id]";
			internal static readonly string Delete = "DELETE FROM [dbo].[goods] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[category_id], [content], [create_time], [imgs], [stock], [title], [update_time]";
			internal static readonly string InsertValues = "@category_id, @content, @create_time, @imgs, @stock, @title, @update_time";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[goods](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(GoodsInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Id }, 
				new SqlParameter { ParameterName = "@category_id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Category_id }, 
				new SqlParameter { ParameterName = "@content", SqlDbType = SqlDbType.NVarChar, Size = -1, Value = item.Content }, 
				new SqlParameter { ParameterName = "@create_time", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Create_time }, 
				new SqlParameter { ParameterName = "@imgs", SqlDbType = SqlDbType.NVarChar, Size = 1024, Value = item.Imgs }, 
				new SqlParameter { ParameterName = "@stock", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Stock }, 
				new SqlParameter { ParameterName = "@title", SqlDbType = SqlDbType.NVarChar, Size = 256, Value = item.Title }, 
				new SqlParameter { ParameterName = "@update_time", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Update_time }
			};
		}
		public GoodsInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as GoodsInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			GoodsInfo item = new GoodsInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 7; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.Category_id = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Content = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Create_time = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Imgs = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Stock = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Title = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Update_time = dr.GetDateTime(dataIndex);
			return item;
		}
		private void CopyItemAllField(GoodsInfo item, GoodsInfo newitem) {
			item.Id = newitem.Id;
			item.Category_id = newitem.Category_id;
			item.Content = newitem.Content;
			item.Create_time = newitem.Create_time;
			item.Imgs = newitem.Imgs;
			item.Stock = newitem.Stock;
			item.Title = newitem.Title;
			item.Update_time = newitem.Update_time;
		}
		#endregion

		public GoodsInfo Delete(int? Id) {
			GoodsInfo item = null;
			SqlHelper.ExecuteReader(dr => { item = BLL.Goods.dal.GetItem(dr); }, string.Concat(TSQL.Delete, @"[id] = @id"),
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}
		public List<GoodsInfo> DeleteByCategory_id(int? Category_id) {
			var items = new List<GoodsInfo>();
			SqlHelper.ExecuteReader(dr => { items.Add(BLL.Goods.dal.GetItem(dr)); }, string.Concat(TSQL.Delete, @"[category_id] = @category_id"),
				new SqlParameter { ParameterName = "@category_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Category_id });
			return items;
		}

		public SqlUpdateBuild Update(GoodsInfo item) {
			return new SqlUpdateBuild(new List<GoodsInfo> { item }, false)
				.SetCategory_id(item.Category_id)
				.SetContent(item.Content)
				.SetCreate_time(item.Create_time)
				.SetImgs(item.Imgs)
				.SetStock(item.Stock)
				.SetTitle(item.Title)
				.SetUpdate_time(item.Update_time);
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<GoodsInfo> _dataSource;
			protected bool _isRefershDataSource;
			protected Dictionary<string, GoodsInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			protected Dictionary<string, Action<GoodsInfo, GoodsInfo>> _setAs = new Dictionary<string, Action<GoodsInfo, GoodsInfo>>();
			public SqlUpdateBuild(List<GoodsInfo> dataSource, bool isRefershDataSource) {
				_dataSource = dataSource;
				_isRefershDataSource = isRefershDataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"[id] IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 es.DAL.Goods.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " OUTPUT ", TSQL.Field.Replace(@"a.[", @"INSERTED.["), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
					BLL.Goods.RemoveCache(_dataSource);
					return affrows;
				}
				var newitems = new List<GoodsInfo>();
				SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Goods.dal.GetItem(dr)); }, sql, _parameters.ToArray());
				BLL.Goods.RemoveCache(_dataSource.Concat(newitems));
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
					await BLL.Goods.RemoveCacheAsync(_dataSource);
					return affrows;
				}
				var newitems = new List<GoodsInfo>();
				await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Goods.dal.GetItemAsync(dr)); }, sql, _parameters.ToArray());
				await BLL.Goods.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("es.DAL.Goods.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetCategory_id(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Category_id") == false) _setAs.Add("Category_id", (olditem, newitem) => olditem.Category_id = newitem.Category_id);
				return this.Set("[category_id]", string.Concat("@category_id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@category_id_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
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
			public SqlUpdateBuild SetImgs(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Imgs") == false) _setAs.Add("Imgs", (olditem, newitem) => olditem.Imgs = newitem.Imgs);
				return this.Set("[imgs]", string.Concat("@imgs_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@imgs_", _parameters.Count), SqlDbType = SqlDbType.NVarChar, Size = 1024, Value = value });
			}
			public SqlUpdateBuild SetStock(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Stock") == false) _setAs.Add("Stock", (olditem, newitem) => olditem.Stock = newitem.Stock);
				return this.Set("[stock]", string.Concat("@stock_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@stock_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetStockIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("Stock") == false) _setAs.Add("Stock", (olditem, newitem) => olditem.Stock = newitem.Stock);
				return this.Set("[stock]", string.Concat("[stock] + @stock_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@stock_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTitle(string value) {
				if (_dataSource != null && _setAs.ContainsKey("Title") == false) _setAs.Add("Title", (olditem, newitem) => olditem.Title = newitem.Title);
				return this.Set("[title]", string.Concat("@title_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@title_", _parameters.Count), SqlDbType = SqlDbType.NVarChar, Size = 256, Value = value });
			}
			public SqlUpdateBuild SetUpdate_time(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("Update_time") == false) _setAs.Add("Update_time", (olditem, newitem) => olditem.Update_time = newitem.Update_time);
				return this.Set("[update_time]", string.Concat("@update_time_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@update_time_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
		}
		#endregion

		public GoodsInfo Insert(GoodsInfo item) {
			GoodsInfo newitem = null;
			SqlHelper.ExecuteReader(dr => { newitem = GetItem(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		public List<GoodsInfo> Insert(IEnumerable<GoodsInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<GoodsInfo>();
			List<GoodsInfo> newitems = new List<GoodsInfo>();
			SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Goods.dal.GetItem(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		public (string sql, SqlParameter[] parms) InsertMakeParam(IEnumerable<GoodsInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new SqlParameter[itemsArr.Length * 7];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 7 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<GoodsInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as GoodsInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			GoodsInfo item = new GoodsInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = await dr.GetFieldValueAsync<int>(dataIndex); if (item.Id == null) { dataIndex += 7; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Category_id = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Content = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Create_time = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Imgs = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Stock = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Title = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Update_time = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			return (item, dataIndex);
		}
		async public Task<GoodsInfo> DeleteAsync(int? Id) {
			GoodsInfo item = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { item = await BLL.Goods.dal.GetItemAsync(dr); }, string.Concat(TSQL.Delete, @"[id] = @id"),
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}
		async public Task<List<GoodsInfo>> DeleteByCategory_idAsync(int? Category_id) {
			var items = new List<GoodsInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { items.Add(await BLL.Goods.dal.GetItemAsync(dr)); }, string.Concat(TSQL.Delete, @"[category_id] = @category_id"),
				new SqlParameter { ParameterName = "@category_id", SqlDbType = SqlDbType.Int, Size = 4, Value = Category_id });
			return items;
		}
		async public Task<GoodsInfo> InsertAsync(GoodsInfo item) {
			GoodsInfo newitem = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { newitem = await GetItemAsync(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		async public Task<List<GoodsInfo>> InsertAsync(IEnumerable<GoodsInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<GoodsInfo>();
			List<GoodsInfo> newitems = new List<GoodsInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Goods.dal.GetItemAsync(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		#endregion
	}
}