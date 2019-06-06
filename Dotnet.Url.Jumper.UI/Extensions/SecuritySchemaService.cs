using Dotnet.Url.Jumper.Aplication.Security;
using Dotnet.Url.Jumper.Application.Services;
using Dotnet.Url.Jumper.UI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dotnet.Url.Jumper.UI.Security
{
    public static class SecuritySchemaService
    {
        public static void RegisterCurrentSecuritySchema(this IServiceCollection services)
        {
            var settings = services.BuildServiceProvider().GetRequiredService<IOptions<SecuritySettings>>();
            switch (settings.Value.securitySchema)
            {
                case SecuritySchema.JWT:
                    services.AddScoped<ISecurityValidatorService, JWTSecurityValidatorService>();
                    services.AddScoped<ITokenGeneratorService, JWTGeneratorByIdUserService>();
                    services.AddBearerAuthentication(services.BuildServiceProvider().GetRequiredService<ISecurityValidatorService>());
                    break;
                case SecuritySchema.ApiKey:
                    services.AddScoped<ISecurityValidatorService, ApiKeySecretValidatorService>();
                    services.AddApiKeyAuthorization(services.BuildServiceProvider().GetRequiredService<ISecurityValidatorService>());
                    break;
            }           
        }
    }
}
