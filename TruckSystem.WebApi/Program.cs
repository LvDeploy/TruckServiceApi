using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TruckSystem.DAL.Context;
using TruckSystem.DAL.Context.Seeder;

namespace TruckSystem.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var build = CreateWebHostBuilder(args).Build();

            using (var scope = build.Services.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetService<SqlContext>())
                {
                    //if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
                    //{
                        dbContext.Database.Migrate();
                    //}

                    var services = scope.ServiceProvider;
                    var seeder = services.GetRequiredService<DbSeeder>();
                    seeder.Seed().Wait();
                }
            }

            build.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                  .UseStartup<Startup>();
            // DbInitializer.Initialize();

            return host;
        }
    }
}
