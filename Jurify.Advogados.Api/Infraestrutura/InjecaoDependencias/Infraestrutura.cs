using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jurify.Advogados.Api.Infraestrutura.InjecaoDependencias
{
    public static class Infraestrutura
    {
        public static void AdicionarServicosDeInfraestrutura(this IServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.ClearProviders();
                config.AddDebug();
            });

            services.AddDbContext<JurifyContext>();
            services.AddScoped<ProvedorUsuarioAtual>();
            services.AddMediatR(typeof(Startup).Assembly);
        }
    }
}
