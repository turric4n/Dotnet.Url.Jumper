using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Dotnet.Url.Jumper.UI.Controllers
{
    [Authorize]
    [Route("api/admin/shorturl")]
    [ApiController]
    public class ShortUrlAdminController : ControllerBase
    {
        private readonly IShortUrlService _shorturlservice;
        private readonly ILogger<ShortUrlAdminController> _loggerservice;

        public ShortUrlAdminController(IShortUrlService shorturlservice, ILogger<ShortUrlAdminController> loggerService)
        {
            _shorturlservice = shorturlservice;
            _loggerservice = loggerService;
        }
        [HttpGet]
        public IEnumerable<ShortUrl> GetAll()
        {
            return _shorturlservice.GetAll();
        }

        [HttpGet("{id}")]
        public ShortUrl GetById(int id)
        {
            return _shorturlservice.GetById(id);
        }

        // POST: api/ShortUrl
        [HttpPost]
        public IActionResult Post([FromBody] NewShortUrl newShortUrl)
        {
            try
            {
                return Ok(_shorturlservice.GenerateNew(newShortUrl));
            }
            catch(Exception ex)
            {
                var exc = ex.InnerException ?? ex;
                return StatusCode((int)HttpStatusCode.InternalServerError, exc);
            }            
        }               
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _shorturlservice.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                var exc = ex.InnerException ?? ex;                
                _loggerservice.LogError("Delete error " + exc.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exc);
            }
        }
    }
}
