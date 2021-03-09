using Dotnet.Url.Jumper.Application;
using Dotnet.Url.Jumper.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;

namespace Dotnet.Url.Jumper.UI.Controllers
{    
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class JumpController : ControllerBase
    {
        private readonly IShortUrlService _shorturlservice;
        private readonly IVisitorLocatorService _visitorService;
        private readonly IOptions<AppSettings> _settings;
        private readonly IStatsService _statsService;
        private readonly ILogger<JumpController> _loggerservice;

        public JumpController(IShortUrlService shorturlservice, IVisitorLocatorService visitorService, 
            IOptions<AppSettings> settings, IStatsService statsService, 
            ILogger<JumpController> loggerService)
        {
            _loggerservice = loggerService;
            _shorturlservice = shorturlservice;
            _visitorService = visitorService;
            _settings = settings;
            _statsService = statsService;
        }
        [HttpGet("/jump/{path}")]
        [HttpGet("/{path}")]
        public IActionResult Get(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) { return BadRequest(); }
                var url = _shorturlservice.GetByPath(path);
                if (url == null) { return NotFound(); }
                var visitor = _visitorService.GetCurrentVisitor();          
                
                _statsService.AddShortUrlStat(url, visitor);

                var uribuilder = new UriBuilder(url.OriginalUrl);
                if (HttpContext.Request.QueryString.HasValue)
                {
                    var currentquerystring = HttpContext.Request.QueryString.Value;
                    if (!string.IsNullOrEmpty(uribuilder.Query)) { currentquerystring = currentquerystring.Replace("?", "&"); }
                    uribuilder.Query = string.Concat(uribuilder.Query, currentquerystring);
                }
                                
                HttpContext.Response.Headers.Add("Location", uribuilder.Uri.AbsoluteUri);
                if (url.RedirectionCode == 0) { url.RedirectionCode = _settings.Value.Defaultredirectioncode; }
                
                return new StatusCodeResult(url.RedirectionCode);
            }
            catch(Exception ex)
            {
                var exc = ex.InnerException ?? ex;
                _loggerservice.LogError("Get error " + exc.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exc);
            }                       
        }
    }
}