using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public ContentResult Get_connection() {
			var sb = new StringBuilder();
			var pools = new List<System.Data.SqlClient.SqlConnectionPool>();
			pools.Add(SqlHelper.Pool);
			pools.AddRange(SqlHelper.SlavePools);
			for (var a = 0; a < pools.Count; a++) {
				var pool = pools[a];
				sb.AppendLine($@"【{pool.Policy.Name}】　　状态：{(pool.IsAvailable ? "正常" : $"[{pool.UnavailableTime}] {pool.UnavailableException.Message}")}
-------------------------------------------------------------------------------------------------------
{pool.StatisticsFullily}
");
			}
			return new ContentResult { ContentType = "text/plan;charset=utf-8", Content = sb.ToString() };
		}
		[HttpGet(@"connection/redis")]
		public ContentResult Get_connection_redis() {
			var sb = new StringBuilder();
			foreach(var pool in RedisHelper.Nodes.Values) {
				sb.AppendLine($@"【{pool.Policy.Name}】　　状态：{(pool.IsAvailable ? "正常" : $"[{pool.UnavailableTime}] {pool.UnavailableException.Message}")}
-------------------------------------------------------------------------------------------------------
Slots：{RedisHelper.Instance.SlotCache.Count}/16384, {pool.StatisticsFullily}
");
			}
			return new ContentResult { ContentType = "text/plan;charset=utf-8", Content = sb.ToString() };
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

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "Sys_Area", 5, "/Sys_Area/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/Sys_Area/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/Sys_Area/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/Sys_Area/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/Sys_Area/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "tag", 6, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/tag/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/tag/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/tag/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/tag/del");

			dir2 = Sysdir.Insert(dir1.Id, DateTime.Now, "test", 7, "/test/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "列表", 1, "/test/");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "添加", 2, "/test/add");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "编辑", 3, "/test/edit");
			dir3 = Sysdir.Insert(dir2.Id, DateTime.Now, "删除", 4, "/test/del");
			*/
			return new APIReturn(0, "管理目录已初始化完成。");
		}
	}
}
