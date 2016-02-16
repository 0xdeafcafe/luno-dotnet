using Luno;
using Luno.Connections;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace LunoTodoApp
{
	public class Startup
	{
		public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
		{
			// Setup configuration sources.
			var configuration = new ConfigurationBuilder()
				.SetBasePath(appEnv.ApplicationBasePath)
				.AddJsonFile("config.json")
				.AddUserSecrets();

			Configuration = configuration.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		public void ConfigureServices(IServiceCollection services)
		{
			// Create an instance of the Luno Client
			services.AddSingleton<LunoClient>(s => new LunoClient(new ApiKeyConnection(
				Configuration.Get<string>("Luno:ApiKey"), 
				Configuration.Get<string>("Luno:SecretKey"))));

			services.AddCaching();
			services.AddSession();
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
			else
				app.UseExceptionHandler("/Home/Error");

			app.UseStaticFiles();
			app.UseSession();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
