using Dotnet.Url.Jumper.Application;
using Dotnet.Url.Jumper.Application.Security;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class JWTSecretValidatorServiceException : Exception
    {
        public JWTSecretValidatorServiceException(string message) : base(message)
        {
        }
    }
    public class JWTSecurityValidatorService : ISecurityValidatorService
    {
        private readonly IOptions<SecuritySettings> _settings;
        public JWTSecurityValidatorService(IOptions<SecuritySettings> settings)
        {
            _settings = settings;
        }
        public IEnumerable<string> GetSecrets()
        {
            return new string[] { _settings.Value.JWTSecret };
        }
        public void ValidateSecret(string securityKey)
        {
            if (GetSecrets().First() != securityKey) { throw new JWTSecretValidatorServiceException("Invalid Secret provided"); }; 
        }   
    }
}
