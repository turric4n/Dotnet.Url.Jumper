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
    public class SQLVisitorRepository : IVisitorRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<SQLVisitorRepository> _loggerService;
        private readonly IMapper _mapper;

        public SQLVisitorRepository(IConnectionFactory connectionFactory, ILogger<SQLVisitorRepository> loggerService, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        private IDbConnection _sqlconnection => _connectionFactory.GetConnection();

        public Visitor Add(Visitor entity)
        {
            var dbvisitor = _mapper.Map<SQLDatamodels.Visitor>(entity);
            entity.Id = _sqlconnection.Insert(dbvisitor);
            return entity;
        }

        public IEnumerable<Visitor> FindBetween(DateTime from, DateTime to)
        {
            var predicate = Predicates.Between<SQLDatamodels.Visitor>(b => b.AddedDate, new BetweenValues() { Value1 = from, Value2 = to });
            var visitors = _sqlconnection.GetListAsync<SQLDatamodels.Visitor>(predicate).Result;
            var toret = _mapper.Map<IEnumerable<Visitor>>(visitors);
            return toret;
        }

        public Visitor FindByCreationDate(DateTime creationDate)
        {
            var predicate = Predicates.Field<SQLDatamodels.Visitor>(f => f.AddedDate, Operator.Eq, creationDate);
            var visitor = _sqlconnection.GetAsync<SQLDatamodels.Visitor>(predicate);
            return _mapper.Map<Visitor>(visitor);
        }

        public Visitor FindById(int id)
        {
            var predicate = Predicates.Field<SQLDatamodels.Visitor>(f => f.Id, Operator.Eq, id);
            var visitor = _sqlconnection.Get<SQLDatamodels.Visitor>(predicate);
            return _mapper.Map<Visitor>(visitor);
        }

        public Visitor FindByModificationDate(DateTime modificationDate)
        {
            var predicate = Predicates.Field<SQLDatamodels.Visitor>(f => f.ModifiedDate, Operator.Eq, modificationDate);
            var visitor = _sqlconnection.GetAsync<SQLDatamodels.Visitor>(predicate);
            return _mapper.Map<Visitor>(visitor);
        }

        public IEnumerable<Visitor> List()
        {
            var visitors = _sqlconnection.GetListAsync<SQLDatamodels.Visitor>().Result;
            var toret = _mapper.Map<IEnumerable<Visitor>>(visitors);
            return toret;
        }

        public void Remove(int id)
        {
            _sqlconnection.Delete<SQLDatamodels.ShortUrls>(Predicates.Field<SQLDatamodels.ShortUrls>(f => f.Id, Operator.Eq, id));
        }

        public Visitor Update(Visitor entity)
        {
            var dbvisitor = _mapper.Map<SQLDatamodels.Visitor>(entity);
            _sqlconnection.Update(dbvisitor);
            return entity;
        }
    }
}
