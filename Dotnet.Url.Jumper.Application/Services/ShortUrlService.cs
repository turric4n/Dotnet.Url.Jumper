using AutoMapper;
using Dotnet.Url.Jumper.Aplication.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Aplication.Services
{
    public class ShortUrlService : IShortUrlService
    {

        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IMapper _mapper;

        public ShortUrlService(IShortUrlRepository shortUrlRepository, IMapper mapper) 
        {
            _shortUrlRepository = shortUrlRepository;
            _mapper = mapper;
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
            var shorturl = _mapper.Map<ShortUrl>(_shortUrlRepository.GetByPath(id));
            return shorturl;
        }

        public int Save(ShortUrl shortUrl)
        {
            throw new NotImplementedException();
        }
    }
}
