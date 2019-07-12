using Jurify.Advogados.Api.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Jurify.Advogados.Api.Infrastructure.DependencyInjection
{
    public static class ApplicationDI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ProvedorUsuarioAtual>();
        }
    }
}
