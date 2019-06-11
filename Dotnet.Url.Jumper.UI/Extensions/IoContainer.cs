using Dotnet.Url.Jumper.Application.Services;
using Dotnet.Url.Jumper.Domain.Services;
using Dotnet.Url.Jumper.Infrastructure.Services.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Url.Jumper.UI.Extensions
{
    public static class IoContainer
    {
        public static void AddIocRegister(this IServiceCollection services)
        {
            //Domain
            services.AddSingleton<IUrlShortenerGeneratorService, StandardUrlShortenerService>();
            //Application
            services.AddScoped<IShortUrlService, ShortUrlService>();
            services.AddScoped<IStatsService, ShortUrlStatsService>();
            services.AddScoped<IVisitorLocatorService, HttpContextVisitorLocatorService>();
            //Infrastructure
            services.AddSingleton<ILoggerService, QuickLoggerService>();            
            services.BuildServiceProvider();
        }
    }
}
