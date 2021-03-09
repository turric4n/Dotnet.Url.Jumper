using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using Dotnet.Url.Jumper.Application.Models;
using Microsoft.Extensions.Logging;


namespace Dotnet.Url.Jumper.Stats.Sender.Application
{
    public class StatsClosedXMLSerializer : IStatsSerializerService
    {
        private readonly ILogger<StatsClosedXMLSerializer> loggerService;

        public StatsClosedXMLSerializer(ILogger<StatsClosedXMLSerializer> loggerService)
        {
            this.loggerService = loggerService;
        }

        public Stream Serialize(IEnumerable<Stat> stats)
        {
            using (var wb = new XLWorkbook())
            { 
                var ws = wb.Worksheets.Add("Stats");
                ws.Cell(1, 1).Value = "AddedDate";
                ws.Cell(1, 2).Value = "OriginalUrl";
                ws.Cell(1, 3).Value = "ShortenedUrl";
                ws.Cell(1, 4).Value = "RedirectionCode";
                ws.Cell(1, 5).Value = "ClientIP";
                ws.Cell(1, 6).Value = "Referer";
                ws.Cell(1, 7).Value = "UserAgent";
                var x = 2;
                foreach(var stat in stats)
                {
                    ws.Cell(x, 1).Value = stat.AddedDate;                    
                    ws.Cell(x, 2).Value = stat.shortUrl?.OriginalUrl;
                    ws.Cell(x, 3).Value = stat.shortUrl?.ShortenedUrl;
                    ws.Cell(x, 4).Value = stat.shortUrl?.RedirectionCode;
                    ws.Cell(x, 5).Value = stat.visitor?.ClientIP;
                    ws.Cell(x, 6).Value = stat.visitor?.Referer;
                    ws.Cell(x, 7).Value = stat.visitor?.UserAgent;
                    x++;
                }
                ws.Columns().AdjustToContents();
                Stream result = new MemoryStream();                
                wb.SaveAs(result);
                return result;
            }
        }
    }
}
