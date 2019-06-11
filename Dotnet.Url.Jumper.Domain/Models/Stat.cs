using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Models
{
    public class Stat
    {
        public DateTime AddedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
        public ShortUrl shortUrl { get; set; }
        public Visitor visitor { get; set; }
    }
}
