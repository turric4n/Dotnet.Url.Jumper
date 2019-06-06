using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using System;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    public abstract class CoreDbEntity : IEntityDate
    {
        public long Id { get; set; }
        public DateTime AddedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
    }
}
