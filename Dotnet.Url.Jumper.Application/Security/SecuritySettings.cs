using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Application.Security
{   
    public class SecuritySettings
    {
        public string securitySchema { get; set; }
        public string JWTSecret { get; set;}
        public string[] ApiKeys { get; set; }
    }
}
