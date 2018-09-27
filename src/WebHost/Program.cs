using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace es.WebHost {
	public class Program {
		public static void Main(string[] args) {
			var config = new ConfigurationBuilder()
				.AddCommandLine(args)
				.Build();

			//dotnet run --urls=http://0.0.0.0:5000
			var host = new WebHostBuilder()
				.UseConfiguration(config)
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}
