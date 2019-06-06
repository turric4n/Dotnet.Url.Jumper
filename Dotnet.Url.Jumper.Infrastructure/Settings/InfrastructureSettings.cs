using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Infrastructure.Settings
{
    public enum DatabaseEngine
    {
        EntityFrameworkSQLite,
        SQLServer
    }
    public class InfrastructureSettings
    {
        public DatabaseEngine databaseEngine { get; set; }
    }
}
