using System.Collections.Generic;
using Dotnet.Url.Jumper.Application.Models;

namespace Dotnet.Url.Jumper.Application.Services
{
    public interface IStatsService
    {
        void AddShortUrlStat(ShortUrl shortUrl, Visitor visitor);
        IEnumerable<Stat> GetStatByOriginalUrl(string OriginalUrl);
        IEnumerable<Stat> GetStatByPath(string Path);
    }
}