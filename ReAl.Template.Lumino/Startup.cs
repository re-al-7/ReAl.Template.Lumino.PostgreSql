using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReAl.Template.Lumino.Helpers;
using ReAl.Template.Lumino.Models;

namespace ReAl.Template.Lumino
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Use a PostgreSQL database
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");
 
            services.AddDbContext<db_seguridadContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("ReAl.Template.Lumino.Postgres")
                )
            );
            
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            //Auth Cookie
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                    options.LoginPath = new PathString("/"));

            services.AddMvc()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("es"), new CultureInfo("es") };
                options.DefaultRequestCulture = new RequestCulture("es", "es");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            //KB: https://andrewlock.net/an-introduction-to-session-storage-in-asp-net-core/
            //Session
            services.AddDistributedMemoryCache();
            services.AddSession();
            
            //Compression
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    "text/javascript",
                    // Custom
                    "image/svg+xml"
                };                
                options.EnableForHttps = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Internationalizacion
            //KB: https://stackoverflow.com/questions/40828570/asp-net-core-model-binding-error-messages-localization
            var supportedCultures = new[] { new CultureInfo("es"), new CultureInfo("es") };
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("es")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Account/Error");
            }

            //Compression
            app.UseResponseCompression();
            
            app.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = content =>
                {
                    if(content.File.Name.EndsWith(".js.gz"))
                    {
                        content.Context.Response.Headers["Content-Type"] = "text/javascript";
                        content.Context.Response.Headers["Content-Encoding"] = "gzip";
                    }
                    if(content.File.Name.EndsWith(".css.gz"))
                    {
                        content.Context.Response.Headers["Content-Type"] = "text/css";
                        content.Context.Response.Headers["Content-Encoding"] = "gzip";
                    }
                }
            });

            //Adds the authentication middleware to the pipeline
            app.UseAuthentication();

            //enable session before MVC
            app.UseSession();            
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

        }
    }
}
