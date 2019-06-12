using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dotnet.Url.Jumper.Domain.Exceptions;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Dotnet.Url.Jumper.Infrastructure.Services.Logger;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class DbContextStatRepository : IStatRepository
    {
        private readonly ILoggerService _loggerservice;
        private CoreDbContext _context;
        private readonly IMapper _mapper;

        public DbContextStatRepository(IMapper mapper, ILoggerService loggerservice,
            CoreDbContext repoContext)
        {
            _context = repoContext;
            _loggerservice = loggerservice;
            _mapper = mapper;
        }

        public Stat Add(Stat entity)
        {
            try
            {
                _loggerservice.Info(this.GetType().ToString(), "Adding new Stat to repository : ");
                var dbentity = _mapper.Map<DbStat>(entity);
                _context.Add(dbentity);
                _context.SaveChanges();
                _loggerservice.Success(this.GetType().ToString(), "Added new Stat to repository : ");
                return _mapper.Map<Stat>(dbentity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding new Stat into repository. " + ex.Message);
            }
        }

        public Stat FindByCreationDate(DateTime creationDate)
        {
            try
            {
                var Stat = _context.Stats
                    .Where(e => e.AddedDate == creationDate)
                    .First();
                return _mapper.Map<Stat>(Stat);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stat from repository. " + ex.Message);
            }
        }

        public Stat FindById(int id)
        {
            try
            {
                var Stat = _context.Stats
                    .Where(e => e.Id == id).First();
                return _mapper.Map<Stat>(Stat);
            }
            catch (Exception ex)
            {

                throw new RepositoryException("Error retrieving Stat from repository. " + ex.Message);
            }
        }

        public Stat FindByModificationDate(DateTime modificationDate)
        {
            try
            {
                var Stat = _context.Stats
                    .Where(e => e.ModifiedDate == modificationDate).First();
                return _mapper.Map<Stat>(Stat);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stat from repository. " + ex.Message);
            }
        }

        public IEnumerable<Stat> List()
        {
            try
            {
                _loggerservice.Info(this.GetType().ToString(), "Retrieving Stats from repository.");
                var Stats = _context.Stats
                    .ToList();
                _loggerservice.Success(this.GetType().ToString(), "Retrieving Stats from repository.");
                return _mapper.Map<IEnumerable<Stat>>(Stats);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stat from repository. " + ex.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                var Stat = _context.Stats
                    .Where(e => e.Id == id).First();
                _context.Stats.Remove(Stat);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error while remove Stat from repository. " + ex.Message);
            }
        }

        public Stat Update(Stat entity)
        {
            try
            {
                var Stat = _mapper.Map<DbStat>(entity);
                _context.Stats.Add(Stat);
                _context.SaveChanges();
                return _mapper.Map<Stat>(Stat);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating Stat from repository. " + ex.Message);
            }
        }

        public IEnumerable<Stat> GetByShortUrl(string ShortUrl)
        {
            try
            {
                var entities = _context.Stats
                    .Where(x => x.shortUrl.ShortenedUrl == ShortUrl)
                    .ToList();
                return _mapper.Map<IEnumerable<Stat>>(entities);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stats from repository by Path. " + ex.Message);
            }
        }

        public IEnumerable<Stat> GetByOriginalUrl(string OriginalUrl)
        {
            try
            {
                var entities = _context.Stats
                    .Where(x => x.shortUrl.OriginalUrl == OriginalUrl)
                    .ToList();                    
                return _mapper.Map<IEnumerable<Stat>>(entities);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stats from repository by OriginalUrl. " + ex.Message);
            }
        }
    }
}
