using AutoMapper;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class ShortUrlStatsService : IStatsService
    {
        private readonly ILogger<ShortUrlStatsService> _loggerService;
        private readonly IMapper _mapper;
        private readonly IStatRepository _statRepository;
        private readonly IVisitorRepository _visitorRepository;

        public ShortUrlStatsService(ILogger<ShortUrlStatsService> loggerService, IMapper mapper, 
            IStatRepository statRepository, IVisitorRepository visitorRepository)
        {
            _loggerService = loggerService;
            _mapper = mapper;
            _statRepository = statRepository;
            _visitorRepository = visitorRepository;
        }

        public IEnumerable<Stat> GetStatByOriginalUrl(string OriginalUrl)
        {
            return _mapper.Map<IEnumerable<Stat>>(_statRepository.GetByOriginalUrl(OriginalUrl));
        }
        public IEnumerable<Stat> GetStatByPath(string Path)
        {
            return _mapper.Map<IEnumerable<Stat>>(_statRepository.GetByShortUrl(Path));
        }
        public void AddShortUrlStat(ShortUrl shortUrl, Visitor visitor)
        {
            var stat = new Stat();
            stat.visitor = visitor;
            stat.shortUrl = shortUrl;
            var domainstat = _mapper.Map<Domain.Models.Stat>(stat);
            _loggerService.LogInformation(JsonConvert.SerializeObject(stat));
            _visitorRepository.Add(domainstat.visitor);
            _statRepository.Add(domainstat);
        }

        public IEnumerable<Stat> GetByDate(DateTime date)
        {
            return _mapper.Map<IEnumerable<Stat>>(_statRepository.FindByCreationDate(date));            
        }

        public IEnumerable<Stat> GetBetween(DateTime from, DateTime to)
        {
            var result = _mapper.Map<IEnumerable<Stat>>(_statRepository.FindBetween(from, to));
            return result;
        }

        public IEnumerable<Stat> GetAll()
        {
            return _mapper.Map<IEnumerable<Stat>>(_statRepository.List());
        }
    }
}
