using AutoMapper;
using Dapper;
using DapperExtensions;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext
{
    public class SQLAdminRepository : IAdminRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<SQLAdminRepository> _loggerService;
        private readonly IMapper _mapper;

        public SQLAdminRepository(IConnectionFactory connectionFactory, ILogger<SQLAdminRepository> loggerService, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        private IDbConnection _sqlconnection => _connectionFactory.GetConnection();

        public Admin Add(Admin entity)
        {
            var admin = _mapper.Map<SQLDatamodels.Admins>(entity);
            entity.Id = _sqlconnection.Insert(admin);
            return entity;
        }

        public IEnumerable<Admin> FindBetween(DateTime from, DateTime to)
        {
            var predicate = Predicates.Between<SQLDatamodels.Admins>(b => b.AddedDate, new BetweenValues() { Value1 = from, Value2 = to });
            var urls = _sqlconnection.GetListAsync<SQLDatamodels.Admins>(predicate).Result;
            var toret = _mapper.Map<IEnumerable<Admin>>(urls);
            return toret;
        }

        public Admin FindByCreationDate(DateTime creationDate)
        {
            var predicate = Predicates.Field<SQLDatamodels.Admins>(f => f.AddedDate, Operator.Eq, creationDate);
            var urls = _sqlconnection.GetAsync<SQLDatamodels.Admins>(predicate);
            return _mapper.Map<Admin>(urls);
        }

        public Admin FindById(int id)
        {
            var predicate = Predicates.Field<SQLDatamodels.Admins>(f => f.Id, Operator.Eq, id);
            var admin = _sqlconnection.Get<SQLDatamodels.Admins>(predicate);
            return _mapper.Map<Admin>(admin);
        }

        public Admin FindByModificationDate(DateTime modificationDate)
        {
            var predicate = Predicates.Field<SQLDatamodels.Admins>(f => f.ModifiedDate, Operator.Eq, modificationDate);
            var admin = _sqlconnection.Get<SQLDatamodels.Admins>(predicate);
            return _mapper.Map<Admin>(admin);
        }

        public Admin FindByUsername(string userName)
        {
            var predicate = Predicates.Field<SQLDatamodels.Admins>(f => f.Username, Operator.Eq, userName);
            var admin = _sqlconnection.Get<SQLDatamodels.Admins>(predicate);
            return _mapper.Map<Admin>(admin);
        }

        public IEnumerable<Admin> List()
        {
            var urls = _sqlconnection.GetListAsync<SQLDatamodels.Admins>().Result;
            var toret = _mapper.Map<IEnumerable<Admin>>(urls);
            return toret;
        }

        public void Remove(int id)
        {
            _sqlconnection.Delete<SQLDatamodels.Admins>(Predicates.Field<SQLDatamodels.Admins>(f => f.Id, Operator.Eq, id));
        }

        public Admin Update(Admin entity)
        {
            var admin = _mapper.Map<SQLDatamodels.Admins>(entity);
            _sqlconnection.Update(admin);
            return entity;
        }
    }
}
