using Dotnet.Url.Jumper.Domain.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using System;
using System.Collections.Generic;
using DapperExtensions;
using Dotnet.Url.Jumper.Domain.Exceptions;
using Dapper;
using System.Linq;
using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Dotnet.Url.Jumper.Infrastructure.Persistence.Repositories.SQLContext
{
    public class SQLShortUrlRepository : IShortUrlRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<SQLShortUrlRepository> _loggerService;
        private readonly IMapper _mapper;

        public SQLShortUrlRepository(IConnectionFactory connectionFactory, ILogger<SQLShortUrlRepository> loggerService, IMapper mapper)
        {
            _connectionFactory = connectionFactory;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        private IDbConnection _sqlconnection => _connectionFactory.GetConnection();

        public ShortUrl Add(ShortUrl entity)
        {
            try
            {
                var dbshorturl = _mapper.Map<SQLDatamodels.ShortUrls>(entity);
                //string sql = @"
                //INSERT INTO ShortURL @Column1, @Column2, @Column3, @Column4, @Column5 VALUES @Value1, @Value2, @Value3, @Value4, @Value5
                //";
                //_sqlconnection.Query(sql, new { })

                entity.Id = _sqlconnection.Insert(dbshorturl);                
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }

        }

        public void DeleteById(int id)
        {
            try
            {
                _sqlconnection.Delete<SQLDatamodels.ShortUrls>(Predicates.Field<SQLDatamodels.ShortUrls>(f => f.Id, Operator.Eq, id));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }            
        }

        public IEnumerable<ShortUrl> FindBetween(DateTime from, DateTime to)
        {
            try
            {
                var predicate = Predicates.Between<SQLDatamodels.ShortUrls>(b => b.AddedDate, new BetweenValues() { Value1 = from, Value2 = to });
                var urls = _sqlconnection.GetListAsync<SQLDatamodels.ShortUrls>(predicate).Result;
                var toret = _mapper.Map<IEnumerable<ShortUrl>>(urls);
                return toret;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }
        }

        public ShortUrl FindByCreationDate(DateTime creationDate)
        {
            try
            {
                var predicate = Predicates.Field<SQLDatamodels.ShortUrls>(f => f.AddedDate, Operator.Eq, creationDate);
                var urls = _sqlconnection.GetListAsync<SQLDatamodels.ShortUrls>(predicate).Result.First();
                return _mapper.Map<ShortUrl>(urls);
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
                var predicate = Predicates.Field<SQLDatamodels.ShortUrls>(f => f.Id, Operator.Eq, id);
                var shorturl = _sqlconnection.GetListAsync<SQLDatamodels.ShortUrls>(predicate).Result.First();
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
                var predicate = Predicates.Field<SQLDatamodels.ShortUrls>(f => f.ModifiedDate, Operator.Eq, modificationDate);
                var urls = _sqlconnection.GetListAsync<SQLDatamodels.ShortUrls>(predicate).Result.First();
                return _mapper.Map<ShortUrl>(urls);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }
        }

        public ShortUrl GetByOriginalUrl(string Url)
        {
            var predicate = Predicates.Field<SQLDatamodels.ShortUrls>(f => f.OriginalUrl, Operator.Eq, Url);
            var urls = _sqlconnection.GetAsync<SQLDatamodels.ShortUrls>(predicate).Result;
            return _mapper.Map<ShortUrl>(urls);
        }

        public ShortUrl GetByPath(string Path)
        {
            var predicate = Predicates.Field<SQLDatamodels.ShortUrls>(f => f.ShortenedUrl, Operator.Eq, Path);
            try
            {
                var url = _sqlconnection.GetListAsync<SQLDatamodels.ShortUrls>(predicate).Result.FirstOrDefault();
                return _mapper.Map<ShortUrl>(url);
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
                var urls = _sqlconnection.GetListAsync<SQLDatamodels.ShortUrls>().Result;
                var toret = _mapper.Map<IEnumerable<ShortUrl>>(urls);
                return toret;
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
                _sqlconnection.Delete<SQLDatamodels.ShortUrls>(Predicates.Field<SQLDatamodels.ShortUrls>(f => f.Id, Operator.Eq, id));
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }            
        }

        public ShortUrl Update(ShortUrl entity)
        {
            try
            {
                var dbshorturl = _mapper.Map<SQLDatamodels.ShortUrls>(entity);
                _sqlconnection.Update(dbshorturl);
                return entity;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Error retrieving ShortUrl from repository. " + ex.Message);
            }
        }
    }
}
