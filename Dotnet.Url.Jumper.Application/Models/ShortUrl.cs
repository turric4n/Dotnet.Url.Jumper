﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Aplication.Models
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime AddedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
    }
}
