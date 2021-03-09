using System.Data;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
