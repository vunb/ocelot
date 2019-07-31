using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Topshelf;

namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(windowsService =>
            {
                windowsService.Service<WebServer>(s =>
                {
                    s.ConstructUsing(service => new WebServer());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                windowsService.RunAsLocalSystem();
                windowsService.StartAutomatically();

                windowsService.SetDescription("Ocelot API Gateway");
                windowsService.SetDisplayName("API Gateway");
                windowsService.SetServiceName("API Gateway");
            });
        }
    }
}
