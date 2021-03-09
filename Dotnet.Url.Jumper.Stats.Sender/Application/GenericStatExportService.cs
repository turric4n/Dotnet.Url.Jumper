using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Transport;

namespace Dotnet.Url.Jumper.Stats.Sender.Application
{
    public class GenericStatExportService : IStatsExportService
    {
        private readonly IStatsSerializerService statsSerializerService;
        private readonly IOutputTransportService outputTransportService;

        public GenericStatExportService(IStatsSerializerService statsSerializerService, IOutputTransportService outputTransportService)
        {
            this.statsSerializerService = statsSerializerService;
            this.outputTransportService = outputTransportService;
        }

        public void Export(IEnumerable<Stat> stats)
        {
            outputTransportService.Send(statsSerializerService.Serialize(stats), "Report.xlsx", stats.FirstOrDefault().AddedDate.ToString());
        }
    }
}
