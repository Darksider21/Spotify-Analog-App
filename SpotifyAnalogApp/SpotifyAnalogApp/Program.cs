using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpotifyAnalogApp.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAnalogApp.Data;
using Microsoft.AspNetCore;

namespace SpotifyAnalogApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var host =  CreateWebHostBuilder(args).Build();

            SeedDatabase(host);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void SeedDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                

               
               
             var aspnetRunContext = services.GetRequiredService<SpotifyAnalogAppContext>();
             SpotifyAnalogAppContextSeed.SeedAsync(aspnetRunContext).Wait();
               
                
            }
        }
    }
}
