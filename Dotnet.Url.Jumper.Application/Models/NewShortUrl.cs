using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Application.Models
{
    public class NewShortUrl
    {
        public string OriginalUrl { get; set; }
        public int RedirectionCode { get; set; }
    }
}
