using Dotnet.Url.Jumper.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    [Table("ShortUrls")]
    public class DbShortUrl : CoreDbEntity
    {
        [Required]
        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public int RedirectionCode { get; set; }
    }
}
