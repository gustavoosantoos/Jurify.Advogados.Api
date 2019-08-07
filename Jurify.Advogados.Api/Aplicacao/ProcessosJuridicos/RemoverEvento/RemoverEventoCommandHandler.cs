using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.RemoverEvento
{
    public class RemoverEventoCommandHandler : BaseHandler, IRequestHandler<RemoverEventoCommand, RespostaCasoDeUso>
    {
        public RemoverEventoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(RemoverEventoCommand request, CancellationToken cancellationToken)
        {
            var evento = await Context.EventosProcessoJuridico
                .FirstOrDefaultAsync(c => c.Codigo == request.CodigoEvento &&
                                          c.CodigoProcesso == request.CodigoProcesso &&
                                          c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !c.Apagado);

            if (evento == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            Context.EventosProcessoJuridico.Remove(evento);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso();
        }
    }
}
