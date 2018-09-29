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

	public partial class V_testddd : IDAL {
		#region transact-sql define
		public string Table { get { return TSQL.Table; } }
		public string Field { get { return TSQL.Field; } }
		public string Sort { get { return TSQL.Sort; } }
		internal class TSQL {
			internal static readonly string Table = "[dbo].[v_testddd]";
			internal static readonly string Field = "a.[category_id], a.[content], a.[create_time], a.[id], a.[imgs], a.[name], a.[stock], a.[title], a.[update_time]";
			internal static readonly string Sort = "";
			internal static readonly string Delete = "DELETE FROM [dbo].[v_testddd] OUTPUT " + Field.Replace(@"a.[", @"DELETED.[") + "WHERE ";
			internal static readonly string InsertField = "[category_id], [content], [create_time], [id], [imgs], [name], [stock], [title], [update_time]";
			internal static readonly string InsertValues = "@category_id, @content, @create_time, @id, @imgs, @name, @stock, @title, @update_time";
			internal static readonly string InsertMultiFormat = "INSERT INTO [dbo].[v_testddd](" + InsertField + ") OUTPUT " + Field.Replace(@"a.[", @"INSERTED.[") + " VALUES{0}";
			internal static readonly string Insert = string.Format(InsertMultiFormat, $"({InsertValues})");
		}
		#endregion

		#region common call
		protected static SqlParameter[] GetParameters(V_testdddInfo item) {
			return new SqlParameter[] {
				new SqlParameter { ParameterName = "@category_id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Category_id }, 
				new SqlParameter { ParameterName = "@content", SqlDbType = SqlDbType.NVarChar, Size = -1, Value = item.Content }, 
				new SqlParameter { ParameterName = "@create_time", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Create_time }, 
				new SqlParameter { ParameterName = "@id", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Id }, 
				new SqlParameter { ParameterName = "@imgs", SqlDbType = SqlDbType.NVarChar, Size = 1024, Value = item.Imgs }, 
				new SqlParameter { ParameterName = "@name", SqlDbType = SqlDbType.NVarChar, Size = 128, Value = item.Name }, 
				new SqlParameter { ParameterName = "@stock", SqlDbType = SqlDbType.Int, Size = 4, Value = item.Stock }, 
				new SqlParameter { ParameterName = "@title", SqlDbType = SqlDbType.NVarChar, Size = 256, Value = item.Title }, 
				new SqlParameter { ParameterName = "@update_time", SqlDbType = SqlDbType.DateTime, Size = 8, Value = item.Update_time }
			};
		}
		public V_testdddInfo GetItem(SqlDataReader dr) {
			int dataIndex = -1;
			return GetItem(dr, ref dataIndex) as V_testdddInfo;
		}
		public object GetItem(SqlDataReader dr, ref int dataIndex) {
			V_testdddInfo item = new V_testdddInfo();
			if (!dr.IsDBNull(++dataIndex)) item.Category_id = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Content = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Create_time = dr.GetDateTime(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Id = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Imgs = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Name = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Stock = dr.GetInt32(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Title = dr.GetString(dataIndex);
			if (!dr.IsDBNull(++dataIndex)) item.Update_time = dr.GetDateTime(dataIndex);
			return item;
		}
		private void CopyItemAllField(V_testdddInfo item, V_testdddInfo newitem) {
			item.Category_id = newitem.Category_id;
			item.Content = newitem.Content;
			item.Create_time = newitem.Create_time;
			item.Id = newitem.Id;
			item.Imgs = newitem.Imgs;
			item.Name = newitem.Name;
			item.Stock = newitem.Stock;
			item.Title = newitem.Title;
			item.Update_time = newitem.Update_time;
		}
		#endregion

		#region async
		async public Task<V_testdddInfo> GetItemAsync(SqlDataReader dr) {
			var read = await GetItemAsync(dr, -1);
			return read.result as V_testdddInfo;
		}
		async public Task<(object result, int dataIndex)> GetItemAsync(SqlDataReader dr, int dataIndex) {
			V_testdddInfo item = new V_testdddInfo();
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Category_id = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Content = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Create_time = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Id = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Imgs = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Name = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Stock = await dr.GetFieldValueAsync<int>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Title = await dr.GetFieldValueAsync<string>(dataIndex);
			if (!await dr.IsDBNullAsync(++dataIndex)) item.Update_time = await dr.GetFieldValueAsync<DateTime>(dataIndex);
			return (item, dataIndex);
		}
		#endregion
	}
}