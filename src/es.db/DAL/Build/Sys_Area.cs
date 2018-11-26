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

	public partial class Sys_Area : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[Sys_Area]";
			internal static readonly string Field = "a.[F_Id], a.[F_CreatorTime], a.[F_CreatorUserId], a.[F_DeleteMark], a.[F_DeleteTime], a.[F_DeleteUserId], a.[F_Description], a.[F_EnabledMark], a.[F_EnCode], a.[F_FullName], a.[F_LastModifyTime], a.[F_LastModifyUserId], a.[F_Layers], a.[F_ParentId], a.[F_SimpleSpelling], a.[F_SortCode]";
			internal static readonly string Sort = "a.[F_Id]";
			internal static readonly string Delete = "DELETE FROM [dbo].[Sys_Area] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[F_Id], [F_CreatorTime], [F_CreatorUserId], [F_DeleteMark], [F_DeleteTime], [F_DeleteUserId], [F_Description], [F_EnabledMark], [F_EnCode], [F_FullName], [F_LastModifyTime], [F_LastModifyUserId], [F_Layers], [F_ParentId], [F_SimpleSpelling], [F_SortCode]";
			internal static readonly string InsertValues = "@F_Id, @F_CreatorTime, @F_CreatorUserId, @F_DeleteMark, @F_DeleteTime, @F_DeleteUserId, @F_Description, @F_EnabledMark, @F_EnCode, @F_FullName, @F_LastModifyTime, @F_LastModifyUserId, @F_Layers, @F_ParentId, @F_SimpleSpelling, @F_SortCode";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[Sys_Area](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(Sys_AreaInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@F_Id", SqlDbType = SqlDbType.VarChar, Size = 50, Value = item.F_Id }, 
				new SqlParameter { ParameterName = "@F_CreatorTime", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.F_CreatorTime }, 
				new SqlParameter { ParameterName = "@F_CreatorUserId", SqlDbType = SqlDbType.VarChar, Size = 50, Value = item.F_CreatorUserId }, 
				new SqlParameter { ParameterName = "@F_DeleteMark", SqlDbType = SqlDbType.Bit, Size = 1, Value = item.F_DeleteMark }, 
				new SqlParameter { ParameterName = "@F_DeleteTime", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.F_DeleteTime }, 
				new SqlParameter { ParameterName = "@F_DeleteUserId", SqlDbType = SqlDbType.VarChar, Size = 50, Value = item.F_DeleteUserId }, 
				new SqlParameter { ParameterName = "@F_Description", SqlDbType = SqlDbType.VarChar, Size = 500, Value = item.F_Description }, 
				new SqlParameter { ParameterName = "@F_EnabledMark", SqlDbType = SqlDbType.Bit, Size = 1, Value = item.F_EnabledMark }, 
				new SqlParameter { ParameterName = "@F_EnCode", SqlDbType = SqlDbType.VarChar, Size = 50, Value = item.F_EnCode }, 
				new SqlParameter { ParameterName = "@F_FullName", SqlDbType = SqlDbType.VarChar, Size = 50, Value = item.F_FullName }, 
				new SqlParameter { ParameterName = "@F_LastModifyTime", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.F_LastModifyTime }, 
				new SqlParameter { ParameterName = "@F_LastModifyUserId", SqlDbType = SqlDbType.VarChar, Size = 50, Value = item.F_LastModifyUserId }, 
				new SqlParameter { ParameterName = "@F_Layers", SqlDbType = SqlDbType.Int, Size = 4, Value = item.F_Layers }, 
				new SqlParameter { ParameterName = "@F_ParentId", SqlDbType = SqlDbType.VarChar, Size = 50, Value = item.F_ParentId }, 
				new SqlParameter { ParameterName = "@F_SimpleSpelling", SqlDbType = SqlDbType.VarChar, Size = 50, Value = item.F_SimpleSpelling }, 
				new SqlParameter { ParameterName = "@F_SortCode", SqlDbType = SqlDbType.Int, Size = 4, Value = item.F_SortCode }
			};
		}
		public Sys_AreaInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Sys_AreaInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			Sys_AreaInfo item = new Sys_AreaInfo();
			if (!dr.IsDBNull(++dataIndex)) item.F_Id = dr.GetString(dataIndex); if (item.F_Id == null) { dataIndex += 15; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.F_CreatorTime = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_CreatorUserId = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_DeleteMark = dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_DeleteTime = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_DeleteUserId = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_Description = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_EnabledMark = dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_EnCode = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_FullName = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_LastModifyTime = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_LastModifyUserId = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_Layers = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_ParentId = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_SimpleSpelling = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.F_SortCode = dr.GetInt32(dataIndex);
			return item;
		}
		private void CopyItemAllField(Sys_AreaInfo item, Sys_AreaInfo newitem) {
			item.F_Id = newitem.F_Id;
			item.F_CreatorTime = newitem.F_CreatorTime;
			item.F_CreatorUserId = newitem.F_CreatorUserId;
			item.F_DeleteMark = newitem.F_DeleteMark;
			item.F_DeleteTime = newitem.F_DeleteTime;
			item.F_DeleteUserId = newitem.F_DeleteUserId;
			item.F_Description = newitem.F_Description;
			item.F_EnabledMark = newitem.F_EnabledMark;
			item.F_EnCode = newitem.F_EnCode;
			item.F_FullName = newitem.F_FullName;
			item.F_LastModifyTime = newitem.F_LastModifyTime;
			item.F_LastModifyUserId = newitem.F_LastModifyUserId;
			item.F_Layers = newitem.F_Layers;
			item.F_ParentId = newitem.F_ParentId;
			item.F_SimpleSpelling = newitem.F_SimpleSpelling;
			item.F_SortCode = newitem.F_SortCode;
		}
		#endregion

		public Sys_AreaInfo Delete(string F_Id) {
			Sys_AreaInfo item = null;
			SqlHelper.ExecuteReader(dr => { item = BLL.Sys_Area.dal.GetItem(dr); }, string.Concat(TSQL.Delete, @"[F_Id] = @F_Id"),
				new SqlParameter { ParameterName = "@F_Id", SqlDbType = SqlDbType.VarChar, Size = 50, Value = F_Id });
			return item;
		}

		public SqlUpdateBuild Update(Sys_AreaInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Sys_AreaInfo> { item }, false);
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("F_Id") == false) sub.SetF_Id(item.F_Id);
			if (ignore.ContainsKey("F_CreatorTime") == false) sub.SetF_CreatorTime(item.F_CreatorTime);
			if (ignore.ContainsKey("F_CreatorUserId") == false) sub.SetF_CreatorUserId(item.F_CreatorUserId);
			if (ignore.ContainsKey("F_DeleteMark") == false) sub.SetF_DeleteMark(item.F_DeleteMark);
			if (ignore.ContainsKey("F_DeleteTime") == false) sub.SetF_DeleteTime(item.F_DeleteTime);
			if (ignore.ContainsKey("F_DeleteUserId") == false) sub.SetF_DeleteUserId(item.F_DeleteUserId);
			if (ignore.ContainsKey("F_Description") == false) sub.SetF_Description(item.F_Description);
			if (ignore.ContainsKey("F_EnabledMark") == false) sub.SetF_EnabledMark(item.F_EnabledMark);
			if (ignore.ContainsKey("F_EnCode") == false) sub.SetF_EnCode(item.F_EnCode);
			if (ignore.ContainsKey("F_FullName") == false) sub.SetF_FullName(item.F_FullName);
			if (ignore.ContainsKey("F_LastModifyTime") == false) sub.SetF_LastModifyTime(item.F_LastModifyTime);
			if (ignore.ContainsKey("F_LastModifyUserId") == false) sub.SetF_LastModifyUserId(item.F_LastModifyUserId);
			if (ignore.ContainsKey("F_Layers") == false) sub.SetF_Layers(item.F_Layers);
			if (ignore.ContainsKey("F_ParentId") == false) sub.SetF_ParentId(item.F_ParentId);
			if (ignore.ContainsKey("F_SimpleSpelling") == false) sub.SetF_SimpleSpelling(item.F_SimpleSpelling);
			if (ignore.ContainsKey("F_SortCode") == false) sub.SetF_SortCode(item.F_SortCode);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Sys_AreaInfo> _dataSource;
			protected bool _isRefershDataSource;
			protected Dictionary<string, Sys_AreaInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			protected Dictionary<string, Action<Sys_AreaInfo, Sys_AreaInfo>> _setAs = new Dictionary<string, Action<Sys_AreaInfo, Sys_AreaInfo>>();
			public SqlUpdateBuild(List<Sys_AreaInfo> dataSource, bool isRefershDataSource) {
				_dataSource = dataSource;
				_isRefershDataSource = isRefershDataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.F_Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"[F_Id] IN ({0})", _dataSource.Select(a => a.F_Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 es.DAL.Sys_Area.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " OUTPUT ", TSQL.Field.Replace(@"a.[", @"INSERTED.["), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
					BLL.Sys_Area.RemoveCache(_dataSource);
					return affrows;
				}
				var newitems = new List<Sys_AreaInfo>();
				SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Sys_Area.dal.GetItem(dr)); }, sql, _parameters.ToArray());
				BLL.Sys_Area.RemoveCache(_dataSource.Concat(newitems));
				foreach (var newitem in newitems) {
					if (_itemsDic.TryGetValue($"{newitem.F_Id}", out var olditem)) foreach (var a in _setAs.Values) a(olditem, newitem);
					else {
						_dataSource.Add(newitem);
						_itemsDic.Add($"{newitem.F_Id}", newitem);
					}
				}
				return newitems.Count;
			}
			async public Task<int> ExecuteNonQueryAsync() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = await SqlHelper.ExecuteNonQueryAsync(sql, _parameters.ToArray());
					await BLL.Sys_Area.RemoveCacheAsync(_dataSource);
					return affrows;
				}
				var newitems = new List<Sys_AreaInfo>();
				await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Sys_Area.dal.GetItemAsync(dr)); }, sql, _parameters.ToArray());
				await BLL.Sys_Area.RemoveCacheAsync(_dataSource);
				foreach (var newitem in newitems) {
					if (_itemsDic.TryGetValue($"{newitem.F_Id}", out var olditem)) foreach (var a in _setAs.Values) a(olditem, newitem);
					else {
						_dataSource.Add(newitem);
						_itemsDic.Add($"{newitem.F_Id}", newitem);
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
				if (value.IndexOf('\'') != -1) throw new Exception("es.DAL.Sys_Area.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetF_Id(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_Id") == false) _setAs.Add("F_Id", (olditem, newitem) => olditem.F_Id = newitem.F_Id);
				return this.Set("[F_Id]", string.Concat("@F_Id_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_Id_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetF_CreatorTime(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_CreatorTime") == false) _setAs.Add("F_CreatorTime", (olditem, newitem) => olditem.F_CreatorTime = newitem.F_CreatorTime);
				return this.Set("[F_CreatorTime]", string.Concat("@F_CreatorTime_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_CreatorTime_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetF_CreatorUserId(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_CreatorUserId") == false) _setAs.Add("F_CreatorUserId", (olditem, newitem) => olditem.F_CreatorUserId = newitem.F_CreatorUserId);
				return this.Set("[F_CreatorUserId]", string.Concat("@F_CreatorUserId_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_CreatorUserId_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetF_DeleteMark(bool? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_DeleteMark") == false) _setAs.Add("F_DeleteMark", (olditem, newitem) => olditem.F_DeleteMark = newitem.F_DeleteMark);
				return this.Set("[F_DeleteMark]", string.Concat("@F_DeleteMark_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_DeleteMark_", _parameters.Count), SqlDbType = SqlDbType.Bit, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetF_DeleteTime(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_DeleteTime") == false) _setAs.Add("F_DeleteTime", (olditem, newitem) => olditem.F_DeleteTime = newitem.F_DeleteTime);
				return this.Set("[F_DeleteTime]", string.Concat("@F_DeleteTime_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_DeleteTime_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetF_DeleteUserId(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_DeleteUserId") == false) _setAs.Add("F_DeleteUserId", (olditem, newitem) => olditem.F_DeleteUserId = newitem.F_DeleteUserId);
				return this.Set("[F_DeleteUserId]", string.Concat("@F_DeleteUserId_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_DeleteUserId_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetF_Description(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_Description") == false) _setAs.Add("F_Description", (olditem, newitem) => olditem.F_Description = newitem.F_Description);
				return this.Set("[F_Description]", string.Concat("@F_Description_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_Description_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 500, Value = value });
			}
			public SqlUpdateBuild SetF_EnabledMark(bool? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_EnabledMark") == false) _setAs.Add("F_EnabledMark", (olditem, newitem) => olditem.F_EnabledMark = newitem.F_EnabledMark);
				return this.Set("[F_EnabledMark]", string.Concat("@F_EnabledMark_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_EnabledMark_", _parameters.Count), SqlDbType = SqlDbType.Bit, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetF_EnCode(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_EnCode") == false) _setAs.Add("F_EnCode", (olditem, newitem) => olditem.F_EnCode = newitem.F_EnCode);
				return this.Set("[F_EnCode]", string.Concat("@F_EnCode_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_EnCode_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetF_FullName(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_FullName") == false) _setAs.Add("F_FullName", (olditem, newitem) => olditem.F_FullName = newitem.F_FullName);
				return this.Set("[F_FullName]", string.Concat("@F_FullName_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_FullName_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetF_LastModifyTime(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_LastModifyTime") == false) _setAs.Add("F_LastModifyTime", (olditem, newitem) => olditem.F_LastModifyTime = newitem.F_LastModifyTime);
				return this.Set("[F_LastModifyTime]", string.Concat("@F_LastModifyTime_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_LastModifyTime_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetF_LastModifyUserId(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_LastModifyUserId") == false) _setAs.Add("F_LastModifyUserId", (olditem, newitem) => olditem.F_LastModifyUserId = newitem.F_LastModifyUserId);
				return this.Set("[F_LastModifyUserId]", string.Concat("@F_LastModifyUserId_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_LastModifyUserId_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetF_Layers(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_Layers") == false) _setAs.Add("F_Layers", (olditem, newitem) => olditem.F_Layers = newitem.F_Layers);
				return this.Set("[F_Layers]", string.Concat("@F_Layers_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_Layers_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetF_LayersIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_Layers") == false) _setAs.Add("F_Layers", (olditem, newitem) => olditem.F_Layers = newitem.F_Layers);
				return this.Set("[F_Layers]", string.Concat("[F_Layers] + @F_Layers_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_Layers_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetF_ParentId(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_ParentId") == false) _setAs.Add("F_ParentId", (olditem, newitem) => olditem.F_ParentId = newitem.F_ParentId);
				return this.Set("[F_ParentId]", string.Concat("@F_ParentId_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_ParentId_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetF_SimpleSpelling(string value) {
				if (_dataSource != null && _setAs.ContainsKey("F_SimpleSpelling") == false) _setAs.Add("F_SimpleSpelling", (olditem, newitem) => olditem.F_SimpleSpelling = newitem.F_SimpleSpelling);
				return this.Set("[F_SimpleSpelling]", string.Concat("@F_SimpleSpelling_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_SimpleSpelling_", _parameters.Count), SqlDbType = SqlDbType.VarChar, Size = 50, Value = value });
			}
			public SqlUpdateBuild SetF_SortCode(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_SortCode") == false) _setAs.Add("F_SortCode", (olditem, newitem) => olditem.F_SortCode = newitem.F_SortCode);
				return this.Set("[F_SortCode]", string.Concat("@F_SortCode_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_SortCode_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetF_SortCodeIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("F_SortCode") == false) _setAs.Add("F_SortCode", (olditem, newitem) => olditem.F_SortCode = newitem.F_SortCode);
				return this.Set("[F_SortCode]", string.Concat("[F_SortCode] + @F_SortCode_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@F_SortCode_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
		}
		#endregion

		public Sys_AreaInfo Insert(Sys_AreaInfo item) {
			Sys_AreaInfo newitem = null;
			SqlHelper.ExecuteReader(dr => { newitem = GetItem(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		public List<Sys_AreaInfo> Insert(IEnumerable<Sys_AreaInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<Sys_AreaInfo>();
			List<Sys_AreaInfo> newitems = new List<Sys_AreaInfo>();
			SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Sys_Area.dal.GetItem(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		public (string sql, SqlParameter[] parms) InsertMakeParam(IEnumerable<Sys_AreaInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new SqlParameter[itemsArr.Length * 16];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 16 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<Sys_AreaInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Sys_AreaInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			Sys_AreaInfo item = new Sys_AreaInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_Id = await dr.GetFieldValueAsync<string>(dataIndex); if (item.F_Id == null) { dataIndex += 15; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_CreatorTime = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_CreatorUserId = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_DeleteMark = await dr.GetFieldValueAsync<bool>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_DeleteTime = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_DeleteUserId = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_Description = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_EnabledMark = await dr.GetFieldValueAsync<bool>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_EnCode = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_FullName = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_LastModifyTime = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_LastModifyUserId = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_Layers = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_ParentId = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_SimpleSpelling = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.F_SortCode = await dr.GetFieldValueAsync<int>(dataIndex);
			return (item, dataIndex);
		}
		async public Task<Sys_AreaInfo> DeleteAsync(string F_Id) {
			Sys_AreaInfo item = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { item = await BLL.Sys_Area.dal.GetItemAsync(dr); }, string.Concat(TSQL.Delete, @"[F_Id] = @F_Id"),
				new SqlParameter { ParameterName = "@F_Id", SqlDbType = SqlDbType.VarChar, Size = 50, Value = F_Id });
			return item;
		}
		async public Task<Sys_AreaInfo> InsertAsync(Sys_AreaInfo item) {
			Sys_AreaInfo newitem = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { newitem = await GetItemAsync(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		async public Task<List<Sys_AreaInfo>> InsertAsync(IEnumerable<Sys_AreaInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<Sys_AreaInfo>();
			List<Sys_AreaInfo> newitems = new List<Sys_AreaInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Sys_Area.dal.GetItemAsync(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		#endregion
	}
}