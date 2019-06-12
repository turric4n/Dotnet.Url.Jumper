using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Persistence.CoreDatamodels;
using Dotnet.Url.Jumper.Infrastructure.Services.Logger;
using Microsoft.Extensions.Configuration;

namespace Dotnet.Url.Jumper.Infrastructure.Repositories.DBContext
{
    public class DbContextVisitorRepository : IVisitorRepository
    {
        private readonly ILoggerService _loggerservice;
        private CoreDbContext _context;
        private readonly IMapper _mapper;

        public DbContextVisitorRepository(IConfiguration configuration, IMapper mapper, ILoggerService loggerservice, CoreDbContext repoContext)
        {            
            _context = repoContext;
            _loggerservice = loggerservice;
            _mapper = mapper;            
        }

        public Visitor Add(Visitor entity)
        {
            var dbentity = _mapper.Map<DbVisitor>(entity);
            _context.Add(dbentity);
            _context.SaveChanges();
            return _mapper.Map<Visitor>(dbentity);
        }

        public Visitor FindByCreationDate(DateTime creationDate)
        {
            var admin = _context.Visitors
                .Where(e => e.AddedDate == creationDate).First();
            return _mapper.Map<Visitor>(admin);
        }

        public Visitor FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Visitor FindByModificationDate(DateTime modificationDate)
        {
            throw new NotImplementedException();
        }

        public Visitor FindByUsername(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Visitor> List()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Visitor Update(Visitor entity)
        {

            var dbentity = _mapper.Map<DbVisitor>(entity);
            _context.Visitors.Add(dbentity);
            _context.SaveChanges();
            return _mapper.Map<Visitor>(dbentity);
        }
    }
}
