using Funq;
using ServiceStack;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Razor;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.WebHost.Endpoints;
using System.Linq;
using System.Net;
using VP.ServicePlatform.Hosting;

namespace WebService
{
    // Based on https://git.vistaprint.net/projects/SVC/repos/serviceplatform/browse/sample/Sample.ServiceStack.WindowsService/HelloAppHost.cs
    public class AppHost : AppHostHttpListenerBase
    {
        private const int BLEED_TIME_MS = 5000;

        /// <summary>
        /// App Host Constructor
        /// </summary>
        public AppHost()
            : base("Default OAuth Service", typeof(AppHost).Assembly, typeof(ServiceLauncher<>).Assembly)
        {
        }

        public override void Start(string urlBase)
        {
            base.Start(urlBase);
        }

        public override void Stop()
        {
            Plugins.OfType<LiveCheckFeature>().First().DisableAndWaitForRequestsToBleedOff(BLEED_TIME_MS);
            base.Stop();
        }

        public override void Configure(Container container)
        {
            Plugins.Add(new CorsFeature());

            this.PreRequestFilters.Add((httpReq, httpRes) =>
            {
                // Handles Request and closes Responses after emitting global HTTP Headers
                if (httpReq.HttpMethod == "OPTIONS")
                {
                    httpRes.EndRequest();
                }
            });

            Plugins.Add(new RazorFormat());

            var config = new EndpointHostConfig
                             {
                                 RawHttpHandlers =
                                     {
                                         (httpReq) =>
                                             {
                                                 httpReq.UseBufferedStream = true;
                                                 return null;
                                             }
                                     },
                                 CustomHttpHandlers =
                                     {
                                         {
                                             HttpStatusCode.NotFound,
                                             new RazorHandler("/notfound")
                                         }
                                     },
                             };

            DefaultLogging.Configure(this);
            DefaultPlugins.Load(this);

            this.ConfigureAllRoutes();
            container.Register<ICacheClient>(new MemoryCacheClient());
            this.RegisterAllServices(container);
        }

        private void ConfigureAllRoutes()
        {

        }

        private void RegisterAllServices(Container container)
        {
        }
    }
}
