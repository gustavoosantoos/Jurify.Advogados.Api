using Jurify.Advogados.Api.Aplicacao.ModuloAgenda.Agenda.AdicionarCompromisso;
using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Dominio.Servicos;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.AdicionarEvento
{
    public class AdicionarEventoCommandHandler : BaseHandler, IRequestHandler<AdicionarEventoCommand, RespostaCasoDeUso>
    {
        private readonly IServicoDeArmazenamento _servicoDeArmazenamento;
        private readonly IMediator _mediator;

        public AdicionarEventoCommandHandler(
            JurifyContext context,
            ServicoUsuarios provedor,
            IServicoDeArmazenamento servicoDeArmazenamento,
            IMediator mediator) : base(context, provedor)
        {
            _servicoDeArmazenamento = servicoDeArmazenamento;
            _mediator = mediator;
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

            if (request.AdicionarNaAgenda && request.DataHoraEvento > DateTime.Now)
            {
                var respostaCompromisso = await AdicionarCompromissoNaAgenda(processo, request);
                if (!respostaCompromisso.Sucesso)
                {
                    return RespostaCasoDeUso.ComFalha(respostaCompromisso.Erros);
                }
            }

            await Context.SaveChangesAsync();
            return RespostaCasoDeUso.ComSucesso(evento.Codigo);
        }

        private async Task<RespostaCasoDeUso> AdicionarCompromissoNaAgenda(ProcessoJuridico processo, AdicionarEventoCommand request)
        {
            var compromissoCommand = new AdicionarCompromissoCommand()
            {
                CodigoCliente = processo.CodigoCliente,
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Inicio = request.DataHoraEvento
            };

            return await _mediator.Send(compromissoCommand);
        }
    }
}
