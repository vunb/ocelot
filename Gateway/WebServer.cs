using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway
{
    public class WebServer
    {
        private List<IDisposable> _webapps = new List<IDisposable>();

        public void Start()
        {
            try
            {

                IWebHostBuilder builder = new WebHostBuilder();

                builder.ConfigureServices(s => {
                    s.AddSingleton(builder);
                });

                builder.UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>();

                var host = builder.Build();

                host.Run();

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        public void Stop()
        {
            try
            {
                foreach (var item in _webapps)
                {
                    item?.Dispose();
                }
                _webapps.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

        }
    }
}
