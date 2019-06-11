using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;
using System;
using System.Linq;
using static IdentityModel.OidcConstants;

namespace Jurify.Advogados.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication(AuthenticationSchemes.AuthorizationHeaderBearer)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["Authentication:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(Configuration["Authentication:RequireHttps"]);
                    options.ApiName = Configuration["Authentication:ResourceName"];
                });

            services.AddHealthChecks();
            services.AddSwaggerDocument(config =>
            {
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                config.AddSecurity("JWT", Enumerable.Empty<string>(),
                     new SwaggerSecurityScheme()
                     {
                         Type = SwaggerSecuritySchemeType.ApiKey,
                         Name = "Authorization",
                         In = SwaggerSecurityApiKeyLocation.Header,
                         Description = "Copy this into  the value field: \nBearer {my long token}"
                     }
                );
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseHealthChecks("/health");
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
