using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AlwaysMoveForward.OAuth2.Web2.Services;
using AlwaysMoveForward.OAuth2.Common.Configuration;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Web.Code.IdentityServer;
using AlwaysMoveForward.OAuth2.Web.Code;
using Microsoft.AspNetCore.Identity;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using IdentityServer4.Stores;
using IdentityServer4.Services;
using AlwaysMoveForward.OAuth2.Web.Code.AspNetIdentity;
using IdentityServer4.Validation;
using Serilog;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IO;

namespace AlwaysMoveForward.OAuth2.Web2
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
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
            services.AddScoped<IUserStore<AMFUserLogin>, UserStore>();
            services.AddScoped<IUserPasswordStore<AMFUserLogin>, UserStore>();
            services.AddScoped<IRoleStore<string>, RoleStore>();
            services.AddTransient<IResourceOwnerPasswordValidator, AMFPasswordValidator>();

            services.AddIdentity<AMFUserLogin, string>(o => {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 1;
            })
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = SiteConstants.AuthenticationScheme;
                options.DefaultChallengeScheme = SiteConstants.AuthenticationScheme;
                options.DefaultSignOutScheme = SiteConstants.AuthenticationScheme;
            })
            .AddCookie(options => {
                options.LoginPath = "/Account/LogIn";
                options.LogoutPath = "/Account/LogOff";

            });

            services.AddMvc();

            // Adds IdentityServer
            services.AddIdentityServer()
                .AddInMemoryPersistedGrants()
                .AddClientStore<ClientStore>()
                .AddProfileService<ProfileService>()
                .AddResourceStore<ResourceStore>()
                .AddAspNetIdentity<AMFUserLogin>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
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

            app.UseAuthentication();

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
