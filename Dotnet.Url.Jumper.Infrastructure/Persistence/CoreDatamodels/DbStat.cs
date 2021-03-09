using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    [Table("Stats")]
    public class DbStat : CoreDbEntity
    {
        public virtual DbShortUrl shortUrl { get; set; }
        public virtual DbVisitor visitor { get; set; }
    }
}
