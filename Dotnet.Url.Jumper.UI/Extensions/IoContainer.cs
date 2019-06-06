using Dotnet.Url.Jumper.Application.Services;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext;
using Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext;
using Dotnet.Url.Jumper.Infrastructure.Services.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Url.Jumper.UI.Extensions
{
    public static class IoContainer
    {
        public static void AddIocRegister(this IServiceCollection services)
        {            
            //Application
            services.AddScoped<IAdminService, AdminService>();
            //Infrastructure
            services.AddSingleton<IConnectionFactory, SQLConnectionFactory>();
            services.AddSingleton<IConnectionFactory, SQLConnectionFactory>();            
            services.AddSingleton<ILoggerService, QuickLoggerService>();
            services.AddSingleton<IAdminRepository, DbContextAdminRepository>();
            services.BuildServiceProvider();
        }
    }
}
