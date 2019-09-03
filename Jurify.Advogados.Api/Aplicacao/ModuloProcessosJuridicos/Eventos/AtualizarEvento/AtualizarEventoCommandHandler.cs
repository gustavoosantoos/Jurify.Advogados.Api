using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.AtualizarEvento
{
    public class AtualizarEventoCommandHandler : BaseHandler, IRequestHandler<AtualizarEventoCommand, RespostaCasoDeUso>
    {
        public AtualizarEventoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AtualizarEventoCommand request, CancellationToken cancellationToken)
        {
            var evento = await Context.EventosProcessoJuridico
                .FirstOrDefaultAsync(e => e.Codigo == request.CodigoEvento &&
                                          e.CodigoProcesso == request.CodigoProcessoJuridico &&
                                          e.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !e.Apagado);

            if (evento == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            evento.AtualizarDescricao(new Descricao(request.Descricao));

            if (evento.Invalid)
            {
                return RespostaCasoDeUso.ComFalha(evento.Notifications);
            }

            await Context.SaveChangesAsync();
            return RespostaCasoDeUso.ComSucesso(evento.Codigo);
        }
    }
}
