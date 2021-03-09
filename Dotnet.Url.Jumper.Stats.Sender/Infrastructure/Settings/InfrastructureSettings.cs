using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Settings
{
    public class InfrastructureSettings
    {
        public string StatsServiceEndpoint { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string ApiKey { get; set; }
    }
}
