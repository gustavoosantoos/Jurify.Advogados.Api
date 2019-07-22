using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.ObterCliente
{
    public class ObterClienteQueryHandler : BaseHandler, IRequestHandler<ObterClienteQuery, RespostaCasoDeUso>
    {
        public ObterClienteQueryHandler(JurifyContext context, ProvedorUsuarioAtual provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ObterClienteQuery request, CancellationToken cancellationToken)
        {
            var cliente = await Context.Clientes
                .AsNoTracking()
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Codigo == request.Codigo &&
                                     c.CodigoEscritorio == Provedor.Escritorio.Codigo &&
                                     !c.Apagado);

            if (cliente == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            return RespostaCasoDeUso.ComSucesso(ClienteCompleto.FromEntity(cliente));
        }
    }
}
