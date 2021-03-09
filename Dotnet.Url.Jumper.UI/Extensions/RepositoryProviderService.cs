using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext;
using Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext;
using Dotnet.Url.Jumper.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Dotnet.Url.Jumper.UI.Extensions
{
    public static class RepositoryProviderService
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            DatabaseEngine repositorytype = Enum.Parse<DatabaseEngine>(services.BuildServiceProvider().GetRequiredService<IOptions<InfrastructureSettings>>().Value.databaseEngine);
            switch (repositorytype)
            {
                case DatabaseEngine.EntityFrameworkSQLite :
                    services.AddScoped<IShortUrlRepository, DbContextShortUrlRepository>();
                    services.AddScoped<IAdminRepository, DbContextAdminRepository>();
                    services.AddScoped<IStatRepository, DbContextStatRepository>();
                    services.AddScoped<IVisitorRepository, DbContextVisitorRepository>();
                    services.AddDbContext<CoreDbContext>(ServiceLifetime.Scoped);
                    break;
                case DatabaseEngine.EntityFrameworkSQL:
                    services.AddScoped<IShortUrlRepository, DbContextShortUrlRepository>();
                    services.AddScoped<IAdminRepository, DbContextAdminRepository>();
                    services.AddScoped<IStatRepository, DbContextStatRepository>();
                    services.AddScoped<IVisitorRepository, DbContextVisitorRepository>();
                    services.AddDbContext<CoreDbContext>(ServiceLifetime.Scoped);
                    break;
                case DatabaseEngine.Dapper:
                    services.AddScoped<IConnectionFactory, SQLConnectionFactory>();
                    services.AddScoped<IShortUrlRepository, SQLShortUrlRepository>();
                    services.AddScoped<IAdminRepository, SQLAdminRepository>();
                    services.AddScoped<IStatRepository, SQLStatRepository>();
                    services.AddScoped<IVisitorRepository, SQLVisitorRepository>();
                    break;
            }
        }
    }
}
