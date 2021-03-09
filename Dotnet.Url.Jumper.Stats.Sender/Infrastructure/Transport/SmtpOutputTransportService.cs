using Dotnet.Url.Jumper.Stats.Sender.Application.Settings;
using Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Transport
{
    public class SmtpOutputTransportService : IOutputTransportService
    {
        private readonly ILogger<SmtpOutputTransportService> _logger;
        private readonly IOptions<InfrastructureSettings> _infrasettings;
        private readonly IOptions<AppSettings> _appsettings;
        private readonly SmtpClient _smtpclient;

        public SmtpOutputTransportService(ILogger<SmtpOutputTransportService> logger, IOptions<InfrastructureSettings> infrasettings, 
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _infrasettings = infrasettings;
            _appsettings = appSettings;
            var smtphost = _infrasettings.Value.SmtpServer;           
            var port = _infrasettings.Value.SmtpPort;
            _smtpclient = new SmtpClient(smtphost, port);
        }

        public void Send(Stream stream, string Filename, string Date)
        {
            var mailfrom = new MailAddress(_appsettings.Value.MailFrom);
            var mailmessage = new MailMessage();

            stream.Position = 0;
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Xml);            
            var attachment = new Attachment(stream, ct);
            attachment.ContentDisposition.FileName = Filename;

            mailmessage.From = mailfrom;
            mailmessage.BodyEncoding = Encoding.UTF8;
            mailmessage.To.Add(_appsettings.Value.MailboxesToSendReport);
            mailmessage.Body = _appsettings.Value.MessageToSend;
            mailmessage.Subject = _appsettings.Value.Subject + " Date : " + Date;
            mailmessage.Attachments.Add(attachment);
            _smtpclient.Send(mailmessage);
        }
    }
}
