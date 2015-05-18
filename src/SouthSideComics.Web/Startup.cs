using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Diagnostics;
using SouthSideComics.Core.Mongo;
using SouthSideComics.Core.Common;

namespace SouthSideComicsWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources
            var configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddUserSecrets()
                .AddEnvironmentVariables();
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<Config>(Configuration.GetSubKey("Data:Config"))                        
                .AddMvc()            
                .AddTransient<ItemMapper>()
                .AddTransient<PersonMapper>()
                .AddTransient<PublisherMapper>()
                .AddTransient<SouthSideComics.Core.Elasticsearch.SearchItemMapper>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Add the console logger.
            loggerFactory.AddConsole(minLevel: LogLevel.Warning);

            // Add the following to the request pipeline only in development environment.
            if (env.IsEnvironment("Development"))
            {                
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // sends the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });                
            });
        }
    }
}
