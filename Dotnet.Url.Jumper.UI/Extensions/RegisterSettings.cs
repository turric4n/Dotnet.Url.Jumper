using Dotnet.Url.Jumper.Application;
using Dotnet.Url.Jumper.Application.Security;
using Dotnet.Url.Jumper.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Url.Jumper.UI.Extensions
{
    public static class RegisterSettings
    {
        static public void RegisterCurrentSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var securitySection = configuration.GetSection("SecuritySettings");
            var infraSettingsSection = configuration.GetSection("InfraSettings");
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<SecuritySettings>(securitySection);
            services.Configure<InfrastructureSettings>(infraSettingsSection);
            services.Configure<AppSettings>(appSettingsSection);
            services.BuildServiceProvider();
            services.AddOptions();
        }
    }
}
