using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.SQLDatamodels
{
    [Table("Stats")]
    public class Stats : Core
    {
        public Stats() : base()
        {
        }

        public long shortUrlId { get; set; }
        public long visitorId { get; set; }
    }
}
