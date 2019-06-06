using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    public class ShortUrl : CoreDbEntity
    {
        [Required]
        public string OriginalUrl { get; set; }
        public DbAdmin Owner { get; set; }
        public DateTime Expiration { get; set; }
    }
}
