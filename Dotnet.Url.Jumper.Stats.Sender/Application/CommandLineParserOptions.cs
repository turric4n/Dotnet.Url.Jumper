using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Stats.Sender.Application
{
    public class CommandLineParserOptions
    {
        [Option(
          Default = false,
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }
        [Option(
            'y', 
            "yesterday",
            Default = false,
            Required = false, 
            HelpText = "Will send yesterday stats.")]
        public bool Yesterday { get; set; }
        [Option(
            "begindate",
            Required = false,
            HelpText = "Begin date to send stats")]
        public DateTime? BeginDate { get; set; }
        [Option(
            "enddate",
            Required = false,
            HelpText = "End date to send stats")]
        public DateTime? EndDate { get; set; }
        [Option(
            'm',
            "mail",
            Default = false,
            Required = false,
            HelpText = "Send stats by email")]
        public bool Email { get; set; }
    }
}
