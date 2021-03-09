using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    public abstract class CoreDbEntity : IEntityDate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime AddedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
    }
}
