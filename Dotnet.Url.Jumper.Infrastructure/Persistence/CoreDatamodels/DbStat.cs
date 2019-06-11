using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    [Table("Stats")]
    public class DbStat : CoreDbEntity
    {
        [Required]
        public DbShortUrl shortUrl { get; set; }
        [Required]
        public DbVisitor visitor { get; set; }
    }
}
