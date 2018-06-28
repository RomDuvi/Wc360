using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WC360
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IConfigurationRoot Config =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .Build();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(Config)
                .UseStartup<Startup>()
                /*.UseKestrel(options =>
                {
                    // retrieve certificate from store
                    using (var store = new X509Store(StoreName.My))
                    {
                        store.Open(OpenFlags.ReadOnly);
                        var certs = store.Certificates.Find(X509FindType.FindBySubjectName, "localhost", false);
                        if (certs.Count > 0)
                        {
                            var certificate = certs[0];

                            // listen for HTTPS
                            options.Listen(IPAddress.Parse("127.0.0.1"), 2323, listenOptions =>
                            {
                                listenOptions.UseHttps(certificate);
                            });
                        }else{
                            options.Listen(IPAddress.Parse("127.0.0.1"), 2323);
                        }
                    }
                })*/
                .Build();

        public static string ConnectionString =>
            Config.GetConnectionString("Default");
    }
}
