using Dapper.Contrib.Extensions;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.SQLDatamodels
{
    [Table("ShortUrls")]
    public class ShortUrls : Core
    {
        public ShortUrls() : base()
        {
        }

        public string OriginalUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public int RedirectionCode { get; set; }
    }
}
