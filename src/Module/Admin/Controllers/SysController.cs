using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using es.BLL;
using es.Model;

namespace es.Module.Admin.Controllers {
	[Route("[controller]")]
	[Obsolete]
	public class SysController : Controller {
		[HttpGet(@"connection")]
		public object Get_connection() {
			var pools = new List<System.Data.SqlClient.ConnectionPool>();
			pools.Add(SqlHelper.Pool);
			pools.AddRange(SqlHelper.SlavePools);

			var ret = new List<object>();
			for (var a = 0; a < pools.Count; a++) {
				var pool = pools[a];
				List<Hashtable> list = new List<Hashtable>();
				foreach (var conn in pool.AllConnections) {
					list.Add(new Hashtable() {
						{ "数据库", conn.SqlConnection.Database },
						{ "状态", conn.SqlConnection.State },
						{ "最后活动", conn.LastActive },
						{ "获取次数", conn.UseSum }
					});
				}
				ret.Add(new {
					Key = a == 0 ? "【主库】" : $"【从库{a - 1}】",
					IsAvailable = pool.IsAvailable,
					UnavailableTime = pool.UnavailableTime,
					FreeConnections = pool.FreeConnections.Count,
					AllConnections = pool.AllConnections.Count,
					GetConnectionQueue = pool.GetConnectionQueue.Count,
					GetConnectionAsyncQueue = pool.GetConnectionAsyncQueue.Count,
					List = list
				});
			}
			return ret;
		}
		[HttpGet(@"connection/redis")]
		public object Get_connection_redis() {
			var ret = new Hashtable();
			foreach(var pool in RedisHelper.ClusterNodes) {
				List<Hashtable> list = new List<Hashtable>();
				foreach (var conn in pool.Value.AllConnections) {
					list.Add(new Hashtable() {
						{ "最后活动", conn.LastActive },
						{ "获取次数", conn.UseSum }
					});
				}
				ret.Add(pool.Key, new {
					FreeConnections = pool.Value.FreeConnections.Count,
					AllConnections = pool.Value.AllConnections.Count,
					GetConnectionQueue = pool.Value.GetConnectionQueue.Count,
					GetConnectionAsyncQueue = pool.Value.GetConnectionAsyncQueue.Count,
					List = list
				});
			}
			return ret;
		}

		[HttpGet(@"init_sysdir")]
		public APIReturn Get_init_sysdir() {
			/*
			if (Sysdir.SelectByParent_id(null).Count() > 0)
				return new APIReturn(-33, "本系统已经初始化过，页面没经过任何操作退出。");

			SysdirInfo dir1, dir2, dir3;
			dir1 = Sysdir.Insert(null, DateTime.Now, "运营管理", 1, null);

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "category", 1, "/category/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/category/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/category/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/category/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/category/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "comment", 2, "/comment/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/comment/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/comment/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/comment/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/comment/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "goods", 3, "/goods/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/goods/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/goods/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/goods/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/goods/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "goods_tag", 4, "/goods_tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/goods_tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/goods_tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/goods_tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/goods_tag/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tag", 5, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tag/del");
			*/
			return new APIReturn(0, "管理目录已初始化完成。");
		}
	}
}
