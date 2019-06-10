using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.DBContext;
using Dotnet.Url.Jumper.Infrastructure.Services.Logger;
using Microsoft.Extensions.Configuration;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class DbContextShortUrlRepository : IShortUrlRepository
    {
        private readonly ILoggerService _loggerservice;
        private IRepoContext<DbShortUrl> _context;
        private readonly IMapper _mapper;

        public DbContextShortUrlRepository(IConfiguration configuration, IMapper mapper, ILoggerService loggerservice, IRepoContext<DbShortUrl> repoContext)
        {
            _context = repoContext;
            _loggerservice = loggerservice;
            _mapper = mapper;
        }

        public ShortUrl Add(ShortUrl entity)
        {
            var dbentity = _mapper.Map<DbShortUrl>(entity);        
            _context.Add(dbentity);
            return _mapper.Map<ShortUrl>(dbentity);
        }

        public ShortUrl FindByCreationDate(DateTime creationDate)
        {
            var shorturl = _context.Entity
                .Where(e => e.AddedDate == creationDate).First();
            return _mapper.Map<ShortUrl>(shorturl);
        }

        public ShortUrl FindById(int id)
        {
            var shorturl = _context.Entity
                .Where(e => e.Id == id).First();
            return _mapper.Map<ShortUrl>(shorturl);
        }

        public ShortUrl FindByModificationDate(DateTime modificationDate)
        {
            var shorturl = _context.Entity
                .Where(e => e.ModifiedDate == modificationDate).First();
            return _mapper.Map<ShortUrl>(shorturl);
        }

        public IEnumerable<ShortUrl> List()
        {
            var shorturls = _context.Entity
                .ToList();
            return _mapper.Map<IEnumerable<ShortUrl>>(shorturls);
        }

        public void Remove(int id)
        {
            _context.Delete(
                _context.Entity.Where(x => x.Id == id)
                .First()
                );            
        }

        public ShortUrl Update(ShortUrl entity)
        {
            var shorturl = _mapper.Map<DbShortUrl>(entity);
            _context.Update(shorturl);
            return _mapper.Map<ShortUrl>(shorturl);
        }

        public ShortUrl GetByPath(string Path)
        {
            var entity = _context.Entity.Where(x => x.ShortenedUrl == Path).First();
            return _mapper.Map<ShortUrl>(entity);            
        }

        public ShortUrl GetByOriginalUrl(string Url)
        {
            var entity = _context.Entity.Where(x => x.OriginalUrl == Url).First();
            return _mapper.Map<ShortUrl>(entity);
        }
    }
}
