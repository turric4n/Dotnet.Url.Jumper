using Dotnet.Url.Jumper.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Url.Jumper.UI.Extensions
{
    public static class ApikeyAuthorization
    {
        public static void AddApiKeyAuthorization(this IServiceCollection services, ISecurityValidatorService secretKeyFinderService)
        {
            services.AddTransient<IAuthorizationHandler, ApiKeyRequirementHandler>();
            services.AddAuthorization(authConfig =>
            {
                authConfig.AddPolicy("ApiKeyPolicy",
                    policyBuilder => policyBuilder
                        .AddRequirements(new ApiKeyRequirement(secretKeyFinderService.GetSecrets())));
            });
        }
    }
}
