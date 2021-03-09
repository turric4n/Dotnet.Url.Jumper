using Dotnet.Url.Jumper.Application.Services;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Stats.Sender.Application;
using Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Repositories.Http;
using Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Transport;
using Microsoft.Extensions.DependencyInjection;
using Moq;


namespace Dotnet.Url.Jumper.Stats.Sender.Extensions
{
    public static class IoContainer
    {
        public static void AddIocRegister(this IServiceCollection services)
        {
            //Application
            services.AddScoped<IStatsService, ShortUrlStatsService>();
            services.AddSingleton<ApplicationCore>();
            services.AddSingleton<IStatsExportService, GenericStatExportService>();
            services.AddSingleton<IStatsSerializerService, StatsClosedXMLSerializer>();
            //Infrastructure
            services.AddSingleton<IStatRepository, HttpStatsRepository>();
            services.AddSingleton(new Mock<IVisitorRepository>().Object);
            services.AddSingleton<IOutputTransportService, SmtpOutputTransportService>();

            services.BuildServiceProvider();
        }
    }
}
