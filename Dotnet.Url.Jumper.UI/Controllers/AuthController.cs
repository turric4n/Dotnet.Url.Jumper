using System;
using Dotnet.Url.Jumper.Application.Models;
using Dotnet.Url.Jumper.Application.Services;
using Dotnet.Url.Jumper.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Url.Jumper.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAdminService _userService;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthController(IAdminService userService, ITokenGeneratorService tokenGeneratorService)
        {
            _userService = userService;
            _tokenGeneratorService = tokenGeneratorService;
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authenticate([FromBody]Admin user)
        {
            try
            {
                var authuser = _userService.Authenticate(user.Username, user.Password);

                var tokenstring = _tokenGeneratorService.Generate(authuser.Id);

                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    authuser.Id,
                    authuser.Username,
                    authuser.FirstName,
                    authuser.LastName,
                    Token = tokenstring
                });
            }
            catch(Exception ex)
            {
                if (ex is InvalidAdminPasswordException || 
                    ex is InvalidAdminDisabledException)
                { return Unauthorized(); }                
                else { return StatusCode(500); }
            }
        }
    }
}