using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AlwaysMoveForward.OAuth2.Client.Code;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace OAuth2.Client
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
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //app.UseJwtBearerAuthentication(new JwtBearerOptions
            //{
            //    Authority = Constants.Authority,
            //    RequireHttpsMetadata = false,

            //    Audience = "api1",

            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true              
            //});

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",

                Authority = Constants.Authority,
                RequireHttpsMetadata = false,

                ClientId = "abcd",
                ClientSecret = "abcd",
                CallbackPath = "/home/handlecallback",

                ResponseType = "code id_token",
                Scope = { "offline_access", "api1.full_access" },
                
                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true,
                TokenValidationParameters = new
                Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    NameClaimType = JwtClaimTypes.Name,
                    RoleClaimType = JwtClaimTypes.Role,
                },
                Events = new OpenIdConnectEvents()
                {
                    OnRemoteFailure = context =>
                    {
                        context.Response.Redirect("/Home/Error");
                        context.HandleResponse();
                        return Task.FromResult(0);
                    }
                }
            });

            //app.UseJwtBearerAuthentication(new JwtBearerOptions
            //{
            //    // base-address of your identityserver
            //    Authority = "http://localhost:55482",

            //    // name of the API resource
            //    Audience = "api1",

            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true
            //});


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static string Hash256(string text)
        {
            string retVal;

            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                retVal = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }

            return retVal;
        }
    }
}
