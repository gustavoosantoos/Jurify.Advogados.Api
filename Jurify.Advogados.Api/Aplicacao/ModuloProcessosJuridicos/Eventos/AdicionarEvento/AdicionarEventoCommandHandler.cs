using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.AdicionarEvento
{
    public class AdicionarEventoCommandHandler : BaseHandler, IRequestHandler<AdicionarEventoCommand, RespostaCasoDeUso>
    {
        public AdicionarEventoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AdicionarEventoCommand request, CancellationToken cancellationToken)
        {
            var processo = await Context.ProcessosJuridicos
                .FirstOrDefaultAsync(c => c.Codigo == request.CodigoProcessoJuridico &&
                                          c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !c.Apagado);

            if (processo == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            var evento = request.AsEntity();
            processo.AdicionarEvento(evento);

            if (processo.Invalid)
                return RespostaCasoDeUso.ComFalha(processo.Notifications);

            await Context.SaveChangesAsync();
            return RespostaCasoDeUso.ComSucesso(evento.Codigo);
        }
    }
}
