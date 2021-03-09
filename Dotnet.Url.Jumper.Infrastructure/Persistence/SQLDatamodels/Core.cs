using DapperExtensions.Mapper;
using System;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.SQLDatamodels
{
    public abstract class Core 
    {
        public Core()
        {
            AddedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        public long Id { get; set; }
        public DateTime AddedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
    }
}
