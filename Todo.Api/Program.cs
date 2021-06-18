using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Todo.Api
{
    public static class Program
    {
        public static void Main(string[] args) {
            
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(environment)) {
                environment = "Development";
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();
            
            try {
                var builder = WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .UseConfiguration(configuration);
                
                builder.Build().Run();
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
