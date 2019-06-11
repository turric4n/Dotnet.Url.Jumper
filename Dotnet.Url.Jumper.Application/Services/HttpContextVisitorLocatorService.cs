using System;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Infrastructure.Services.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class HttpContextVisitorLocatorService : IVisitorLocatorService
    {
        private readonly ILoggerService _loggerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<AppSettings> _settings;

        public HttpContextVisitorLocatorService(ILoggerService loggerService, IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings)
        {
            _loggerService = loggerService;
            _httpContextAccessor = httpContextAccessor;
            _settings = settings;
        }

        public Visitor GetCurrentVisitor()
        {            
            var clientipheader = string.IsNullOrEmpty(_settings.Value.ProxyModeClientIPHeaderKey) ? "HTTP_CLIENT_IP" : _settings.Value.ProxyModeClientIPHeaderKey;
            var refererheader = string.IsNullOrEmpty(_settings.Value.CustomRefererHeaderKey) ? "Referer" : _settings.Value.ProxyModeClientIPHeaderKey;
            StringValues clientip = string.Empty;
            StringValues referer = string.Empty;

            if (clientipheader == "HTTP_CLIENT_IP")
            {
                clientip = !string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()) ?
                    _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() :
                    _httpContextAccessor.HttpContext.Connection.LocalIpAddress.ToString();               
            }
            else
            {
                var gotclientip = _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(clientipheader, out clientip) ?
                    _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(clientipheader, out clientip) :
                    _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Host", out clientip);
            }

            if (clientip == "") { clientip = "0.0.0.0"; }

            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(refererheader, out referer);
            var visitor = new Visitor();
            visitor.ClientIP = clientip;
            visitor.Referer = referer.ToString();
            return visitor;
        }
    }
}
