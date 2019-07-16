using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Jurify.Advogados.Api.Infraestrutura.Configuracoes
{
    public static class HealthChecks
    {
        public static void AdicionarHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }

        public static void UsarHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health");
        }
    }
}
