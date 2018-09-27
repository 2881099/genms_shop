using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

public static class StarupExtensions {
	public static ConfigurationBuilder LoadInstalledModules(this ConfigurationBuilder build, IList<ModuleInfo> modules, IHostingEnvironment env) {
		var moduleRootFolder = new DirectoryInfo(Path.Combine(env.ContentRootPath, "Module"));
		var moduleFolders = moduleRootFolder.GetDirectories();

		foreach (var moduleFolder in moduleFolders) {
			Assembly assembly;
			try {
				assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(moduleFolder.FullName, moduleFolder.Name + ".dll"));
			} catch (FileLoadException) {
				throw;
			}
			if (assembly.FullName.Contains(moduleFolder.Name))
				modules.Add(new ModuleInfo {
					Name = moduleFolder.Name,
					Assembly = assembly,
					Path = moduleFolder.FullName
				});
		}

		return build;
	}

	public static ConfigurationBuilder AddCustomizedJsonFile(this ConfigurationBuilder build, IList<ModuleInfo> modules, IHostingEnvironment env, string productPath) {
		build.SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", true, true);
		foreach (var module in modules) {
			var jsonpath = $"Module/{module.Name}/appsettings.json";
			if (File.Exists(Path.Combine(env.ContentRootPath, jsonpath)))
				build.AddJsonFile(jsonpath, true, true);
		}
		if (env.IsProduction()) {
			build.AddJsonFile(Path.Combine(productPath, "appsettings.json"), true, true);
			foreach (var module in modules) {
				var jsonpath = Path.Combine(productPath, $"Module_{module.Name}_appsettings.json");
				if (File.Exists(Path.Combine(env.ContentRootPath, jsonpath)))
					build.AddJsonFile(jsonpath, true, true);
			}
		}
		return build;
	}

	public static IServiceCollection AddCustomizedMvc(this IServiceCollection services, IList<ModuleInfo> modules) {
		var mvcBuilder = services.AddMvc().AddJsonOptions(a => {
				a.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
				a.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
				a.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
			})
			.AddRazorOptions(o => {
				foreach (var module in modules) {
					var a = MetadataReference.CreateFromFile(module.Assembly.Location);
					o.AdditionalCompilationReferences.Add(a);
				}
			})
			.AddViewLocalization()
			.AddDataAnnotationsLocalization();

		foreach (var module in modules)
			mvcBuilder.AddApplicationPart(module.Assembly);

		return services;
	}

	public static IApplicationBuilder UseCustomizedMvc(this IApplicationBuilder app, IList<ModuleInfo> modules) {
		foreach (var module in modules) {
			var moduleInitializerType =
				module.Assembly.GetTypes().FirstOrDefault(x => typeof(IModuleInitializer).IsAssignableFrom(x));
			if ((moduleInitializerType != null) && (moduleInitializerType != typeof(IModuleInitializer))) {
				var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
				moduleInitializer.Init(app);
			}
		}
		return app.UseMvc();
	}
	public static IApplicationBuilder UseCustomizedStaticFiles(this IApplicationBuilder app, IList<ModuleInfo> modules) {
		app.UseDefaultFiles();
		app.UseStaticFiles(new StaticFileOptions() {
			OnPrepareResponse = (context) => {
				var headers = context.Context.Response.GetTypedHeaders();
				headers.CacheControl = new CacheControlHeaderValue() {
					Public = true,
					MaxAge = TimeSpan.FromDays(60)
				};
			}
		});
		return app;
	}
}
