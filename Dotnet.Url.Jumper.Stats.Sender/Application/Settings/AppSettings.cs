using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Url.Jumper.Stats.Sender.Application.Settings
{
    public class AppSettings
    {
        public string MailboxesToSendReport { get; set; }
        public string Subject { get; set; }
        public string MessageToSend { get; set; }
        public string MailFrom { get; set; }
    }
}
