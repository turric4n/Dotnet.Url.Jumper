using Dotnet.Url.Jumper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Repositories
{
    public interface IStatRepository : IReadWriteRepository<Stat, int>
    {
        IEnumerable<Stat> GetByOriginalUrl(string OriginalUrl);
        IEnumerable<Stat> GetByShortUrl(string ShortUrl);
    }
}
