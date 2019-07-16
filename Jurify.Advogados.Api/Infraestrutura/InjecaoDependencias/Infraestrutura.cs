using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Microsoft.Extensions.DependencyInjection;

namespace Jurify.Advogados.Api.Infraestrutura.InjecaoDependencias
{
    public static class Infraestrutura
    {
        public static void AdicionarServicosDeInfraestrutura(this IServiceCollection services)
        {
            services.AddScoped<ProvedorUsuarioAtual>();
        }
    }
}
