using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Web.Code;
using AlwaysMoveForward.OAuth2.Web.Code.IdentityServer;
using AlwaysMoveForward.OAuth2.Web.Code.AspNetIdentity;
using AlwaysMoveForward.OAuth2.Common.Configuration;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using IdentityServer4;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Serilog;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AlwaysMoveForward.OAuth2.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.LiterateConsole()
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, @"c:\personal\log-{Date}.txt"))           
                .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseConfiguration>(Configuration.GetSection("Database"));

            // Add framework services.
            services.AddMvc();

            services.AddScoped<ServiceManagerBuilder>();
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddTransient<IClientStore, ClientStore>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IResourceStore, ResourceStore>();
            services.AddTransient<IUserStore<AMFUserLogin>, UserStore > ();
            services.AddTransient<IUserPasswordStore<AMFUserLogin>, UserStore>();
            services.AddTransient<IRoleStore<string>, RoleStore>();
            services.AddTransient<IResourceOwnerPasswordValidator, AMFPasswordValidator>();

            services.AddIdentity<AMFUserLogin, string> (o => {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 1;
            })
            .AddDefaultTokenProviders();

            // Adds IdentityServer
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryPersistedGrants()
                .AddClientStore<ClientStore>()
                .AddProfileService<ProfileService>()
                .AddResourceStore<ResourceStore>()
                .AddAspNetIdentity<AMFUserLogin>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }          

            loggerFactory.AddSerilog(Log.Logger);

            app.UseIdentity();

            // Adds IdentityServer
            app.UseIdentityServer();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
