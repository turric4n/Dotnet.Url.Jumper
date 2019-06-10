using AspNet.Security.ApiKey.Providers;
using AspNet.Security.ApiKey.Providers.Events;
using AspNet.Security.ApiKey.Providers.Extensions;
using Dotnet.Url.Jumper.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dotnet.Url.Jumper.UI.Extensions
{
    public static class ApikeyAuthorization
    {
        public static void AddApiKeyAuthorization(this IServiceCollection services, ISecurityValidatorService secretKeyFinderService)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = ApiKeyDefaults.AuthenticationScheme;
            })
            .AddApiKey(options =>
            {
                var myapikeyvalidator = secretKeyFinderService;
                options.Header = "X-API-KEY";
                options.HeaderKey = String.Empty;
                options.Events = new ApiKeyEvents
                {
                    OnApiKeyValidated = context =>
                    {
                        try
                        {
                            myapikeyvalidator.ValidateSecret(context.ApiKey);
                            context.Principal = new ClaimsPrincipal();
                            context.Success();
                        }
                        catch (Exception ex)
                        {
                            context.Fail(ex);
                        }                        
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure is NotSupportedException)
                        {
                            context.StatusCode = HttpStatusCode.UpgradeRequired;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
