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
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["Authentication:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["Authentication:RequireHttps"]);
                    options.ApiName = configuration["Authentication:ResourceName"];
                });
        }

        public static void UsarAutenticacao(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
