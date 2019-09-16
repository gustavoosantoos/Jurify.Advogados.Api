using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Jurify.Advogados.Api.Infraestrutura.InjecaoDependencias
{
    public static class Infraestrutura
    {
        public static void AdicionarServicosDeInfraestrutura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(config =>
            {
                config.ClearProviders();
                config.AddDebug();
            });
            
            services.AddDbContext<JurifyContext>();

            services.AddHttpClient("AUTENTICADOR_API", config =>
            {
                config.BaseAddress = new Uri($"{configuration["Authentication:Authority"]}/api/advogados/");
            });

            services.AddScoped<ServicoUsuarios>();
            services.AddMediatR(typeof(Startup).Assembly);
        }
    }
}
