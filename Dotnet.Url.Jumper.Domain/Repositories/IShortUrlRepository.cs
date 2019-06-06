using Dotnet.Url.Jumper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Repositories
{
    public interface IShortUrlRepository : IReadWriteRepository<ShortUrl, int>
    {
        ShortUrl GetByPath(string Path);
        ShortUrl GetByOriginalUrl(string Url);
    }
}
