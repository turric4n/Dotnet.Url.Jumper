using Dotnet.Url.Jumper.Domain.Exceptions;
using Dotnet.Url.Jumper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Models
{
    public class ShortUrl
    {
        private int redirectionCode;

        public int Id { get; set; }             
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public int RedirectionCode { get => redirectionCode; set 
            {
                if (value == 0 || (value >= 301 && value <= 308))
                {
                    redirectionCode = value;
                }
                else
                {
                    throw new DefaultRedirectCodeException("Domain Rules : Redirectioncode needs to be between 301 and 308");
                }
            }
        }

        public DateTime AddedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
    }
}
