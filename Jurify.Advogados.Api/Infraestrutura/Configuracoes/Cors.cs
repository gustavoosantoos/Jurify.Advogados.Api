using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Jurify.Advogados.Api.Infraestrutura.Configuracoes
{
    public static class Cors
    {
        public const string POLICY_NAME = "jurify_api_policy";

        public static void AdicionarCors(this IServiceCollection services)
        {
            services.AddCors(config =>
            {
                config.AddPolicy(POLICY_NAME, policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowCredentials()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("WWW-Authenticate", "www-authenticate");
                });
            });
        }

        public static void UsarCors(this IApplicationBuilder app)
        {
            app.UseCors(POLICY_NAME);
        }
    }
}
