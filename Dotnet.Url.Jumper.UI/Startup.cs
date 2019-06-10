using Dotnet.Url.Jumper.UI.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using Dotnet.Url.Jumper.Application.Services;
using Dotnet.Url.Jumper.Application;
using Dotnet.Url.Jumper.Infrastructure.Settings;
using Dotnet.Url.Jumper.Application.Security;
using Dotnet.Url.Jumper.UI.Security;

namespace Dotnet.Url.Jumper.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddMemoryCache();
            services.AddSingleton(Configuration);
            services.RegisterCurrentSettings(Configuration);           
            services.AddIocRegister();
            services.RegisterRepositories();
            services.AddMvc().AddNewtonsoftJson();
            services.AddControllers().AddNewtonsoftJson();           
            services.AddRazorPages();
            //Important add authentication before MVC
            services.RegisterCurrentSecuritySchema();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Dev" )
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
