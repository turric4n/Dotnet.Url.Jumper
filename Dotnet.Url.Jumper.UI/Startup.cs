using Dotnet.Url.Jumper.UI.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dotnet.Url.Jumper.UI.Security;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using QuickLogger.Extensions.NetCore;

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
            //Core services
            services.AddQuickLogger();
            services.AddMemoryCache();
            services.AddSingleton(Configuration);
            services.RegisterCurrentSettings(Configuration);
            services.AddHttpContextAccessor();                       
            services.AddMvc()
                .AddNewtonsoftJson();
            services.AddControllers()
                .AddNewtonsoftJson();               
            services.AddRazorPages();            
            //Swagger Generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dotnet Url Shortener/Jumper", Version = "v1" });
                c.AddSecurityDefinition("X-API-KEY", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the ApiKey scheme. Example: \"X-API-KEY: {token}\"",                    
                    In = ParameterLocation.Header,
                    Name = "X-API-KEY",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //c.OperationFilter<Open>();

            });
            //Important add authentication before MVC
            services.RegisterCurrentSecuritySchema();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Repositories
            services.RegisterRepositories();
            //Own Services
            services.AddIocRegister();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development" )
            {
                app.UseDeveloperExceptionPage();
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dotnet Url Shortener/Jumper");
                });
            }
            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
