using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class DbContextAdminRepository : IAdminRepository
    {
        private readonly ILogger<DbContextAdminRepository> _loggerservice;
        private CoreDbContext _context;
        private readonly IMapper _mapper;

        public DbContextAdminRepository(IConfiguration configuration, IMapper mapper, ILogger<DbContextAdminRepository> loggerservice, CoreDbContext repoContext)
        {
            _context = repoContext;
            _loggerservice = loggerservice;
            _mapper = mapper;            
        }

        public Admin Add(Admin entity)
        {
            var dbentity = _mapper.Map<DbShortUrl>(entity);
            _context.Add(dbentity);
            _context.SaveChanges();
            return _mapper.Map<Admin>(dbentity);
        }

        public Admin FindByCreationDate(DateTime creationDate)
        {
            var admin = _context.Admins
                .Where(e => e.AddedDate == creationDate).First();
            return _mapper.Map<Admin>(admin);
        }

        public Admin FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Admin FindByModificationDate(DateTime modificationDate)
        {
            throw new NotImplementedException();
        }

        public Admin FindByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Admin> List()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Admin Update(Admin entity)
        {

            var dbentity = _mapper.Map<DbShortUrl>(entity);
            _context.Add(dbentity);
            _context.SaveChanges();
            return _mapper.Map<Admin>(dbentity);
        }

        public IEnumerable<Admin> FindBetween(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
