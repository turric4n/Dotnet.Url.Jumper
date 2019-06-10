using Dotnet.Url.Jumper.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Application.Services
{
    public interface IShortUrlService
    {
        IEnumerable<ShortUrl> GetAll();
        ShortUrl GenerateNew(NewShortUrl newShortUrl);

        ShortUrl GetById(int id);

        ShortUrl GetByPath(string path);

        ShortUrl GetByOriginalUrl(string originalUrl);
    }
}
