using Dotnet.Url.Jumper.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dotnet.Url.Jumper.Stats.Sender.Application
{
    public interface IStatsSerializerService
    {
        Stream Serialize(IEnumerable<Stat> stats);
    }
}
