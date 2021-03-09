using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Dotnet.Url.Jumper.Domain.Exceptions;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class DbContextStatRepository : IStatRepository
    {
        private readonly ILogger<DbContextStatRepository> _loggerservice;
        private CoreDbContext _context;
        private readonly IMapper _mapper;

        public DbContextStatRepository(IMapper mapper, ILogger<DbContextStatRepository> loggerservice,
            CoreDbContext repoContext)
        {
            _context = repoContext;
            _loggerservice = loggerservice;
            _mapper = mapper;
        }

        public Stat Add(Domain.Models.Stat entity)
        {
            var entitystring = JsonConvert.SerializeObject(entity);
            _loggerservice.LogInformation("Adding new Stat to repository : " + entitystring);
            try
            {                                
                var dbentity = _mapper.Map<DbStat>(entity);                
                _context.Add(dbentity);
                _context.Attach(dbentity.shortUrl);
                _context.SaveChanges();
                _loggerservice.LogInformation("Added new Stat to repository : " + entitystring);
                return _mapper.Map<Domain.Models.Stat>(dbentity);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error adding new Stat into repository. " + entitystring + " " + ex.Message);
            }
        }

        public Stat FindByCreationDate(DateTime creationDate)
        {
            try
            {
                var Stat = _context.Stats
                    .AsNoTracking()
                    .Include(v => v.visitor)
                    .Include(s => s.shortUrl)
                    .Where(e => e.AddedDate == creationDate)
                    .First();
                return _mapper.Map<Domain.Models.Stat>(Stat);
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
                    .AsNoTracking()
                    .Include(v => v.visitor)
                    .Include(s => s.shortUrl)
                    .Where(e => e.Id == id).First();
                return _mapper.Map<Domain.Models.Stat>(Stat);
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
                    .AsNoTracking()
                    .Include(v => v.visitor)
                    .Include(s => s.shortUrl)
                    .Where(e => e.ModifiedDate == modificationDate).First();
                return _mapper.Map<Domain.Models.Stat>(Stat);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stat from repository. " + ex.Message);
            }
        }

        public IEnumerable<Domain.Models.Stat> List()
        {
            try
            {
                _loggerservice.LogInformation("Retrieving Stats from repository.");
                var Stats = _context.Stats
                    .AsNoTracking()
                    .Include(v => v.visitor)
                    .Include(s => s.shortUrl)
                    .OrderBy(x => x.AddedDate)
                    .ToList();
                _loggerservice.LogInformation("Retrieving Stats from repository.");
                return _mapper.Map<IEnumerable<Domain.Models.Stat>>(Stats);
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

        public Stat Update(Domain.Models.Stat entity)
        {
            try
            {
                var stat = _mapper.Map<DbStat>(entity);
                _context.Stats
                    .Add(stat);
                _context.SaveChanges();
                return _mapper.Map<Domain.Models.Stat>(stat);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error updating Stat from repository. " + ex.Message);
            }
        }

        public IEnumerable<Domain.Models.Stat> GetByShortUrl(string ShortUrl)
        {
            try
            {
                var entities = _context.Stats
                    .AsNoTracking()
                    .Include(v => v.visitor)
                    .Include(s => s.shortUrl)
                    .Where(x => x.shortUrl.ShortenedUrl == ShortUrl)
                    .ToList();
                return _mapper.Map<IEnumerable<Domain.Models.Stat>>(entities);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stats from repository by Path. " + ex.Message);
            }
        }

        public IEnumerable<Domain.Models.Stat> GetByOriginalUrl(string OriginalUrl)
        {
            try
            {
                var entities = _context.Stats
                    .AsNoTracking()
                    .Include(v => v.visitor)
                    .Include(s => s.shortUrl)
                    .Where(x => x.shortUrl.OriginalUrl == OriginalUrl)
                    .ToList();                    
                return _mapper.Map<IEnumerable<Domain.Models.Stat>>(entities);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stats from repository by OriginalUrl. " + ex.Message);
            }
        }

        public IEnumerable<Domain.Models.Stat> FindBetween(DateTime from, DateTime to)
        {
            try
            {                
                var entities = _context.Stats
                    .AsNoTracking()
                    .Include(v => v.visitor)
                    .Include(s => s.shortUrl)
                    .Where(x => x.AddedDate >= from && x.AddedDate <= to)
                    .OrderBy(x => x.AddedDate)                    
                    .ToList();
                return _mapper.Map<IEnumerable<Domain.Models.Stat>>(entities);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving Stats from repository by Between. " + ex.Message);
            }
        }
    }
}
