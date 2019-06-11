using Dotnet.Url.Jumper.Application;
using Dotnet.Url.Jumper.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace Dotnet.Url.Jumper.UI.Controllers
{
    [AllowAnonymous]    
    [ApiController]
    public class JumpController : ControllerBase
    {
        private readonly IShortUrlService _shorturlservice;
        private readonly IVisitorLocatorService _visitorService;
        private readonly IOptions<AppSettings> _settings;
        private readonly IStatsService _statsService;

        public JumpController(IShortUrlService shorturlservice, IVisitorLocatorService visitorService, 
            IOptions<AppSettings> settings, IStatsService statsService)
        {
            _shorturlservice = shorturlservice;
            _visitorService = visitorService;
            _settings = settings;
            _statsService = statsService;
        }

        [Route("/{path}")]
        [HttpGet]
        public IActionResult Get(string path)
        {
            try
            {
                var url = _shorturlservice.GetByPath(path);
                var visitor = _visitorService.GetCurrentVisitor();
                _statsService.AddShortUrlStat(url, visitor);
                HttpContext.Response.Headers.Add("Location", url.OriginalUrl);
                if (url.RedirectionCode == 0) { url.RedirectionCode = _settings.Value.Defaultredirectioncode; }
                return new StatusCodeResult(url.RedirectionCode);
            }
            catch(Exception ex)
            {
                var exc = ex.InnerException ?? ex;
                return StatusCode((int)HttpStatusCode.InternalServerError, exc);
            }                       
        }
    }
}