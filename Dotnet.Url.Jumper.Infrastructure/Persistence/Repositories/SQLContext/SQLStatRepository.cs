using AutoMapper;
using Dapper;
using DapperExtensions;
using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext
{
    public class SQLStatRepository : IStatRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<SQLStatRepository> _loggerService;
        private readonly IMapper _mapper;

        public SQLStatRepository(IConnectionFactory connectionFactory, ILogger<SQLStatRepository> loggerService, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        private IDbConnection _sqlconnection => _connectionFactory.GetConnection();

        public Stat Add(Stat entity)
        {
            var dbstat = _mapper.Map<SQLDatamodels.Stats>(entity);
            _sqlconnection.Insert(dbstat);
            return entity;
        }

        public IEnumerable<Stat> FindBetween(DateTime from, DateTime to)
        {         
            var sql = @"SELECT * FROM Stats AS stat 
                        JOIN ShortUrls AS url ON url.Id = stat.shortUrlId
                        JOIN Visitor as vis ON vis.Id = stat.visitorId
                        WHERE stat.AddedDate BETWEEN @param1 AND @param2";


            var stats = _sqlconnection.Query<SQLDatamodels.Stats, SQLDatamodels.ShortUrls, SQLDatamodels.Visitor, Stat>(
                sql,
                (stat, shorturl, visitor) =>
                {
                    var domainobject = _mapper.Map<Stat>(stat);
                    domainobject.shortUrl = _mapper.Map<ShortUrl>(shorturl);
                    domainobject.visitor = _mapper.Map<Visitor>(visitor);
                    return domainobject;
                }, new { param1 = from, param2 = to });
            var toret = stats;            
            return toret;
        }

        public Stat FindByCreationDate(DateTime creationDate)
        {

            var predicate = Predicates.Field<SQLDatamodels.Stats>(f => f.AddedDate, Operator.Eq, creationDate);
            var stat = _sqlconnection.GetAsync<SQLDatamodels.Stats>(predicate);
            return _mapper.Map<Stat>(stat);
        }

        public Stat FindById(int id)
        {
            var predicate = Predicates.Field<SQLDatamodels.Stats>(f => f.Id, Operator.Eq, id);
            var stat = _sqlconnection.Get<SQLDatamodels.Stats>(predicate);
            return _mapper.Map<Stat>(stat);
        }

        public Stat FindByModificationDate(DateTime modificationDate)
        {
            var predicate = Predicates.Field<SQLDatamodels.Stats>(f => f.ModifiedDate, Operator.Eq, modificationDate);
            var stat = _sqlconnection.GetAsync<SQLDatamodels.Stats>(predicate);
            return _mapper.Map<Stat>(stat);
        }

        public IEnumerable<Stat> GetByOriginalUrl(string OriginalUrl)
        {
            var predicate = Predicates.Field<SQLDatamodels.ShortUrls>(f => f.OriginalUrl, Operator.Eq, OriginalUrl);
            var stat = _sqlconnection.GetListAsync<SQLDatamodels.ShortUrls>(predicate).Result;
            return _mapper.Map<IEnumerable<Stat>>(stat);
        }

        public IEnumerable<Stat> GetByShortUrl(string ShortUrl)
        {
            var predicate = Predicates.Field<SQLDatamodels.Stats>(f => f.shortUrlId, Operator.Eq, ShortUrl);
            var stat = _sqlconnection.GetListAsync<SQLDatamodels.Stats>(predicate);
            return _mapper.Map<IEnumerable<Stat>>(stat);
        }

        public IEnumerable<Stat> List()
        {
            var stats = _sqlconnection.GetListAsync<SQLDatamodels.Stats>().Result;
            var toret = _mapper.Map<IEnumerable<Stat>>(stats);
            return toret;
        }

        public void Remove(int id)
        {
            var predicate = Predicates.Field<SQLDatamodels.Stats>(f => f.shortUrlId, Operator.Eq, id);
            _sqlconnection.Delete(predicate);
        }

        public Stat Update(Stat entity)
        {
            var dbstat = _mapper.Map<SQLDatamodels.ShortUrls>(entity);
            _sqlconnection.Update(dbstat);
            return entity;
        }
    }
}
