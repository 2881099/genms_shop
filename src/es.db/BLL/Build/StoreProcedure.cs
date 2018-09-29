using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace es.BLL {

	public partial class StoreProcedure {

		#region Sp_test
		public static void Sp_test(int? p1, out int? p2) => Sp_test(p1, out p2, false);
		public static List<object[][]> Sp_testReturn(int? p1, out int? p2) => Sp_test(p1, out p2, true);
		private static List<object[][]> Sp_test(int? p1, out int? p2, bool isReturn) {
			p2 = null;
			SqlParameter parmO0 = null;
			var sqlParams = new[] {
				new SqlParameter { ParameterName = "@p1", SqlDbType = SqlDbType.Int, Size = 4, Value = p1 },
				parmO0 = new SqlParameter { ParameterName = "@p2", SqlDbType = SqlDbType.Int, Size = 4, Value = p2 }
			};
			parmO0.Direction = ParameterDirection.Output;
			List<object[][]> ds = null;

			if (isReturn) ds = ExecuteArrayAll(@"[dbo].[sp_test]", sqlParams);
			else SqlHelper.Instance.ExecuteNonQuery(CommandType.StoredProcedure, @"[dbo].[sp_test]", sqlParams);

			if (parmO0.Value != DBNull.Value) p2 = (int?)parmO0.Value;
			
			return ds;
		}
		#endregion

		/// <summary>
		/// 执行存储过程，可能有多个结果集返回
		/// </summary>
		/// <param name="procedure">存储过程</param>
		/// <param name="sqlParams">参数</param>
		/// <returns></returns>
		public static List<object[][]> ExecuteArrayAll(string procedure, params SqlParameter[] sqlParams) {
			var ds = new List<object[][]>();
			SqlHelper.Instance.ExecuteReader(dr => {
				while (true) {
					var dt = new List<object[]>();
					while (dr.Read()) {
						object[] values = new object[dr.FieldCount];
						dr.GetValues(values);
						dt.Add(values);
					}
					ds.Add(dt.ToArray());
					if (dr.NextResult() == false) break;
				}
			}, CommandType.StoredProcedure, procedure, sqlParams);
			return ds;
		}
	}
}