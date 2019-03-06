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

	public partial class Tb_alltype : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[tb_alltype]";
			internal static readonly string Field = "a.[Id], a.[testFieldBool1111], a.[testFieldBoolNullable], a.[testFieldByte], a.[testFieldByteNullable], a.[testFieldBytes], a.[testFieldDateTime], a.[testFieldDateTimeNullable], a.[testFieldDateTimeNullableOffset], a.[testFieldDateTimeOffset], a.[testFieldDecimal], a.[testFieldDecimalNullable], a.[testFieldDouble], a.[testFieldDoubleNullable], a.[testFieldEnum1], a.[testFieldEnum1Nullable], a.[testFieldEnum2], a.[testFieldEnum2Nullable], a.[testFieldFloat], a.[testFieldFloatNullable], a.[testFieldGuid], a.[testFieldGuidNullable], a.[testFieldInt], a.[testFieldIntNullable], a.[testFieldLong], a.[testFieldSByte], a.[testFieldSByteNullable], a.[testFieldShort], a.[testFieldShortNullable], a.[testFieldString], a.[testFieldTimeSpan], a.[testFieldTimeSpanNullable], a.[testFieldUInt], a.[testFieldUIntNullable], a.[testFieldULong], a.[testFieldULongNullable], a.[testFieldUShort], a.[testFieldUShortNullable], a.[testFielLongNullable]";
			internal static readonly string Sort = "a.[Id]";
			internal static readonly string Delete = "DELETE FROM [dbo].[tb_alltype] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[testFieldBool1111], [testFieldBoolNullable], [testFieldByte], [testFieldByteNullable], [testFieldBytes], [testFieldDateTime], [testFieldDateTimeNullable], [testFieldDateTimeNullableOffset], [testFieldDateTimeOffset], [testFieldDecimal], [testFieldDecimalNullable], [testFieldDouble], [testFieldDoubleNullable], [testFieldEnum1], [testFieldEnum1Nullable], [testFieldEnum2], [testFieldEnum2Nullable], [testFieldFloat], [testFieldFloatNullable], [testFieldGuid], [testFieldGuidNullable], [testFieldInt], [testFieldIntNullable], [testFieldLong], [testFieldSByte], [testFieldSByteNullable], [testFieldShort], [testFieldShortNullable], [testFieldString], [testFieldTimeSpan], [testFieldTimeSpanNullable], [testFieldUInt], [testFieldUIntNullable], [testFieldULong], [testFieldULongNullable], [testFieldUShort], [testFieldUShortNullable], [testFielLongNullable]";
			internal static readonly string InsertValues = "@testFieldBool1111, @testFieldBoolNullable, @testFieldByte, @testFieldByteNullable, @testFieldBytes, @testFieldDateTime, @testFieldDateTimeNullable, @testFieldDateTimeNullableOffset, @testFieldDateTimeOffset, @testFieldDecimal, @testFieldDecimalNullable, @testFieldDouble, @testFieldDoubleNullable, @testFieldEnum1, @testFieldEnum1Nullable, @testFieldEnum2, @testFieldEnum2Nullable, @testFieldFloat, @testFieldFloatNullable, @testFieldGuid, @testFieldGuidNullable, @testFieldInt, @testFieldIntNullable, @testFieldLong, @testFieldSByte, @testFieldSByteNullable, @testFieldShort, @testFieldShortNullable, @testFieldString, @testFieldTimeSpan, @testFieldTimeSpanNullable, @testFieldUInt, @testFieldUIntNullable, @testFieldULong, @testFieldULongNullable, @testFieldUShort, @testFieldUShortNullable, @testFielLongNullable";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[tb_alltype](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(Tb_alltypeInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Id }, 
				new SqlParameter { ParameterName = "@testFieldBool1111", SqlDbType = SqlDbType.Bit, Size = 1, Value = item.TestFieldBool1111 }, 
				new SqlParameter { ParameterName = "@testFieldBoolNullable", SqlDbType = SqlDbType.Bit, Size = 1, Value = item.TestFieldBoolNullable }, 
				new SqlParameter { ParameterName = "@testFieldByte", SqlDbType = SqlDbType.TinyInt, Size = 1, Value = item.TestFieldByte }, 
				new SqlParameter { ParameterName = "@testFieldByteNullable", SqlDbType = SqlDbType.TinyInt, Size = 1, Value = item.TestFieldByteNullable }, 
				new SqlParameter { ParameterName = "@testFieldBytes", SqlDbType = SqlDbType.VarBinary, Size = 255, Value = item.TestFieldBytes }, 
				new SqlParameter { ParameterName = "@testFieldDateTime", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.TestFieldDateTime }, 
				new SqlParameter { ParameterName = "@testFieldDateTimeNullable", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.TestFieldDateTimeNullable }, 
				new SqlParameter { ParameterName = "@testFieldDateTimeNullableOffset", SqlDbType = SqlDbType.DateTimeOffset, Size = 10, Value = item.TestFieldDateTimeNullableOffset }, 
				new SqlParameter { ParameterName = "@testFieldDateTimeOffset", SqlDbType = SqlDbType.DateTimeOffset, Size = 10, Value = item.TestFieldDateTimeOffset }, 
				new SqlParameter { ParameterName = "@testFieldDecimal", SqlDbType = SqlDbType.Decimal, Size = 9, Value = item.TestFieldDecimal }, 
				new SqlParameter { ParameterName = "@testFieldDecimalNullable", SqlDbType = SqlDbType.Decimal, Size = 9, Value = item.TestFieldDecimalNullable }, 
				new SqlParameter { ParameterName = "@testFieldDouble", SqlDbType = SqlDbType.Float, Size = 8, Value = item.TestFieldDouble }, 
				new SqlParameter { ParameterName = "@testFieldDoubleNullable", SqlDbType = SqlDbType.Float, Size = 8, Value = item.TestFieldDoubleNullable }, 
				new SqlParameter { ParameterName = "@testFieldEnum1", SqlDbType = SqlDbType.Int, Size = 4, Value = item.TestFieldEnum1 }, 
				new SqlParameter { ParameterName = "@testFieldEnum1Nullable", SqlDbType = SqlDbType.Int, Size = 4, Value = item.TestFieldEnum1Nullable }, 
				new SqlParameter { ParameterName = "@testFieldEnum2", SqlDbType = SqlDbType.BigInt, Size = 8, Value = item.TestFieldEnum2 }, 
				new SqlParameter { ParameterName = "@testFieldEnum2Nullable", SqlDbType = SqlDbType.BigInt, Size = 8, Value = item.TestFieldEnum2Nullable }, 
				new SqlParameter { ParameterName = "@testFieldFloat", SqlDbType = SqlDbType.Real, Size = 4, Value = item.TestFieldFloat }, 
				new SqlParameter { ParameterName = "@testFieldFloatNullable", SqlDbType = SqlDbType.Real, Size = 4, Value = item.TestFieldFloatNullable }, 
				new SqlParameter { ParameterName = "@testFieldGuid", SqlDbType = SqlDbType.UniqueIdentifier, Size = 16, Value = item.TestFieldGuid }, 
				new SqlParameter { ParameterName = "@testFieldGuidNullable", SqlDbType = SqlDbType.UniqueIdentifier, Size = 16, Value = item.TestFieldGuidNullable }, 
				new SqlParameter { ParameterName = "@testFieldInt", SqlDbType = SqlDbType.Int, Size = 4, Value = item.TestFieldInt }, 
				new SqlParameter { ParameterName = "@testFieldIntNullable", SqlDbType = SqlDbType.Int, Size = 4, Value = item.TestFieldIntNullable }, 
				new SqlParameter { ParameterName = "@testFieldLong", SqlDbType = SqlDbType.BigInt, Size = 8, Value = item.TestFieldLong }, 
				new SqlParameter { ParameterName = "@testFieldSByte", SqlDbType = SqlDbType.TinyInt, Size = 1, Value = item.TestFieldSByte }, 
				new SqlParameter { ParameterName = "@testFieldSByteNullable", SqlDbType = SqlDbType.TinyInt, Size = 1, Value = item.TestFieldSByteNullable }, 
				new SqlParameter { ParameterName = "@testFieldShort", SqlDbType = SqlDbType.SmallInt, Size = 2, Value = item.TestFieldShort }, 
				new SqlParameter { ParameterName = "@testFieldShortNullable", SqlDbType = SqlDbType.SmallInt, Size = 2, Value = item.TestFieldShortNullable }, 
				new SqlParameter { ParameterName = "@testFieldString", SqlDbType = SqlDbType.NVarChar, Size = 255, Value = item.TestFieldString }, 
				new SqlParameter { ParameterName = "@testFieldTimeSpan", SqlDbType = SqlDbType.Time, Size = 5, Value = item.TestFieldTimeSpan }, 
				new SqlParameter { ParameterName = "@testFieldTimeSpanNullable", SqlDbType = SqlDbType.Time, Size = 5, Value = item.TestFieldTimeSpanNullable }, 
				new SqlParameter { ParameterName = "@testFieldUInt", SqlDbType = SqlDbType.Int, Size = 4, Value = item.TestFieldUInt }, 
				new SqlParameter { ParameterName = "@testFieldUIntNullable", SqlDbType = SqlDbType.Int, Size = 4, Value = item.TestFieldUIntNullable }, 
				new SqlParameter { ParameterName = "@testFieldULong", SqlDbType = SqlDbType.BigInt, Size = 8, Value = item.TestFieldULong }, 
				new SqlParameter { ParameterName = "@testFieldULongNullable", SqlDbType = SqlDbType.BigInt, Size = 8, Value = item.TestFieldULongNullable }, 
				new SqlParameter { ParameterName = "@testFieldUShort", SqlDbType = SqlDbType.SmallInt, Size = 2, Value = item.TestFieldUShort }, 
				new SqlParameter { ParameterName = "@testFieldUShortNullable", SqlDbType = SqlDbType.SmallInt, Size = 2, Value = item.TestFieldUShortNullable }, 
				new SqlParameter { ParameterName = "@testFielLongNullable", SqlDbType = SqlDbType.BigInt, Size = 8, Value = item.TestFielLongNullable }
			};
		}
		public Tb_alltypeInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as Tb_alltypeInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			Tb_alltypeInfo item = new Tb_alltypeInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Id = dr.GetInt32(dataIndex); if (item.Id == null) { dataIndex += 38; return null; }
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldBool1111 = dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldBoolNullable = dr.GetBoolean(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldByte = dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldByteNullable = dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldBytes = GetBytes(dr, dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDateTime = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDateTimeNullable = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDateTimeNullableOffset = dr.GetDateTimeOffset(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDateTimeOffset = dr.GetDateTimeOffset(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDecimal = dr.GetDecimal(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDecimalNullable = dr.GetDecimal(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDouble = dr.GetDouble(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldDoubleNullable = dr.GetDouble(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldEnum1 = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldEnum1Nullable = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldEnum2 = dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldEnum2Nullable = dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldFloat = dr.GetFloat(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldFloatNullable = dr.GetFloat(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldGuid = dr.GetGuid(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldGuidNullable = dr.GetGuid(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldInt = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldIntNullable = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldLong = dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldSByte = dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldSByteNullable = dr.GetByte(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldShort = dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldShortNullable = dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldString = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldTimeSpan = dr.GetTimeSpan(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldTimeSpanNullable = dr.GetTimeSpan(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldUInt = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldUIntNullable = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldULong = dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldULongNullable = dr.GetInt64(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldUShort = dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFieldUShortNullable = dr.GetInt16(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.TestFielLongNullable = dr.GetInt64(dataIndex);
			return item;
		}
		private void CopyItemAllField(Tb_alltypeInfo item, Tb_alltypeInfo newitem) {
			item.Id = newitem.Id;
			item.TestFieldBool1111 = newitem.TestFieldBool1111;
			item.TestFieldBoolNullable = newitem.TestFieldBoolNullable;
			item.TestFieldByte = newitem.TestFieldByte;
			item.TestFieldByteNullable = newitem.TestFieldByteNullable;
			item.TestFieldBytes = newitem.TestFieldBytes;
			item.TestFieldDateTime = newitem.TestFieldDateTime;
			item.TestFieldDateTimeNullable = newitem.TestFieldDateTimeNullable;
			item.TestFieldDateTimeNullableOffset = newitem.TestFieldDateTimeNullableOffset;
			item.TestFieldDateTimeOffset = newitem.TestFieldDateTimeOffset;
			item.TestFieldDecimal = newitem.TestFieldDecimal;
			item.TestFieldDecimalNullable = newitem.TestFieldDecimalNullable;
			item.TestFieldDouble = newitem.TestFieldDouble;
			item.TestFieldDoubleNullable = newitem.TestFieldDoubleNullable;
			item.TestFieldEnum1 = newitem.TestFieldEnum1;
			item.TestFieldEnum1Nullable = newitem.TestFieldEnum1Nullable;
			item.TestFieldEnum2 = newitem.TestFieldEnum2;
			item.TestFieldEnum2Nullable = newitem.TestFieldEnum2Nullable;
			item.TestFieldFloat = newitem.TestFieldFloat;
			item.TestFieldFloatNullable = newitem.TestFieldFloatNullable;
			item.TestFieldGuid = newitem.TestFieldGuid;
			item.TestFieldGuidNullable = newitem.TestFieldGuidNullable;
			item.TestFieldInt = newitem.TestFieldInt;
			item.TestFieldIntNullable = newitem.TestFieldIntNullable;
			item.TestFieldLong = newitem.TestFieldLong;
			item.TestFieldSByte = newitem.TestFieldSByte;
			item.TestFieldSByteNullable = newitem.TestFieldSByteNullable;
			item.TestFieldShort = newitem.TestFieldShort;
			item.TestFieldShortNullable = newitem.TestFieldShortNullable;
			item.TestFieldString = newitem.TestFieldString;
			item.TestFieldTimeSpan = newitem.TestFieldTimeSpan;
			item.TestFieldTimeSpanNullable = newitem.TestFieldTimeSpanNullable;
			item.TestFieldUInt = newitem.TestFieldUInt;
			item.TestFieldUIntNullable = newitem.TestFieldUIntNullable;
			item.TestFieldULong = newitem.TestFieldULong;
			item.TestFieldULongNullable = newitem.TestFieldULongNullable;
			item.TestFieldUShort = newitem.TestFieldUShort;
			item.TestFieldUShortNullable = newitem.TestFieldUShortNullable;
			item.TestFielLongNullable = newitem.TestFielLongNullable;
		}
		public byte[] GetBytes(SqlDataReader dr, int dataIndex) {
			if (dr.IsDBNull(dataIndex)) return null;
			var ms = new MemoryStream();
			byte[] bt = new byte[1048576 * 8];
			int size = 0;
			while ((size = (int)dr.GetBytes(dataIndex, ms.Position, bt, 0, bt.Length)) > 0) ms.Write(bt, 0, size);
			return ms.ToArray();
		}
		#endregion

		public Tb_alltypeInfo Delete(int? Id) {
			Tb_alltypeInfo item = null;
			SqlHelper.ExecuteReader(dr => { item = BLL.Tb_alltype.dal.GetItem(dr); }, string.Concat(TSQL.Delete, @"[Id] = @Id"),
				new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}

		public SqlUpdateBuild Update(Tb_alltypeInfo item, string[] ignoreFields) {
			var sub = new SqlUpdateBuild(new List<Tb_alltypeInfo> { item }, false);
			var ignore = ignoreFields?.ToDictionary(a => a, StringComparer.CurrentCultureIgnoreCase) ?? new Dictionary<string, string>();
			if (ignore.ContainsKey("testFieldBool1111") == false) sub.SetTestFieldBool1111(item.TestFieldBool1111);
			if (ignore.ContainsKey("testFieldBoolNullable") == false) sub.SetTestFieldBoolNullable(item.TestFieldBoolNullable);
			if (ignore.ContainsKey("testFieldByte") == false) sub.SetTestFieldByte(item.TestFieldByte);
			if (ignore.ContainsKey("testFieldByteNullable") == false) sub.SetTestFieldByteNullable(item.TestFieldByteNullable);
			if (ignore.ContainsKey("testFieldBytes") == false) sub.SetTestFieldBytes(item.TestFieldBytes);
			if (ignore.ContainsKey("testFieldDateTime") == false) sub.SetTestFieldDateTime(item.TestFieldDateTime);
			if (ignore.ContainsKey("testFieldDateTimeNullable") == false) sub.SetTestFieldDateTimeNullable(item.TestFieldDateTimeNullable);
			if (ignore.ContainsKey("testFieldDateTimeNullableOffset") == false) sub.SetTestFieldDateTimeNullableOffset(item.TestFieldDateTimeNullableOffset);
			if (ignore.ContainsKey("testFieldDateTimeOffset") == false) sub.SetTestFieldDateTimeOffset(item.TestFieldDateTimeOffset);
			if (ignore.ContainsKey("testFieldDecimal") == false) sub.SetTestFieldDecimal(item.TestFieldDecimal);
			if (ignore.ContainsKey("testFieldDecimalNullable") == false) sub.SetTestFieldDecimalNullable(item.TestFieldDecimalNullable);
			if (ignore.ContainsKey("testFieldDouble") == false) sub.SetTestFieldDouble(item.TestFieldDouble);
			if (ignore.ContainsKey("testFieldDoubleNullable") == false) sub.SetTestFieldDoubleNullable(item.TestFieldDoubleNullable);
			if (ignore.ContainsKey("testFieldEnum1") == false) sub.SetTestFieldEnum1(item.TestFieldEnum1);
			if (ignore.ContainsKey("testFieldEnum1Nullable") == false) sub.SetTestFieldEnum1Nullable(item.TestFieldEnum1Nullable);
			if (ignore.ContainsKey("testFieldEnum2") == false) sub.SetTestFieldEnum2(item.TestFieldEnum2);
			if (ignore.ContainsKey("testFieldEnum2Nullable") == false) sub.SetTestFieldEnum2Nullable(item.TestFieldEnum2Nullable);
			if (ignore.ContainsKey("testFieldFloat") == false) sub.SetTestFieldFloat(item.TestFieldFloat);
			if (ignore.ContainsKey("testFieldFloatNullable") == false) sub.SetTestFieldFloatNullable(item.TestFieldFloatNullable);
			if (ignore.ContainsKey("testFieldGuid") == false) sub.SetTestFieldGuid(item.TestFieldGuid);
			if (ignore.ContainsKey("testFieldGuidNullable") == false) sub.SetTestFieldGuidNullable(item.TestFieldGuidNullable);
			if (ignore.ContainsKey("testFieldInt") == false) sub.SetTestFieldInt(item.TestFieldInt);
			if (ignore.ContainsKey("testFieldIntNullable") == false) sub.SetTestFieldIntNullable(item.TestFieldIntNullable);
			if (ignore.ContainsKey("testFieldLong") == false) sub.SetTestFieldLong(item.TestFieldLong);
			if (ignore.ContainsKey("testFieldSByte") == false) sub.SetTestFieldSByte(item.TestFieldSByte);
			if (ignore.ContainsKey("testFieldSByteNullable") == false) sub.SetTestFieldSByteNullable(item.TestFieldSByteNullable);
			if (ignore.ContainsKey("testFieldShort") == false) sub.SetTestFieldShort(item.TestFieldShort);
			if (ignore.ContainsKey("testFieldShortNullable") == false) sub.SetTestFieldShortNullable(item.TestFieldShortNullable);
			if (ignore.ContainsKey("testFieldString") == false) sub.SetTestFieldString(item.TestFieldString);
			if (ignore.ContainsKey("testFieldTimeSpan") == false) sub.SetTestFieldTimeSpan(item.TestFieldTimeSpan);
			if (ignore.ContainsKey("testFieldTimeSpanNullable") == false) sub.SetTestFieldTimeSpanNullable(item.TestFieldTimeSpanNullable);
			if (ignore.ContainsKey("testFieldUInt") == false) sub.SetTestFieldUInt(item.TestFieldUInt);
			if (ignore.ContainsKey("testFieldUIntNullable") == false) sub.SetTestFieldUIntNullable(item.TestFieldUIntNullable);
			if (ignore.ContainsKey("testFieldULong") == false) sub.SetTestFieldULong(item.TestFieldULong);
			if (ignore.ContainsKey("testFieldULongNullable") == false) sub.SetTestFieldULongNullable(item.TestFieldULongNullable);
			if (ignore.ContainsKey("testFieldUShort") == false) sub.SetTestFieldUShort(item.TestFieldUShort);
			if (ignore.ContainsKey("testFieldUShortNullable") == false) sub.SetTestFieldUShortNullable(item.TestFieldUShortNullable);
			if (ignore.ContainsKey("testFielLongNullable") == false) sub.SetTestFielLongNullable(item.TestFielLongNullable);
			return sub;
		}
		#region class SqlUpdateBuild
		public partial class SqlUpdateBuild {
			protected List<Tb_alltypeInfo> _dataSource;
			protected bool _isRefershDataSource;
			protected Dictionary<string, Tb_alltypeInfo> _itemsDic;
			protected string _fields;
			protected string _where;
			protected List<SqlParameter> _parameters = new List<SqlParameter>();
			protected Dictionary<string, Action<Tb_alltypeInfo, Tb_alltypeInfo>> _setAs = new Dictionary<string, Action<Tb_alltypeInfo, Tb_alltypeInfo>>();
			public SqlUpdateBuild(List<Tb_alltypeInfo> dataSource, bool isRefershDataSource) {
				_dataSource = dataSource;
				_isRefershDataSource = isRefershDataSource;
				_itemsDic = _dataSource == null ? null : _dataSource.ToDictionary(a => $"{a.Id}");
				if (_dataSource != null && _dataSource.Any())
					this.Where(@"[Id] IN ({0})", _dataSource.Select(a => a.Id).Distinct());
			}
			public SqlUpdateBuild() { }
			public override string ToString() {
				if (string.IsNullOrEmpty(_fields)) return string.Empty;
				if (string.IsNullOrEmpty(_where)) throw new Exception("防止 es.DAL.Tb_alltype.SqlUpdateBuild 误修改，请必须设置 where 条件。");
				return string.Concat("UPDATE ", TSQL.Table, " SET ", _fields.Substring(1), " OUTPUT ", TSQL.Field.Replace(@"a.[", @"INSERTED.["), " WHERE ", _where);
			}
			public int ExecuteNonQuery() {
				string sql = this.ToString();
				if (string.IsNullOrEmpty(sql)) return 0;
				if (_dataSource == null || _dataSource.Any() == false || _isRefershDataSource == false) {
					var affrows = SqlHelper.ExecuteNonQuery(sql, _parameters.ToArray());
					BLL.Tb_alltype.RemoveCache(_dataSource);
					return affrows;
				}
				var newitems = new List<Tb_alltypeInfo>();
				SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Tb_alltype.dal.GetItem(dr)); }, sql, _parameters.ToArray());
				BLL.Tb_alltype.RemoveCache(_dataSource.Concat(newitems));
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
					await BLL.Tb_alltype.RemoveCacheAsync(_dataSource);
					return affrows;
				}
				var newitems = new List<Tb_alltypeInfo>();
				await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Tb_alltype.dal.GetItemAsync(dr)); }, sql, _parameters.ToArray());
				await BLL.Tb_alltype.RemoveCacheAsync(_dataSource);
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
				if (value.IndexOf('\'') != -1) throw new Exception("es.DAL.Tb_alltype.SqlUpdateBuild 可能存在注入漏洞，不允许传递 ' 给参数 value，若使用正常字符串，请使用参数化传递。");
				_fields = string.Concat(_fields, ", ", field, " = ", value);
				if (parms != null && parms.Length > 0) _parameters.AddRange(parms);
				return this;
			}
			public SqlUpdateBuild SetTestFieldBool1111(bool? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldBool1111") == false) _setAs.Add("TestFieldBool1111", (olditem, newitem) => olditem.TestFieldBool1111 = newitem.TestFieldBool1111);
				return this.Set("[testFieldBool1111]", string.Concat("@testFieldBool1111_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldBool1111_", _parameters.Count), SqlDbType = SqlDbType.Bit, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldBoolNullable(bool? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldBoolNullable") == false) _setAs.Add("TestFieldBoolNullable", (olditem, newitem) => olditem.TestFieldBoolNullable = newitem.TestFieldBoolNullable);
				return this.Set("[testFieldBoolNullable]", string.Concat("@testFieldBoolNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldBoolNullable_", _parameters.Count), SqlDbType = SqlDbType.Bit, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldByte(byte? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldByte") == false) _setAs.Add("TestFieldByte", (olditem, newitem) => olditem.TestFieldByte = newitem.TestFieldByte);
				return this.Set("[testFieldByte]", string.Concat("@testFieldByte_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldByte_", _parameters.Count), SqlDbType = SqlDbType.TinyInt, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldByteIncrement(byte? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldByte") == false) _setAs.Add("TestFieldByte", (olditem, newitem) => olditem.TestFieldByte = newitem.TestFieldByte);
				return this.Set("[testFieldByte]", string.Concat("[testFieldByte] + @testFieldByte_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldByte_", _parameters.Count), SqlDbType = SqlDbType.TinyInt, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldByteNullable(byte? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldByteNullable") == false) _setAs.Add("TestFieldByteNullable", (olditem, newitem) => olditem.TestFieldByteNullable = newitem.TestFieldByteNullable);
				return this.Set("[testFieldByteNullable]", string.Concat("@testFieldByteNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldByteNullable_", _parameters.Count), SqlDbType = SqlDbType.TinyInt, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldByteNullableIncrement(byte? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldByteNullable") == false) _setAs.Add("TestFieldByteNullable", (olditem, newitem) => olditem.TestFieldByteNullable = newitem.TestFieldByteNullable);
				return this.Set("[testFieldByteNullable]", string.Concat("[testFieldByteNullable] + @testFieldByteNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldByteNullable_", _parameters.Count), SqlDbType = SqlDbType.TinyInt, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldBytes(byte[] value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldBytes") == false) _setAs.Add("TestFieldBytes", (olditem, newitem) => olditem.TestFieldBytes = newitem.TestFieldBytes);
				return this.Set("[testFieldBytes]", string.Concat("@testFieldBytes_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldBytes_", _parameters.Count), SqlDbType = SqlDbType.VarBinary, Size = 255, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDateTime(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDateTime") == false) _setAs.Add("TestFieldDateTime", (olditem, newitem) => olditem.TestFieldDateTime = newitem.TestFieldDateTime);
				return this.Set("[testFieldDateTime]", string.Concat("@testFieldDateTime_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDateTime_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDateTimeNullable(DateTime? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDateTimeNullable") == false) _setAs.Add("TestFieldDateTimeNullable", (olditem, newitem) => olditem.TestFieldDateTimeNullable = newitem.TestFieldDateTimeNullable);
				return this.Set("[testFieldDateTimeNullable]", string.Concat("@testFieldDateTimeNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDateTimeNullable_", _parameters.Count), SqlDbType = SqlDbType.DateTime, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDateTimeNullableOffset(DateTimeOffset? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDateTimeNullableOffset") == false) _setAs.Add("TestFieldDateTimeNullableOffset", (olditem, newitem) => olditem.TestFieldDateTimeNullableOffset = newitem.TestFieldDateTimeNullableOffset);
				return this.Set("[testFieldDateTimeNullableOffset]", string.Concat("@testFieldDateTimeNullableOffset_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDateTimeNullableOffset_", _parameters.Count), SqlDbType = SqlDbType.DateTimeOffset, Size = 10, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDateTimeOffset(DateTimeOffset? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDateTimeOffset") == false) _setAs.Add("TestFieldDateTimeOffset", (olditem, newitem) => olditem.TestFieldDateTimeOffset = newitem.TestFieldDateTimeOffset);
				return this.Set("[testFieldDateTimeOffset]", string.Concat("@testFieldDateTimeOffset_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDateTimeOffset_", _parameters.Count), SqlDbType = SqlDbType.DateTimeOffset, Size = 10, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDecimal(decimal? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDecimal") == false) _setAs.Add("TestFieldDecimal", (olditem, newitem) => olditem.TestFieldDecimal = newitem.TestFieldDecimal);
				return this.Set("[testFieldDecimal]", string.Concat("@testFieldDecimal_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDecimal_", _parameters.Count), SqlDbType = SqlDbType.Decimal, Size = 9, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDecimalIncrement(decimal? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDecimal") == false) _setAs.Add("TestFieldDecimal", (olditem, newitem) => olditem.TestFieldDecimal = newitem.TestFieldDecimal);
				return this.Set("[testFieldDecimal]", string.Concat("[testFieldDecimal] + @testFieldDecimal_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDecimal_", _parameters.Count), SqlDbType = SqlDbType.Decimal, Size = 9, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDecimalNullable(decimal? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDecimalNullable") == false) _setAs.Add("TestFieldDecimalNullable", (olditem, newitem) => olditem.TestFieldDecimalNullable = newitem.TestFieldDecimalNullable);
				return this.Set("[testFieldDecimalNullable]", string.Concat("@testFieldDecimalNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDecimalNullable_", _parameters.Count), SqlDbType = SqlDbType.Decimal, Size = 9, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDecimalNullableIncrement(decimal? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDecimalNullable") == false) _setAs.Add("TestFieldDecimalNullable", (olditem, newitem) => olditem.TestFieldDecimalNullable = newitem.TestFieldDecimalNullable);
				return this.Set("[testFieldDecimalNullable]", string.Concat("[testFieldDecimalNullable] + @testFieldDecimalNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDecimalNullable_", _parameters.Count), SqlDbType = SqlDbType.Decimal, Size = 9, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDouble(double? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDouble") == false) _setAs.Add("TestFieldDouble", (olditem, newitem) => olditem.TestFieldDouble = newitem.TestFieldDouble);
				return this.Set("[testFieldDouble]", string.Concat("@testFieldDouble_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDouble_", _parameters.Count), SqlDbType = SqlDbType.Float, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDoubleIncrement(double? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDouble") == false) _setAs.Add("TestFieldDouble", (olditem, newitem) => olditem.TestFieldDouble = newitem.TestFieldDouble);
				return this.Set("[testFieldDouble]", string.Concat("[testFieldDouble] + @testFieldDouble_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDouble_", _parameters.Count), SqlDbType = SqlDbType.Float, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDoubleNullable(double? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDoubleNullable") == false) _setAs.Add("TestFieldDoubleNullable", (olditem, newitem) => olditem.TestFieldDoubleNullable = newitem.TestFieldDoubleNullable);
				return this.Set("[testFieldDoubleNullable]", string.Concat("@testFieldDoubleNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDoubleNullable_", _parameters.Count), SqlDbType = SqlDbType.Float, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldDoubleNullableIncrement(double? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldDoubleNullable") == false) _setAs.Add("TestFieldDoubleNullable", (olditem, newitem) => olditem.TestFieldDoubleNullable = newitem.TestFieldDoubleNullable);
				return this.Set("[testFieldDoubleNullable]", string.Concat("[testFieldDoubleNullable] + @testFieldDoubleNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldDoubleNullable_", _parameters.Count), SqlDbType = SqlDbType.Float, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldEnum1(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldEnum1") == false) _setAs.Add("TestFieldEnum1", (olditem, newitem) => olditem.TestFieldEnum1 = newitem.TestFieldEnum1);
				return this.Set("[testFieldEnum1]", string.Concat("@testFieldEnum1_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldEnum1_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldEnum1Increment(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldEnum1") == false) _setAs.Add("TestFieldEnum1", (olditem, newitem) => olditem.TestFieldEnum1 = newitem.TestFieldEnum1);
				return this.Set("[testFieldEnum1]", string.Concat("[testFieldEnum1] + @testFieldEnum1_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldEnum1_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldEnum1Nullable(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldEnum1Nullable") == false) _setAs.Add("TestFieldEnum1Nullable", (olditem, newitem) => olditem.TestFieldEnum1Nullable = newitem.TestFieldEnum1Nullable);
				return this.Set("[testFieldEnum1Nullable]", string.Concat("@testFieldEnum1Nullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldEnum1Nullable_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldEnum1NullableIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldEnum1Nullable") == false) _setAs.Add("TestFieldEnum1Nullable", (olditem, newitem) => olditem.TestFieldEnum1Nullable = newitem.TestFieldEnum1Nullable);
				return this.Set("[testFieldEnum1Nullable]", string.Concat("[testFieldEnum1Nullable] + @testFieldEnum1Nullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldEnum1Nullable_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldEnum2(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldEnum2") == false) _setAs.Add("TestFieldEnum2", (olditem, newitem) => olditem.TestFieldEnum2 = newitem.TestFieldEnum2);
				return this.Set("[testFieldEnum2]", string.Concat("@testFieldEnum2_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldEnum2_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldEnum2Increment(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldEnum2") == false) _setAs.Add("TestFieldEnum2", (olditem, newitem) => olditem.TestFieldEnum2 = newitem.TestFieldEnum2);
				return this.Set("[testFieldEnum2]", string.Concat("[testFieldEnum2] + @testFieldEnum2_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldEnum2_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldEnum2Nullable(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldEnum2Nullable") == false) _setAs.Add("TestFieldEnum2Nullable", (olditem, newitem) => olditem.TestFieldEnum2Nullable = newitem.TestFieldEnum2Nullable);
				return this.Set("[testFieldEnum2Nullable]", string.Concat("@testFieldEnum2Nullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldEnum2Nullable_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldEnum2NullableIncrement(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldEnum2Nullable") == false) _setAs.Add("TestFieldEnum2Nullable", (olditem, newitem) => olditem.TestFieldEnum2Nullable = newitem.TestFieldEnum2Nullable);
				return this.Set("[testFieldEnum2Nullable]", string.Concat("[testFieldEnum2Nullable] + @testFieldEnum2Nullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldEnum2Nullable_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldFloat(float? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldFloat") == false) _setAs.Add("TestFieldFloat", (olditem, newitem) => olditem.TestFieldFloat = newitem.TestFieldFloat);
				return this.Set("[testFieldFloat]", string.Concat("@testFieldFloat_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldFloat_", _parameters.Count), SqlDbType = SqlDbType.Real, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldFloatIncrement(float? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldFloat") == false) _setAs.Add("TestFieldFloat", (olditem, newitem) => olditem.TestFieldFloat = newitem.TestFieldFloat);
				return this.Set("[testFieldFloat]", string.Concat("[testFieldFloat] + @testFieldFloat_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldFloat_", _parameters.Count), SqlDbType = SqlDbType.Real, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldFloatNullable(float? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldFloatNullable") == false) _setAs.Add("TestFieldFloatNullable", (olditem, newitem) => olditem.TestFieldFloatNullable = newitem.TestFieldFloatNullable);
				return this.Set("[testFieldFloatNullable]", string.Concat("@testFieldFloatNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldFloatNullable_", _parameters.Count), SqlDbType = SqlDbType.Real, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldFloatNullableIncrement(float? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldFloatNullable") == false) _setAs.Add("TestFieldFloatNullable", (olditem, newitem) => olditem.TestFieldFloatNullable = newitem.TestFieldFloatNullable);
				return this.Set("[testFieldFloatNullable]", string.Concat("[testFieldFloatNullable] + @testFieldFloatNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldFloatNullable_", _parameters.Count), SqlDbType = SqlDbType.Real, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldGuid(Guid? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldGuid") == false) _setAs.Add("TestFieldGuid", (olditem, newitem) => olditem.TestFieldGuid = newitem.TestFieldGuid);
				return this.Set("[testFieldGuid]", string.Concat("@testFieldGuid_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldGuid_", _parameters.Count), SqlDbType = SqlDbType.UniqueIdentifier, Size = 16, Value = value });
			}
			public SqlUpdateBuild SetTestFieldGuidNullable(Guid? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldGuidNullable") == false) _setAs.Add("TestFieldGuidNullable", (olditem, newitem) => olditem.TestFieldGuidNullable = newitem.TestFieldGuidNullable);
				return this.Set("[testFieldGuidNullable]", string.Concat("@testFieldGuidNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldGuidNullable_", _parameters.Count), SqlDbType = SqlDbType.UniqueIdentifier, Size = 16, Value = value });
			}
			public SqlUpdateBuild SetTestFieldInt(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldInt") == false) _setAs.Add("TestFieldInt", (olditem, newitem) => olditem.TestFieldInt = newitem.TestFieldInt);
				return this.Set("[testFieldInt]", string.Concat("@testFieldInt_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldInt_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldIntIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldInt") == false) _setAs.Add("TestFieldInt", (olditem, newitem) => olditem.TestFieldInt = newitem.TestFieldInt);
				return this.Set("[testFieldInt]", string.Concat("[testFieldInt] + @testFieldInt_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldInt_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldIntNullable(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldIntNullable") == false) _setAs.Add("TestFieldIntNullable", (olditem, newitem) => olditem.TestFieldIntNullable = newitem.TestFieldIntNullable);
				return this.Set("[testFieldIntNullable]", string.Concat("@testFieldIntNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldIntNullable_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldIntNullableIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldIntNullable") == false) _setAs.Add("TestFieldIntNullable", (olditem, newitem) => olditem.TestFieldIntNullable = newitem.TestFieldIntNullable);
				return this.Set("[testFieldIntNullable]", string.Concat("[testFieldIntNullable] + @testFieldIntNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldIntNullable_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldLong(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldLong") == false) _setAs.Add("TestFieldLong", (olditem, newitem) => olditem.TestFieldLong = newitem.TestFieldLong);
				return this.Set("[testFieldLong]", string.Concat("@testFieldLong_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldLong_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldLongIncrement(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldLong") == false) _setAs.Add("TestFieldLong", (olditem, newitem) => olditem.TestFieldLong = newitem.TestFieldLong);
				return this.Set("[testFieldLong]", string.Concat("[testFieldLong] + @testFieldLong_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldLong_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldSByte(byte? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldSByte") == false) _setAs.Add("TestFieldSByte", (olditem, newitem) => olditem.TestFieldSByte = newitem.TestFieldSByte);
				return this.Set("[testFieldSByte]", string.Concat("@testFieldSByte_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldSByte_", _parameters.Count), SqlDbType = SqlDbType.TinyInt, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldSByteIncrement(byte? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldSByte") == false) _setAs.Add("TestFieldSByte", (olditem, newitem) => olditem.TestFieldSByte = newitem.TestFieldSByte);
				return this.Set("[testFieldSByte]", string.Concat("[testFieldSByte] + @testFieldSByte_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldSByte_", _parameters.Count), SqlDbType = SqlDbType.TinyInt, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldSByteNullable(byte? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldSByteNullable") == false) _setAs.Add("TestFieldSByteNullable", (olditem, newitem) => olditem.TestFieldSByteNullable = newitem.TestFieldSByteNullable);
				return this.Set("[testFieldSByteNullable]", string.Concat("@testFieldSByteNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldSByteNullable_", _parameters.Count), SqlDbType = SqlDbType.TinyInt, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldSByteNullableIncrement(byte? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldSByteNullable") == false) _setAs.Add("TestFieldSByteNullable", (olditem, newitem) => olditem.TestFieldSByteNullable = newitem.TestFieldSByteNullable);
				return this.Set("[testFieldSByteNullable]", string.Concat("[testFieldSByteNullable] + @testFieldSByteNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldSByteNullable_", _parameters.Count), SqlDbType = SqlDbType.TinyInt, Size = 1, Value = value });
			}
			public SqlUpdateBuild SetTestFieldShort(short? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldShort") == false) _setAs.Add("TestFieldShort", (olditem, newitem) => olditem.TestFieldShort = newitem.TestFieldShort);
				return this.Set("[testFieldShort]", string.Concat("@testFieldShort_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldShort_", _parameters.Count), SqlDbType = SqlDbType.SmallInt, Size = 2, Value = value });
			}
			public SqlUpdateBuild SetTestFieldShortIncrement(short? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldShort") == false) _setAs.Add("TestFieldShort", (olditem, newitem) => olditem.TestFieldShort = newitem.TestFieldShort);
				return this.Set("[testFieldShort]", string.Concat("[testFieldShort] + @testFieldShort_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldShort_", _parameters.Count), SqlDbType = SqlDbType.SmallInt, Size = 2, Value = value });
			}
			public SqlUpdateBuild SetTestFieldShortNullable(short? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldShortNullable") == false) _setAs.Add("TestFieldShortNullable", (olditem, newitem) => olditem.TestFieldShortNullable = newitem.TestFieldShortNullable);
				return this.Set("[testFieldShortNullable]", string.Concat("@testFieldShortNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldShortNullable_", _parameters.Count), SqlDbType = SqlDbType.SmallInt, Size = 2, Value = value });
			}
			public SqlUpdateBuild SetTestFieldShortNullableIncrement(short? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldShortNullable") == false) _setAs.Add("TestFieldShortNullable", (olditem, newitem) => olditem.TestFieldShortNullable = newitem.TestFieldShortNullable);
				return this.Set("[testFieldShortNullable]", string.Concat("[testFieldShortNullable] + @testFieldShortNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldShortNullable_", _parameters.Count), SqlDbType = SqlDbType.SmallInt, Size = 2, Value = value });
			}
			public SqlUpdateBuild SetTestFieldString(string value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldString") == false) _setAs.Add("TestFieldString", (olditem, newitem) => olditem.TestFieldString = newitem.TestFieldString);
				return this.Set("[testFieldString]", string.Concat("@testFieldString_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldString_", _parameters.Count), SqlDbType = SqlDbType.NVarChar, Size = 255, Value = value });
			}
			public SqlUpdateBuild SetTestFieldTimeSpan(TimeSpan? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldTimeSpan") == false) _setAs.Add("TestFieldTimeSpan", (olditem, newitem) => olditem.TestFieldTimeSpan = newitem.TestFieldTimeSpan);
				return this.Set("[testFieldTimeSpan]", string.Concat("@testFieldTimeSpan_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldTimeSpan_", _parameters.Count), SqlDbType = SqlDbType.Time, Size = 5, Value = value });
			}
			public SqlUpdateBuild SetTestFieldTimeSpanNullable(TimeSpan? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldTimeSpanNullable") == false) _setAs.Add("TestFieldTimeSpanNullable", (olditem, newitem) => olditem.TestFieldTimeSpanNullable = newitem.TestFieldTimeSpanNullable);
				return this.Set("[testFieldTimeSpanNullable]", string.Concat("@testFieldTimeSpanNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldTimeSpanNullable_", _parameters.Count), SqlDbType = SqlDbType.Time, Size = 5, Value = value });
			}
			public SqlUpdateBuild SetTestFieldUInt(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldUInt") == false) _setAs.Add("TestFieldUInt", (olditem, newitem) => olditem.TestFieldUInt = newitem.TestFieldUInt);
				return this.Set("[testFieldUInt]", string.Concat("@testFieldUInt_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldUInt_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldUIntIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldUInt") == false) _setAs.Add("TestFieldUInt", (olditem, newitem) => olditem.TestFieldUInt = newitem.TestFieldUInt);
				return this.Set("[testFieldUInt]", string.Concat("[testFieldUInt] + @testFieldUInt_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldUInt_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldUIntNullable(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldUIntNullable") == false) _setAs.Add("TestFieldUIntNullable", (olditem, newitem) => olditem.TestFieldUIntNullable = newitem.TestFieldUIntNullable);
				return this.Set("[testFieldUIntNullable]", string.Concat("@testFieldUIntNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldUIntNullable_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldUIntNullableIncrement(int? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldUIntNullable") == false) _setAs.Add("TestFieldUIntNullable", (olditem, newitem) => olditem.TestFieldUIntNullable = newitem.TestFieldUIntNullable);
				return this.Set("[testFieldUIntNullable]", string.Concat("[testFieldUIntNullable] + @testFieldUIntNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldUIntNullable_", _parameters.Count), SqlDbType = SqlDbType.Int, Size = 4, Value = value });
			}
			public SqlUpdateBuild SetTestFieldULong(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldULong") == false) _setAs.Add("TestFieldULong", (olditem, newitem) => olditem.TestFieldULong = newitem.TestFieldULong);
				return this.Set("[testFieldULong]", string.Concat("@testFieldULong_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldULong_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldULongIncrement(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldULong") == false) _setAs.Add("TestFieldULong", (olditem, newitem) => olditem.TestFieldULong = newitem.TestFieldULong);
				return this.Set("[testFieldULong]", string.Concat("[testFieldULong] + @testFieldULong_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldULong_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldULongNullable(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldULongNullable") == false) _setAs.Add("TestFieldULongNullable", (olditem, newitem) => olditem.TestFieldULongNullable = newitem.TestFieldULongNullable);
				return this.Set("[testFieldULongNullable]", string.Concat("@testFieldULongNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldULongNullable_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldULongNullableIncrement(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldULongNullable") == false) _setAs.Add("TestFieldULongNullable", (olditem, newitem) => olditem.TestFieldULongNullable = newitem.TestFieldULongNullable);
				return this.Set("[testFieldULongNullable]", string.Concat("[testFieldULongNullable] + @testFieldULongNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldULongNullable_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFieldUShort(short? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldUShort") == false) _setAs.Add("TestFieldUShort", (olditem, newitem) => olditem.TestFieldUShort = newitem.TestFieldUShort);
				return this.Set("[testFieldUShort]", string.Concat("@testFieldUShort_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldUShort_", _parameters.Count), SqlDbType = SqlDbType.SmallInt, Size = 2, Value = value });
			}
			public SqlUpdateBuild SetTestFieldUShortIncrement(short? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldUShort") == false) _setAs.Add("TestFieldUShort", (olditem, newitem) => olditem.TestFieldUShort = newitem.TestFieldUShort);
				return this.Set("[testFieldUShort]", string.Concat("[testFieldUShort] + @testFieldUShort_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldUShort_", _parameters.Count), SqlDbType = SqlDbType.SmallInt, Size = 2, Value = value });
			}
			public SqlUpdateBuild SetTestFieldUShortNullable(short? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldUShortNullable") == false) _setAs.Add("TestFieldUShortNullable", (olditem, newitem) => olditem.TestFieldUShortNullable = newitem.TestFieldUShortNullable);
				return this.Set("[testFieldUShortNullable]", string.Concat("@testFieldUShortNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldUShortNullable_", _parameters.Count), SqlDbType = SqlDbType.SmallInt, Size = 2, Value = value });
			}
			public SqlUpdateBuild SetTestFieldUShortNullableIncrement(short? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFieldUShortNullable") == false) _setAs.Add("TestFieldUShortNullable", (olditem, newitem) => olditem.TestFieldUShortNullable = newitem.TestFieldUShortNullable);
				return this.Set("[testFieldUShortNullable]", string.Concat("[testFieldUShortNullable] + @testFieldUShortNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFieldUShortNullable_", _parameters.Count), SqlDbType = SqlDbType.SmallInt, Size = 2, Value = value });
			}
			public SqlUpdateBuild SetTestFielLongNullable(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFielLongNullable") == false) _setAs.Add("TestFielLongNullable", (olditem, newitem) => olditem.TestFielLongNullable = newitem.TestFielLongNullable);
				return this.Set("[testFielLongNullable]", string.Concat("@testFielLongNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFielLongNullable_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
			public SqlUpdateBuild SetTestFielLongNullableIncrement(long? value) {
				if (_dataSource != null && _setAs.ContainsKey("TestFielLongNullable") == false) _setAs.Add("TestFielLongNullable", (olditem, newitem) => olditem.TestFielLongNullable = newitem.TestFielLongNullable);
				return this.Set("[testFielLongNullable]", string.Concat("[testFielLongNullable] + @testFielLongNullable_", _parameters.Count), 
					new SqlParameter { ParameterName = string.Concat("@testFielLongNullable_", _parameters.Count), SqlDbType = SqlDbType.BigInt, Size = 8, Value = value });
			}
		}
		#endregion

		public Tb_alltypeInfo Insert(Tb_alltypeInfo item) {
			Tb_alltypeInfo newitem = null;
			SqlHelper.ExecuteReader(dr => { newitem = GetItem(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		public List<Tb_alltypeInfo> Insert(IEnumerable<Tb_alltypeInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<Tb_alltypeInfo>();
			List<Tb_alltypeInfo> newitems = new List<Tb_alltypeInfo>();
			SqlHelper.ExecuteReader(dr => { newitems.Add(BLL.Tb_alltype.dal.GetItem(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		public (string sql, SqlParameter[] parms) InsertMakeParam(IEnumerable<Tb_alltypeInfo> items) {
			var itemsArr = items?.Where(a => a != null).ToArray();
			if (itemsArr == null || itemsArr.Any() == false) return (null, null);
			var values = "";
			var parms = new SqlParameter[itemsArr.Length * 38];
			for (var a = 0; a < itemsArr.Length; a++) {
				var item = itemsArr[a];
				values += $",({TSQL.InsertValues.Replace(", ", a + ", ")}{a})";
				var tmparms = GetParameters(item);
				for (var b = 0; b < tmparms.Length; b++) {
					tmparms[b].ParameterName += a;
					parms[a * 38 + b] = tmparms[b];
				}
			}
			return (string.Format(TSQL.InsertMultiFormat, values.Substring(1)), parms);
		}

		#region async
		async public Task<Tb_alltypeInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as Tb_alltypeInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			Tb_alltypeInfo item = new Tb_alltypeInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = await dr.GetFieldValueAsync<int>(dataIndex); if (item.Id == null) { dataIndex += 38; return (null, dataIndex); }
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldBool1111 = await dr.GetFieldValueAsync<bool>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldBoolNullable = await dr.GetFieldValueAsync<bool>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldByte = await dr.GetFieldValueAsync<byte>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldByteNullable = await dr.GetFieldValueAsync<byte>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldBytes = await dr.GetFieldValueAsync<byte[]>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDateTime = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDateTimeNullable = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDateTimeNullableOffset = await dr.GetFieldValueAsync<DateTimeOffset>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDateTimeOffset = await dr.GetFieldValueAsync<DateTimeOffset>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDecimal = await dr.GetFieldValueAsync<decimal>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDecimalNullable = await dr.GetFieldValueAsync<decimal>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDouble = await dr.GetFieldValueAsync<double>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldDoubleNullable = await dr.GetFieldValueAsync<double>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldEnum1 = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldEnum1Nullable = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldEnum2 = await dr.GetFieldValueAsync<long>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldEnum2Nullable = await dr.GetFieldValueAsync<long>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldFloat = await dr.GetFieldValueAsync<float>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldFloatNullable = await dr.GetFieldValueAsync<float>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldGuid = await dr.GetFieldValueAsync<Guid>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldGuidNullable = await dr.GetFieldValueAsync<Guid>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldInt = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldIntNullable = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldLong = await dr.GetFieldValueAsync<long>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldSByte = await dr.GetFieldValueAsync<byte>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldSByteNullable = await dr.GetFieldValueAsync<byte>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldShort = await dr.GetFieldValueAsync<short>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldShortNullable = await dr.GetFieldValueAsync<short>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldString = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldTimeSpan = await dr.GetFieldValueAsync<TimeSpan>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldTimeSpanNullable = await dr.GetFieldValueAsync<TimeSpan>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldUInt = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldUIntNullable = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldULong = await dr.GetFieldValueAsync<long>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldULongNullable = await dr.GetFieldValueAsync<long>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldUShort = await dr.GetFieldValueAsync<short>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFieldUShortNullable = await dr.GetFieldValueAsync<short>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.TestFielLongNullable = await dr.GetFieldValueAsync<long>(dataIndex);
			return (item, dataIndex);
		}
		async public Task<Tb_alltypeInfo> DeleteAsync(int? Id) {
			Tb_alltypeInfo item = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { item = await BLL.Tb_alltype.dal.GetItemAsync(dr); }, string.Concat(TSQL.Delete, @"[Id] = @Id"),
				new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Size = 4, Value = Id });
			return item;
		}
		async public Task<Tb_alltypeInfo> InsertAsync(Tb_alltypeInfo item) {
			Tb_alltypeInfo newitem = null;
			await SqlHelper.ExecuteReaderAsync(async dr => { newitem = await GetItemAsync(dr); }, TSQL.Insert, GetParameters(item));
			if (newitem == null) return null;
			this.CopyItemAllField(item, newitem);
			return item;
		}
		async public Task<List<Tb_alltypeInfo>> InsertAsync(IEnumerable<Tb_alltypeInfo> items) {
			var mp = InsertMakeParam(items);
			if (string.IsNullOrEmpty(mp.sql)) return new List<Tb_alltypeInfo>();
			List<Tb_alltypeInfo> newitems = new List<Tb_alltypeInfo>();
			await SqlHelper.ExecuteReaderAsync(async dr => { newitems.Add(await BLL.Tb_alltype.dal.GetItemAsync(dr)); }, mp.sql, mp.parms);
			return newitems;
		}
		#endregion
	}
}