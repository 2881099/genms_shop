using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace es.BLL {
	/// <summary>
	/// dng.Mssql代理类
	/// </summary>
	public abstract partial class SqlHelper : es.DAL.SqlHelper {
	}
}

namespace es.DAL {
	/// <summary>
	/// dng.Mssql代理类
	/// </summary>
	public abstract partial class SqlHelper {
		internal static Executer Instance { get; private set; }
		public static SqlConnectionPool Pool => Instance.MasterPool;
		public static List<SqlConnectionPool> SlavePools => Instance.SlavePools;
		/// <summary>
		/// 是否跟踪记录SQL执行性能日志
		/// </summary>
		public static bool IsTracePerformance { get => Instance.IsTracePerformance; set => Instance.IsTracePerformance = value; }
		public static void Initialization(IDistributedCache cache, IConfiguration cacheStrategy, string masterConnectionString, string[] slaveConnectionString, ILogger log) {
			CacheStrategy = cacheStrategy;
			Instance = new Executer(cache, masterConnectionString, slaveConnectionString, log);
		}

		public static string Addslashes(string filter, params object[] parms) { return Executer.Addslashes(filter, parms); }

		/// <summary>
		/// 若使用读写分离，查询【从库】条件cmdText.StartsWith("SELECT ")，否则查询【主库】
		/// </summary>
		/// <param name="readerHander"></param>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		public static void ExecuteReader(Action<SqlDataReader> readerHander, string cmdText, params SqlParameter[] cmdParms) => Instance.ExecuteReader(readerHander, CommandType.Text, cmdText, cmdParms);
		/// <summary>
		/// 若使用读写分离，查询【从库】条件cmdText.StartsWith("SELECT ")，否则查询【主库】
		/// </summary>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		public static object[][] ExecuteArray(string cmdText, params SqlParameter[] cmdParms) => Instance.ExecuteArray(CommandType.Text, cmdText, cmdParms);
		/// <summary>
		/// 在【主库】执行
		/// </summary>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		public static int ExecuteNonQuery(string cmdText, params SqlParameter[] cmdParms) => Instance.ExecuteNonQuery(CommandType.Text, cmdText, cmdParms);
		/// <summary>
		/// 在【主库】执行
		/// </summary>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		public static object ExecuteScalar(string cmdText, params SqlParameter[] cmdParms) => Instance.ExecuteScalar(CommandType.Text, cmdText, cmdParms);

		/// <summary>
		/// 若使用读写分离，查询【从库】条件cmdText.StartsWith("SELECT ")，否则查询【主库】
		/// </summary>
		/// <param name="readerHander"></param>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		public static Task ExecuteReaderAsync(Func<SqlDataReader, Task> readerHander, string cmdText, params SqlParameter[] cmdParms) => Instance.ExecuteReaderAsync(readerHander, CommandType.Text, cmdText, cmdParms);
		/// <summary>
		/// 若使用读写分离，查询【从库】条件cmdText.StartsWith("SELECT ")，否则查询【主库】
		/// </summary>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		public static Task<object[][]> ExecuteArrayAsync(string cmdText, params SqlParameter[] cmdParms) => Instance.ExecuteArrayAsync(CommandType.Text, cmdText, cmdParms);
		/// <summary>
		/// 在【主库】执行
		/// </summary>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		public static Task<int> ExecuteNonQueryAsync(string cmdText, params SqlParameter[] cmdParms) => Instance.ExecuteNonQueryAsync(CommandType.Text, cmdText, cmdParms);
		/// <summary>
		/// 在【主库】执行
		/// </summary>
		/// <param name="cmdText"></param>
		/// <param name="cmdParms"></param>
		public static Task<object> ExecuteScalarAsync(string cmdText, params SqlParameter[] cmdParms) => Instance.ExecuteScalarAsync(CommandType.Text, cmdText, cmdParms);

		/// <summary>
		/// 开启事务（不支持异步），60秒未执行完将自动提交
		/// </summary>
		/// <param name="handler">事务体 () => {}</param>
		public static void Transaction(Action handler) => Instance.Transaction(handler);
		/// <summary>
		/// 开启事务（不支持异步）
		/// </summary>
		/// <param name="handler">事务体 () => {}</param>
		/// <param name="timeout">超时，未执行完将自动提交</param>
		public static void Transaction(Action handler, TimeSpan timeout) => Instance.Transaction(handler, timeout);

