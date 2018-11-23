using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace es.WebHost {
	public class Startup {
		public Startup(IHostingEnvironment env) {
			var builder = new ConfigurationBuilder()
				.LoadInstalledModules(Modules, env)
				.AddCustomizedJsonFile(Modules, env, "/var/webos/es/");

			this.Configuration = builder.AddEnvironmentVariables().Build();
			this.env = env;

			Newtonsoft.Json.JsonConvert.DefaultSettings = () => {
				var st = new Newtonsoft.Json.JsonSerializerSettings();
				st.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
				st.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
				st.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.RoundtripKind;
				return st;
			};
			//去掉以下注释可开启 RedisHelper 静态类
			//var csredis = new CSRedis.CSRedisClient(Configuration["ConnectionStrings:redis1"]); //单redis节点模式
			//RedisHelper.Initialization(csredis);
		}

		public static List<ModuleInfo> Modules = new List<ModuleInfo>();
		public IConfiguration Configuration { get; }
		public IHostingEnvironment env { get; }

		public void ConfigureServices(IServiceCollection services) {
			//下面这行代码依赖redis-server，注释后系统将以memory作为缓存存储的介质
			//services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
			services.AddSingleton<IConfiguration>(Configuration);
			services.AddSingleton<IHostingEnvironment>(env);
			services.AddScoped<CustomExceptionFilter>();

			services.AddSession(a => {
				a.IdleTimeout = TimeSpan.FromMinutes(30);
				a.Cookie.Name = "Session_es";
			});
			services.AddCors(options => options.AddPolicy("cors_all", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
			services.AddCustomizedMvc(Modules);
			Modules.ForEach(module => module.Initializer?.ConfigureServices(services, env));

			if (env.IsDevelopment())
				services.AddCustomizedSwaggerGen();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime lifetime) {
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Console.OutputEncoding = Encoding.GetEncoding("GB2312");
			Console.InputEncoding = Encoding.GetEncoding("GB2312");

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddNLog().AddDebug();
			NLog.LogManager.LoadConfiguration("nlog.config");

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			es.BLL.SqlHelper.Initialization(app.ApplicationServices.GetService<IDistributedCache>(), Configuration.GetSection("es_BLL_ITEM_CACHE"),
				Configuration["ConnectionStrings:es_mssql"], /* 此参数可以配置【从数据库】 */ null, loggerFactory.CreateLogger("es_DAL_sqlhelper"));

			app.UseSession();
			app.UseCors("cors_all");
			app.UseMvc();
			app.UseCustomizedStaticFiles(Modules);
			Modules.ForEach(module => module.Initializer?.Configure(app, env, loggerFactory, lifetime));

			if (env.IsDevelopment())
				app.UseCustomizedSwagger(env);
		}
	}
}
