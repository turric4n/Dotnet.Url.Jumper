using Dotnet.Url.Jumper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Repositories
{
    public interface IVisitorRepository : IReadWriteRepository<Visitor, int>
    {
    }
}