		/// <summary>
		/// 生成类似Mongodb的ObjectId有序、不重复Guid
		/// </summary>
		/// <returns></returns>
		public static Guid NewMongodbId() => Executer.NewMongodbId();

		/// <summary>
		/// 循环或批量删除缓存键，项目启动时检测：Cache.Remove("key1|key2") 若成功删除 key1、key2，说明支持批量删除
		/// </summary>
		/// <param name="keys">缓存键[数组]</param>
		public static void CacheRemove(params string[] keys) => Instance.CacheRemove(keys);
		/// <summary>
		/// 循环或批量删除缓存键，项目启动时检测：Cache.Remove("key1|key2") 若成功删除 key1、key2，说明支持批量删除
		/// </summary>
		/// <param name="keys">缓存键[数组]</param>
		async static public Task CacheRemoveAsync(params string[] keys) => await Instance.CacheRemoveAsync(keys);
		public static IDistributedCache Cache => Instance.Cache;
		internal static IConfiguration CacheStrategy { get; private set; }

		/// <summary>
		/// 缓存壳
		/// </summary>
		/// <typeparam name="T">缓存类型</typeparam>
		/// <param name="key">缓存键</param>
		/// <param name="timeoutSeconds">缓存秒数</param>
		/// <param name="getData">获取源数据的函数</param>
		/// <param name="serialize">序列化函数</param>
		/// <param name="deserialize">反序列化函数</param>
		/// <returns></returns>
		public static T CacheShell<T>(string key, int timeoutSeconds, Func<T> getData, Func<T, string> serialize = null, Func<string, T> deserialize = null) => 
			Instance.CacheShell(key, timeoutSeconds, getData, serialize, deserialize);
		/// <summary>
		/// 缓存壳(哈希表)
		/// </summary>
		/// <typeparam name="T">缓存类型</typeparam>
		/// <param name="key">缓存键</param>
		/// <param name="field">字段</param>
		/// <param name="timeoutSeconds">缓存秒数</param>
		/// <param name="getData">获取源数据的函数</param>
		/// <param name="serialize">序列化函数</param>
		/// <param name="deserialize">反序列化函数</param>
		/// <returns></returns>
		public static T CacheShell<T>(string key, string field, int timeoutSeconds, Func<T> getData, Func<(T, long), string> serialize = null, Func<string, (T, long)> deserialize = null) =>
			Instance.CacheShell(key, field, timeoutSeconds, getData, serialize, deserialize);
		/// <summary>
		/// 缓存壳
		/// </summary>
		/// <typeparam name="T">缓存类型</typeparam>
		/// <param name="key">缓存键</param>
		/// <param name="timeoutSeconds">缓存秒数</param>
		/// <param name="getDataAsync">获取源数据的函数</param>
		/// <param name="serialize">序列化函数</param>
		/// <param name="deserialize">反序列化函数</param>
		/// <returns></returns>
		async public static Task<T> CacheShellAsync<T>(string key, int timeoutSeconds, Func<Task<T>> getDataAsync, Func<T, string> serialize = null, Func<string, T> deserialize = null) =>
			await Instance.CacheShellAsync(key, timeoutSeconds, getDataAsync, serialize, deserialize);
		/// <summary>
		/// 缓存壳(哈希表)
		/// </summary>
		/// <typeparam name="T">缓存类型</typeparam>
		/// <param name="key">缓存键</param>
		/// <param name="field">字段</param>
		/// <param name="timeoutSeconds">缓存秒数</param>
		/// <param name="getDataAsync">获取源数据的函数</param>
		/// <param name="serialize">序列化函数</param>
		/// <param name="deserialize">反序列化函数</param>
		/// <returns></returns>
		async public static Task<T> CacheShellAsync<T>(string key, string field, int timeoutSeconds, Func<Task<T>> getDataAsync, Func<(T, long), string> serialize = null, Func<string, (T, long)> deserialize = null) =>
			await Instance.CacheShellAsync(key, field, timeoutSeconds, getDataAsync, serialize, deserialize);
	}
}