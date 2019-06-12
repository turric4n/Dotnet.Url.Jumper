using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dotnet.Url.Jumper.Domain.Exceptions;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Dotnet.Url.Jumper.Infrastructure.Services.Logger;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class DbContextShortUrlRepository : IShortUrlRepository
    {
        private readonly ILoggerService _loggerservice;
        private CoreDbContext _context;
        private readonly IMapper _mapper;

        public DbContextShortUrlRepository(IMapper mapper, ILoggerService loggerservice, 
            CoreDbContext repoContext)
        {
            _context = repoContext;
            _loggerservice = loggerservice;
            _mapper = mapper;
        }

        public ShortUrl Add(ShortUrl entity)
        {
            try
            {
                _loggerservice.Info(this.GetType().ToString(), "Adding new ShortURL to repository : " + entity.OriginalUrl + " , ");
                var dbentity = _mapper.Map<DbShortUrl>(entity);
                _context.ShortUrls.Add(dbentity);
                _context.SaveChanges();
                _context.Entry(dbentity).State = EntityState.Detached;
                _loggerservice.Success(this.GetType().ToString(), "Added new ShortURL to repository : " + entity.OriginalUrl + " , ");
                return _mapper.Map<ShortUrl>(dbentity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding new ShortUrl into repository. " + ex.Message);
            }                     
        }

        public ShortUrl FindByCreationDate(DateTime creationDate)
        {
            try
            {
                var shorturl = _context.ShortUrls
                    .Where(e => e.AddedDate == creationDate)
                    .First();
                return _mapper.Map<ShortUrl>(shorturl);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }
        }

        public ShortUrl FindById(int id)
        {
            try
            {
                var shorturl = _context.ShortUrls
                    .Where(e => e.Id == id).First();
                return _mapper.Map<ShortUrl>(shorturl);
            }
            catch (Exception ex)
            {

                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }
        }

        public ShortUrl FindByModificationDate(DateTime modificationDate)
        {
            try
            {
                var shorturl = _context.ShortUrls
                    .Where(e => e.ModifiedDate == modificationDate).First();
                return _mapper.Map<ShortUrl>(shorturl);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }
        }

        public IEnumerable<ShortUrl> List()
        {
            try
            {
                _loggerservice.Info(this.GetType().ToString(), "Retrieving ShortURLs from repository.");
                var shorturls = _context.ShortUrls
                    .ToList();
                _loggerservice.Success(this.GetType().ToString(), "Retrieving ShortURLs from repository.");
                return _mapper.Map<IEnumerable<ShortUrl>>(shorturls);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                var entity = _context.ShortUrls.Where(
                              x => x.Id == id).First();
                _context.ShortUrls.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while remove ShortUrl from repository. " + ex.Message);
            } 
        }

        public ShortUrl Update(ShortUrl entity)
        {
            try
            {
                var shorturl = _mapper.Map<DbShortUrl>(entity);
                _context.Update(shorturl);
                _context.SaveChanges();
                return _mapper.Map<ShortUrl>(shorturl);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating ShortUrl from repository. " + ex.Message);
            }
        }

        public ShortUrl GetByPath(string Path)
        {
            try
            {
                var entity = _context.ShortUrls
                    .Where(x => x.ShortenedUrl == Path)
                    .First();
                return _mapper.Map<ShortUrl>(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository from Path. " + ex.Message);
            }       
        }

        public ShortUrl GetByOriginalUrl(string Url)
        {
            try
            {
                var entity = _context.ShortUrls
                    .Where(x => x.OriginalUrl == Url)
                    .First();
                return _mapper.Map<ShortUrl>(entity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository from OriginalUrl. " + ex.Message);
            }
        }
    }
}
