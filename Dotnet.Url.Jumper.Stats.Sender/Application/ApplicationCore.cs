using CommandLine;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Application.Services;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Stats.Sender.Application
{
    public class ApplicationCore
    {
        private readonly IStatsService statservice;
        private readonly ILogger<ApplicationCore> logger;
        private readonly IStatsExportService statsExportService;

        public ApplicationCore(IStatsService statservice, ILogger<ApplicationCore> logger, IStatsExportService statsExportService)
        {
            this.statservice = statservice;
            this.logger = logger;
            this.statsExportService = statsExportService;
        }        
        public void Start(CommandLineParserOptions options)
        {            
            if (!(options.Yesterday || (options.BeginDate.HasValue && options.EndDate.HasValue)))                
            {
                throw new ArgumentException("Invalid date arguments are provided.");
                
            }
            else if (!(options.Email))
            {
                throw new ArgumentException("Invalid output arguments are provided.");
            }
            IEnumerable<Stat> stats = null;
            if (options.Yesterday)
            {
                stats = statservice.GetBetween(DateTime.Today.AddDays(-1), DateTime.Today);
            }
            else
            {
                if (options.BeginDate.Value.Date == options.EndDate.Value.Date) { throw new Exception("Dates are the same");  }
                stats = statservice.GetBetween(options.BeginDate.Value.Date, options.EndDate.Value.Date);
            }

            if (stats == null) { throw new Exception("Stats are null, talk with developers"); }

            statsExportService.Export(stats);

        }
    }
}
