using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Transport
{
    public interface IOutputTransportService
    {
        void Send(Stream stream, string Filename, string Date);
    }
}
