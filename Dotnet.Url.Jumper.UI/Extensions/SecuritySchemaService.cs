using Dotnet.Url.Jumper.Application.Security;
using Dotnet.Url.Jumper.Application.Services;
using Dotnet.Url.Jumper.UI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Dotnet.Url.Jumper.UI.Security
{
    public static class SecuritySchemaService
    {
        public static void RegisterCurrentSecuritySchema(this IServiceCollection services)
        {
            var schema = Enum.Parse<SecuritySchema>(services.BuildServiceProvider().GetRequiredService<IOptions<SecuritySettings>>().Value.securitySchema);
            switch (schema)
            {
                case SecuritySchema.JWT:
                    services.AddScoped<ISecurityValidatorService, JWTSecurityValidatorService>();
                    services.AddScoped<ITokenGeneratorService, JWTGeneratorByIdUserService>();
                    services.AddBearerAuthentication(services.BuildServiceProvider().GetRequiredService<ISecurityValidatorService>());
                    break;
                case SecuritySchema.ApiKey:
                    services.AddScoped<ISecurityValidatorService, ApiKeySecretValidatorService>();
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                        options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                    })
                        .AddApiKeySupport(options => { });
                    break;
            }           
        }
    }
}
