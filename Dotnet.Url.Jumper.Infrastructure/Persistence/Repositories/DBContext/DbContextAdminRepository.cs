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
    public class DbContextAdminRepository : IAdminRepository
    {
        private readonly ILoggerService _loggerservice;
        private IRepoContext<DbAdmin> _context;
        private readonly IMapper _mapper;

        public DbContextAdminRepository(IConfiguration configuration, IMapper mapper, ILoggerService loggerservice)
        {

            _context = new CoreDbContext<DbAdmin>(configuration);
            _loggerservice = loggerservice;
            _mapper = mapper;
        }

        public void Add(Admin entity)
        {
            var dbentity = _mapper.Map<DbAdmin>(entity);        
            _context.Add(dbentity);
        }

        public Admin FindByCreationDate(DateTime creationDate)
        {
            var admin = _context.Entity
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

        public void Update(Admin entity)
        {
            var dbentity = _mapper.Map<DbAdmin>(entity);
            _context.Add(dbentity);
        }
    }
}
