using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    [Table("Visitor")]
    public class DbVisitor : CoreDbEntity
    {
        public string ClientIP { get; set; }
        public string Referer { get; set; }         
        public string UserAgent { get; set; }
    }
}
