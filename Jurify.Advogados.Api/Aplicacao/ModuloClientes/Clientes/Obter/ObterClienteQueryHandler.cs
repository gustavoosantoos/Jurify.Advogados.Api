using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Obter.Models;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Obter
{
    public class ObterClienteQueryHandler : BaseHandler, IRequestHandler<ObterClienteQuery, RespostaCasoDeUso>
    {
        public ObterClienteQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ObterClienteQuery request, CancellationToken cancellationToken)
        {
            var cliente = await Context.Clientes
                .IncludeFilter(c => c.Enderecos.Where(e => !e.Apagado))
                .IncludeFilter(c => c.Anexos.Where(a => !a.Apagado))
                .IncludeFilter(c => c.Processos.Where(a => !a.Apagado))
                .FirstOrDefaultAsync(c => c.Codigo == request.Codigo &&
                                     c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                     !c.Apagado);

            if (cliente == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            return RespostaCasoDeUso.ComSucesso(await Cliente.FromEntity(cliente, ServicoUsuarios));
        }
    }
}
