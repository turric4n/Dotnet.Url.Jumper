using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Url.Jumper.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JumpController : ControllerBase
    {
        public JumpController()
        {
        }
    }
}