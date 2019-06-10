using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    public class DbShortUrl : CoreDbEntity
    {
        [Required]
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
    }
}
