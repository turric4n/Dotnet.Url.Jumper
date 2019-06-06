using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels
{
    interface IEntityDate
    {
        DateTime AddedDate { set; get; }
        DateTime ModifiedDate { set; get; }
    }
}
