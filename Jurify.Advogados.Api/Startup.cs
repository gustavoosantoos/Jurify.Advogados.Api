using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.Configuracoes;
using Jurify.Advogados.Api.Infraestrutura.InjecaoDependencias;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jurify.Advogados.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AdicionarCors();
            services.AdicionarAutenticacao(Configuration);
            services.AdicionarHealthChecks();
            services.AdicionarDocumentacaoSwagger();
            services.AdicionarServicosDeInfraestrutura(Configuration);
            services.AdicionarServicosDaAplicacao();

            services.AddRouting(config => config.LowercaseUrls = true);
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(Cors.POLICY_NAME));
                options.Filters.Add(typeof(UsuarioAtualFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsarCors();
            app.UsarAutenticacao();
            app.UsarHealthChecks();
            app.UsarDocumentacaoSwagger();
            app.UseMvc();
        }
    }
}
