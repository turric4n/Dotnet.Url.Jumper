using System.Collections.Generic;

namespace Dotnet.Url.Jumper.Application.Services
{
    public interface ISecurityValidatorService
    {
        IEnumerable<string> GetSecrets();
        void ValidateSecret(string securityKey);
    }
}
