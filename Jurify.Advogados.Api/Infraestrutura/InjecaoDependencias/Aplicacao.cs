using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Jurify.Advogados.Api.Infraestrutura.InjecaoDependencias
{
    public static class Aplicacao
    {
        public static void AdicionarServicosDaAplicacao(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);
        }
    }
}
