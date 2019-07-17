using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jurify.Advogados.Api.Infraestrutura.InjecaoDependencias
{
    public static class Infraestrutura
    {
        public static void AdicionarServicosDeInfraestrutura(this IServiceCollection services)
        {
            services.AddDbContext<JurifyContext>();
            services.AddScoped<ProvedorUsuarioAtual>();
            services.AddMediatR(typeof(Startup).Assembly);
        }
    }
}
