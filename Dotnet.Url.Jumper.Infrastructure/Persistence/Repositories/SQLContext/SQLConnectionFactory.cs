using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext
{
    public class SQLConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _settings;

        public SQLConnectionFactory(IConfiguration settings)
        {
            _settings = settings;
        }

        public IDbConnection GetConnection(string connectionStringName)
        {
            var conn = new SqlConnection();
            conn.ConnectionString = _settings.GetConnectionString(connectionStringName);
            return conn;
        }
    }
}
