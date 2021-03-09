using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Domain.Repositories;
using Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Stat = Dotnet.Url.Jumper.Domain.Models.Stat;

namespace Dotnet.Url.Jumper.Stats.Sender.Infrastructure.Repositories.Http
{
    public class HttpStatsRepository : IStatRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpStatsRepository> logger;

        public HttpStatsRepository(IOptions<InfrastructureSettings> settings, 
            ILogger<HttpStatsRepository> logger)
        {         
            var key = settings.Value.ApiKey;
            var auth = new AuthenticationHeaderValue("X-API-KEY", key);
            var uri = new Uri(settings.Value.StatsServiceEndpoint);
            _httpClient = new HttpClient();                    
            _httpClient.BaseAddress = uri;
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", key);            
            this.logger = logger;
        }

        public Stat Add(Stat entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stat> FindBetween(DateTime from, DateTime to)
        {
            try
            {
                var statbydate = new StatByDate() { From = from, To = to };
                var content = new StringContent(JsonConvert.SerializeObject(statbydate), Encoding.UTF8, "application/json");
                var response = _httpClient.PostAsync("/api/admin/stats/bydate", content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    logger.LogError("Error while retrieve stats from HTTPRepo repository ");
                    throw new Exception("Error while retrieve stats from HTTPRepo repository");
                }
                var stats = JsonConvert.DeserializeObject<IEnumerable<Stat>>(response.Content.ReadAsStringAsync().Result);
                return stats;
            }
            
            catch (Exception ex)
            {
                logger.LogError("Error while retrieve stats from HTTPRepo repository " + ex);
                throw new Exception(ex.Message);
            }
        }

        public Stat FindByCreationDate(DateTime creationDate)
        {
            throw new NotImplementedException();
        }

        public Stat FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Stat FindByModificationDate(DateTime modificationDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stat> GetByOriginalUrl(string OriginalUrl)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stat> GetByShortUrl(string ShortUrl)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stat> List()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Stat Update(Stat entity)
        {
            throw new NotImplementedException();
        }
    }
}
