using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    [Table("Stats")]
    public class DbStat : CoreDbEntity
    {
        [Required]
        public virtual DbShortUrl shortUrl { get; set; }
        [Required]
        public virtual DbVisitor visitor { get; set; }
    }
}
