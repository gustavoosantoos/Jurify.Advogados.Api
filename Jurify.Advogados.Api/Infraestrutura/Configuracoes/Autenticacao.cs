using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using static IdentityModel.OidcConstants;

namespace Jurify.Advogados.Api.Infraestrutura.Configuracoes
{
    public static class Autenticacao
    {
        public static void AdicionarAutenticacao(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(AuthenticationSchemes.AuthorizationHeaderBearer)
                .AddJwtBearer(options =>
                {
                    options.Audience = configuration["Authentication:ResourceName"];
                    options.RequireHttpsMetadata = false;
                    options.Authority = configuration["Authentication:Authority"];
                });
        }

        public static void UsarAutenticacao(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
