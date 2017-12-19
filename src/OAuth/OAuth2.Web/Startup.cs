using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Web.Code;
using AlwaysMoveForward.OAuth2.Web.Code.IdentityServer;
using AlwaysMoveForward.OAuth2.Web.Code.AspNetIdentity;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Serilog;
using AlwaysMoveForward.Core.Common.Configuration;
using AlwaysMoveForward.Core.Common.Encryption;
using System;

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
            services.AddTransient<IUserStore<AMFUserLogin>, UserStore > ();
            services.AddTransient<IUserPasswordStore<AMFUserLogin>, UserStore>();
            services.AddTransient<IRoleStore<string>, RoleStore>();
            services.AddScoped<IUserClaimsPrincipalFactory<AMFUserLogin>, ClaimsPrincipalFactory>();

            services.AddIdentity<AMFUserLogin, string>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 8;
            })
            .AddDefaultTokenProviders();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            DatabaseConfiguration dbConfiguration = new DatabaseConfiguration();
            Configuration.GetSection("Database").Bind(dbConfiguration);

            // Adds IdentityServer
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddConfigurationStore(options=>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(dbConfiguration.ConnectionString);
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(dbConfiguration.ConnectionString);

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>()
                .AddProfileService<ProfileService>()
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
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
