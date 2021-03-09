using CommandLine;
using Dotnet.Url.Jumper.Stats.Sender.Application;
using Dotnet.Url.Jumper.Stats.Sender.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System;
using System.IO;
using QuickLogger.Extensions.NetCore;

namespace Dotnet.Url.Jumper.Stats.Sender
{
    class Program
    {
        private static IServiceCollection services;
        private static IConfiguration _configuration;
        static void Main(string[] args)
        {
            services = new ServiceCollection();
            services.AddAutoMapper(typeof(Dotnet.Url.Jumper.Application.Mappings.MappingProfile), 
                typeof(Dotnet.Url.Jumper.Infrastructure.Mappings.MappingProfile));
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
            services.AddQuickLogger();
            services.AddSingleton(_configuration);            
            services.AddIocRegister();            
            services.RegisterCurrentSettings(_configuration);


            Parser.Default.ParseArguments<CommandLineParserOptions>(args)
                .WithParsed(o =>
                {
                    services.BuildServiceProvider().GetRequiredService<ApplicationCore>().Start(o);
                });
        }
    }
}
