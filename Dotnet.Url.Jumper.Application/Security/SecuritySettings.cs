using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Url.Jumper.Aplication.Security
{   
    public class SecuritySettings
    {
        public SecuritySchema securitySchema;
        public string JWTSecret { get; set;}
        public string[] ApiKeys { get; set; }
    }
}
