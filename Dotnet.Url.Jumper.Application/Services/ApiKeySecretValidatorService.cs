using Dotnet.Url.Jumper.Application;
using Dotnet.Url.Jumper.Application.Security;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dotnet.Url.Jumper.Application.Services
{
    public class ApiKeySecretValidatorServiceException : Exception
    {
        public ApiKeySecretValidatorServiceException(string message) : base(message)
        {
        }
    }
    public class ApiKeySecretValidatorService : ISecurityValidatorService
    {
        private readonly IOptions<SecuritySettings> _settings;
        public ApiKeySecretValidatorService(IOptions<SecuritySettings> settings)
        {
            _settings = settings;
        }
        public IEnumerable<string> GetSecrets()
        {
            return _settings.Value.ApiKeys;
        }
        public void ValidateSecret(string securityKey)
        {
            try
            {
                var secret = GetSecrets().Where(x => x.ToLower().Equals(securityKey.ToLower())).First();
            }
            catch
            {
                throw new JWTSecretValidatorServiceException("Invalid Secret provided");
            }                        
        }   
    }
}
