using System;
using System.Net;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dotnet.Url.Jumper.UI.Controllers
{
    [Authorize]
    [Route("api/admin/stats")]
    [ApiController]
    public class StatController : Controller
    {
        private readonly IStatsService _statsService;
        private readonly ILogger<StatController> _loggerservice;

        public StatController(IStatsService statsService, ILogger<StatController> loggerService)
        {
            _loggerservice = loggerService;
            _statsService = statsService;
        }
        [HttpPost]
        [Route("bydate")]
        public IActionResult Post([FromBody] StatByDate statbydate)
        {
            try
            {
                var stats = _statsService.GetBetween(statbydate.From, statbydate.To);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                var exc = ex.InnerException ?? ex;
                return StatusCode((int)HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            try
            {
                var stats = _statsService.GetAll();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                var exc = ex.InnerException ?? ex;
                return StatusCode((int)HttpStatusCode.InternalServerError, exc);
            }
        }
    }
}

