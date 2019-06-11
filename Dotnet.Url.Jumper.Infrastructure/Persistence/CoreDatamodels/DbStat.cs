using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    public class DbStat : CoreDbEntity
    {
        public DateTime AddedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
        public DbShortUrl shortUrl { get; set; }
        public DbVisitor visitor { get; set; }
    }
}
