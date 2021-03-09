using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.SQLDatamodels
{
    [Table("Visitors")]
    public class Visitor : Core
    {
        public Visitor() : base()
        {
        }
        public string ClientIP { get; set; }
        public string Referer { get; set; }         
        public string UserAgent { get; set; }
    }
}
