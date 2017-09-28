﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Web.Code;
using AlwaysMoveForward.OAuth2.Common.Configuration;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using System.IdentityModel.Tokens.Jwt;
using IdentityServer4.Configuration;
using IdentityServer4.Stores;
using Serilog;
using System.IO;

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

            services.AddTransient<IClientStore, AMFClientStore>();
            services.AddTransient<IProfileService, AMFProfileService>();
            services.AddTransient<IResourceStore, AMFResourceStore>();
            services.AddTransient<IResourceOwnerPasswordValidator, AMFPasswordValidator>();

            services.AddIdentity<AMFUserLogin, string>()
                .AddDefaultTokenProviders();

            // Adds IdentityServer
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryPersistedGrants()
                .AddClientStore<AMFClientStore>()
                .AddProfileService<AMFProfileService>()
                .AddResourceStore<AMFResourceStore>();
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
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
