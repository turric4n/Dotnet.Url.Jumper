using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;


namespace Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext
{
    public class SQLConnectionFactory : IConnectionFactory, IDisposable
    {
        private readonly IConfiguration _settings;
        private readonly SqlConnection _sqlConnection;
        private readonly ILogger<SQLConnectionFactory> _loggerService;

        public SQLConnectionFactory(IConfiguration settings, 
            ILogger<SQLConnectionFactory> loggerService)
        {
            _settings = settings;
            _loggerService = loggerService;
            _sqlConnection = new SqlConnection();
            _sqlConnection.ConnectionString = _settings.GetConnectionString("CoreConnection");
            _sqlConnection.StateChange += Conn_StateChange;
            _sqlConnection.InfoMessage += Conn_InfoMessage;
        }

        public void Dispose()
        {
            _sqlConnection.Close();
            _sqlConnection.Dispose();
        }

        public IDbConnection GetConnection()
        {
            if (_sqlConnection.State == ConnectionState.Closed) { _sqlConnection.Open(); };
            return _sqlConnection;
        }

        private void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            _loggerService.LogDebug($"[SQL Connection Info -> {e.Message}]");
        }

        private void Conn_StateChange(object sender, StateChangeEventArgs e)
        {
            _loggerService.LogDebug($"[SQL Connection changed state -> Original State : {e.OriginalState}, Current State : {e.CurrentState} ]");
        }
    }
}
