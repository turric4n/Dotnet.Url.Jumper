using System.Collections.Generic;
using AutoMapper;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Domain.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IMemoryCache _shortUrlCacheService;        
        private readonly ILogger<ShortUrlService> _loggerservice;
        private readonly IMapper _mapper;
        private readonly IUrlShortenerGeneratorService _generator;

        public ShortUrlService(IShortUrlRepository shortUrlRepository, IUrlShortenerGeneratorService shorturlgenerator, 
            IMemoryCache shorturlCacheService, ILogger<ShortUrlService> loggerservice, 
            IMapper mapper) 
        {
            _shortUrlCacheService = shorturlCacheService;
            _loggerservice = loggerservice;
            _shortUrlRepository = shortUrlRepository;
            _generator = shorturlgenerator;
            _mapper = mapper;
        }

        public void DeleteById(int id)
        {
            _shortUrlRepository.Remove(id);
        }

        public ShortUrl GenerateNew(NewShortUrl newShortUrl)
        {
            _loggerservice.LogInformation("Generate URL request called : " + newShortUrl.OriginalUrl);
            var shorturl = _mapper.Map<ShortUrl>(newShortUrl);            
            var reposhorturl = _shortUrlRepository.Add(_mapper.Map<Domain.Models.ShortUrl>(shorturl));
            reposhorturl.ShortenedUrl = _generator.Encode(reposhorturl.Id);
            _shortUrlRepository.Update(reposhorturl);
            _loggerservice.LogInformation("Caching new short URL into cache service : ");
            shorturl = _mapper.Map<ShortUrl>(reposhorturl);
            _shortUrlCacheService.Set(shorturl.ShortenedUrl, shorturl);
            return shorturl;
        }

        public IEnumerable<ShortUrl> GetAll()
        {
            _loggerservice.LogInformation("GetAll request called : ");
            var shorturls = _shortUrlRepository.List();
            return _mapper.Map<IEnumerable<ShortUrl>>(shorturls);
        }

        public ShortUrl GetById(int id)
        {
            _loggerservice.LogInformation("GetById request called : " + id.ToString());
            var shorturl = _mapper.Map<ShortUrl>(_shortUrlRepository.FindById(id));
            return shorturl;
        }
       
        public ShortUrl GetByOriginalUrl(string originalUrl)
        {
            _loggerservice.LogInformation("GetByOriginalUrl request called : " + originalUrl);
            var shorturl = _mapper.Map<ShortUrl>(_shortUrlRepository.GetByOriginalUrl(originalUrl));
            return shorturl;
        }

        public ShortUrl GetByPath(string path)
        {
            _loggerservice.LogInformation("GetByPath request called : " + path);
            ShortUrl shorturl = null;
            _shortUrlCacheService.TryGetValue(path, out shorturl);
            if (shorturl == null)
            {
                _loggerservice.LogInformation("shorturl is not in cache service, getting from repo : " + path);
                shorturl = _mapper.Map<ShortUrl>(_shortUrlRepository.GetByPath(path));
                if (shorturl != null)
                {
                    _shortUrlCacheService.Set(path, shorturl);
                    _loggerservice.LogInformation("shorturl successfully cached : " + path);
                }
            }
            else { _loggerservice.LogInformation("shorturl get from cache : " + path); }            
            return shorturl;           
        }

        public void ValidateShortUrl(string ShortUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}
