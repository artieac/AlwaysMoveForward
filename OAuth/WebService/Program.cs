using VP.Digital.Common.Hosting;
using VP.ServicePlatform.Hosting;

namespace WebService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostingConfiguration hostingConfiguration = HostingConfiguration.GetInstance();

            // Start our service via the ServiceLauncher.
            // Responds to the --console parameter to figure out whether to run in console or service mode.
            var sl = new ServiceLauncher<AppHost>
            {
                CommandLineArguments = args,
                DefaultEndPointUrl = string.Format("http://{0}:{1}/", hostingConfiguration.HostName, hostingConfiguration.Port),
                DisplayName = "Default OAuth Service",
                ServiceName = "VP.Digital.Security.OAuth.WebService",
            };

            sl.Launch();
        }
    }
}
