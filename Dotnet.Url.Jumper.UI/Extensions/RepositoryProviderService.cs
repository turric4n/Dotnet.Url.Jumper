using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.DBContext;
using Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext;
using Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext;
using Dotnet.Url.Jumper.Infrastructure.Settings;
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
                case DatabaseEngine.EntityFrameworkSQLite:
                    services.AddSingleton<IShortUrlRepository, DbContextShortUrlRepository>();
                    services.AddSingleton<IAdminRepository, DbContextAdminRepository>();
                    services.AddSingleton(typeof(IRepoContext<>), typeof(CoreDbContext<>));
                    break;
                case DatabaseEngine.SQLServer:
                    services.AddSingleton<IConnectionFactory, SQLConnectionFactory>();
                    break;
            }
        }
    }
}
