using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    public class DbVisitor : CoreDbEntity
    {
        public string ClientIP { get; set; }
        public string Referer { get; set; }        
    }
}
