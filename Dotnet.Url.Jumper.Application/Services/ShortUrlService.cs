using System.Collections.Generic;
using AutoMapper;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Domain.Exceptions;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Domain.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IMemoryCache _shortUrlCacheService;
        private readonly IMapper _mapper;
        private readonly IUrlShortenerGeneratorService _generator;

        public ShortUrlService(IShortUrlRepository shortUrlRepository, IUrlShortenerGeneratorService shorturlgenerator, IMemoryCache shorturlCacheService, IMapper mapper) 
        {
            _shortUrlCacheService = shorturlCacheService;
            _shortUrlRepository = shortUrlRepository;
            _generator = shorturlgenerator;
            _mapper = mapper;
        }

        public ShortUrl GenerateNew(NewShortUrl newShortUrl)
        {           
            var shorturl = new ShortUrl()
            {
                OriginalUrl = newShortUrl.OriginalUrl
            };
            var reposhorturl = _shortUrlRepository.Add(_mapper.Map<Domain.Models.ShortUrl>(shorturl));
            reposhorturl.ShortenedUrl = _generator.Encode(reposhorturl.Id);
            _shortUrlRepository.Update(reposhorturl);
            shorturl = _mapper.Map<ShortUrl>(reposhorturl);
            _shortUrlCacheService.Set(shorturl.ShortenedUrl, shorturl);
            return shorturl;
        }

        public IEnumerable<ShortUrl> GetAll()
        {
            var shorturls = _shortUrlRepository.List();
            return _mapper.Map<IEnumerable<ShortUrl>>(shorturls);
        }

        public ShortUrl GetById(int id)
        {
            var shorturl = _mapper.Map<ShortUrl>(_shortUrlRepository.FindById(id));
            return shorturl;
        }
       
        public ShortUrl GetByOriginalUrl(string originalUrl)
        {
            var shorturl = _mapper.Map<ShortUrl>(_shortUrlRepository.GetByOriginalUrl(originalUrl));
            return shorturl;
        }

        public ShortUrl GetByPath(string path)
        {
            ShortUrl shorturl = null;
            _shortUrlCacheService.TryGetValue(path, out shorturl);
            if (shorturl == null)
            {
                shorturl = _mapper.Map<ShortUrl>(_shortUrlRepository.GetByPath(path));
                _shortUrlCacheService.Set(path, shorturl);
            }
            return shorturl;           
        }
    }
}
