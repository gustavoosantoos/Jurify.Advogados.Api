using Jurify.Advogados.Api.Dominio.Entidades;
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

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.EventosAnexos.AdicionarAnexo
{
    public class AdicionarAnexoCommandHandler : BaseHandler, IRequestHandler<AdicionarAnexoCommand, RespostaCasoDeUso>
    {
        private readonly IServicoDeArmazenamento _servicoDeArmazenamento;

        public AdicionarAnexoCommandHandler(
            JurifyContext context,
            ServicoUsuarios provedor,
            IServicoDeArmazenamento servicoDeArmazenamento) : base(context, provedor)
        {
            _servicoDeArmazenamento = servicoDeArmazenamento;
        }

        public async Task<RespostaCasoDeUso> Handle(AdicionarAnexoCommand request, CancellationToken cancellationToken)
        {
            EventoProcessoJuridico eventoProcessoJuridico = await ObterEvento(request.CodigoProcessoJuridico, request.CodigoEvento);

            if (eventoProcessoJuridico == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            AnexoEventoProcessoJuridico anexo = await SalvarAnexoNaCloud(request);
            eventoProcessoJuridico.AdicionarAnexo(anexo);

            if (eventoProcessoJuridico.Invalid)
            {
                await _servicoDeArmazenamento.RemoverArquivo(anexo.Identificador);
                return RespostaCasoDeUso.ComFalha(eventoProcessoJuridico.Notifications);
            }

            await Context.SaveChangesAsync();
            return RespostaCasoDeUso.ComSucesso(anexo.Codigo);
        }

        private async Task<EventoProcessoJuridico> ObterEvento(Guid codigoProcesso, Guid codigoEvento)
        {
            return await Context.EventosProcessoJuridico
               .FirstOrDefaultAsync(c => c.Codigo == codigoEvento &&
                                         c.CodigoProcesso == codigoProcesso &&
                                         c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                         !c.Apagado);
        }

        private async Task<AnexoEventoProcessoJuridico> SalvarAnexoNaCloud(AdicionarAnexoCommand command)
        {
            var identificadorArquivo = $"anexos-processosjuridicos-{Guid.NewGuid()}";
            var urlArquivo = await _servicoDeArmazenamento.SalvarArquivo(identificadorArquivo, command.Arquivo);

            return new AnexoEventoProcessoJuridico(
                command.CodigoEvento,
                command.NomeArquivo,
                identificadorArquivo,
                urlArquivo
            );
        }
    }
}
