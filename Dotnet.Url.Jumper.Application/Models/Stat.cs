﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Application.Models
{
    public class Stat
    {
        public Stat()
        {
            AddedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        public DateTime AddedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
        public ShortUrl shortUrl { get; set; }
        public Visitor visitor { get; set; }
    }
}
