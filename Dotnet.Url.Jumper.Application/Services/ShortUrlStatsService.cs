using AutoMapper;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Infrastructure.Services.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class ShortUrlStatsService : IStatsService
    {
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        private readonly IStatRepository _statRepository;

        public ShortUrlStatsService(ILoggerService loggerService, IMapper mapper, IStatRepository statRepository)
        {
            _loggerService = loggerService;
            _mapper = mapper;
            _statRepository = statRepository;
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
            _loggerService.Info(this.GetType().ToString(), JsonConvert.SerializeObject(stat));
            _statRepository.Add(_mapper.Map<Domain.Models.Stat>(stat));
        }
    }
}
