using Jurify.Advogados.Api.Dominio.Servicos;
using Jurify.Advogados.Api.Infraestrutura.Servicos;
using Microsoft.Extensions.DependencyInjection;

namespace Jurify.Advogados.Api.Infraestrutura.InjecaoDependencias
{
    public static class Aplicacao
    {
        public static void AdicionarServicosDaAplicacao(this IServiceCollection services)
        {
            services.AddScoped<IServicoDeArmazenamento, ServicoDeArmazenamentoAzure>();
            services.AddScoped<IServicoDeEmail, ServicoDeEmail>();
            services.AddScoped<IServicoHash, ServicoHash>();
        }
    }
}
