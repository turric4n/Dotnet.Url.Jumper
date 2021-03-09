using System;
using System.Collections.Generic;
using System.Text;
using Dotnet.Url.Jumper.Application.Models;

namespace Dotnet.Url.Jumper.Stats.Sender.Application
{
    public class DesiredDateService : IDesiredDateService
    {
        private readonly DateTime beginDate;
        private readonly DateTime endDate;

        public DesiredDateService(DateTime BeginDate, DateTime EndDate)
        {
            beginDate = BeginDate;
            endDate = EndDate;
        }

        public StatByDate GetDesiredDate()
        {
            throw new NotImplementedException();
        }
    }
}
