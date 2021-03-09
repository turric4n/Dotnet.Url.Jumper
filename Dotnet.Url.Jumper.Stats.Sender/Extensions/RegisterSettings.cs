using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dotnet.Url.Jumper.Stats.Sender.Application.Settings;
using Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Settings;


namespace Dotnet.Url.Jumper.Stats.Sender.Extensions
{
    public static class RegisterSettings
    {
        static public void RegisterCurrentSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var infraSettingsSection = configuration.GetSection("InfraSettings");
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<InfrastructureSettings>(infraSettingsSection);
            services.Configure<AppSettings>(appSettingsSection);
            services.BuildServiceProvider();
            services.AddOptions();
        }
    }
}
