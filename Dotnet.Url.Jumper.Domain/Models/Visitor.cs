using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Models
{
    public class Visitor
    {
        public long Id { get; set; }
        public string ClientIP { get; set; }
        public string Referer { get; set; }        
        public string UserAgent { get; set; }
    }
}
