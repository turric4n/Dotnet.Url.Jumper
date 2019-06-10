using Dotnet.Url.Jumper.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Url.Jumper.UI.Controllers
{
    [AllowAnonymous]    
    [ApiController]
    public class JumpController : ControllerBase
    {
        private readonly IShortUrlService _shorturlservice;

        public JumpController(IShortUrlService shorturlservice)
        {
            _shorturlservice = shorturlservice;
        }

        [Route("/{path}")]
        [HttpGet]
        public IActionResult Get(string path)
        {
            try
            {
                var url = _shorturlservice.GetByPath(path);
                HttpContext.Response.Headers.Add("Location", url.OriginalUrl);
                return new StatusCodeResult(307);
            }
            catch
            {
                return NotFound();
            }                       
        }
    }
}