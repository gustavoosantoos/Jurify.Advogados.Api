using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.AceitarEscritorio;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.AdicionarMensagemPublica;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ListarMensagensPublicas;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.MarcarInteresse;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ReativarMensagemPublica;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.RemoverMensagemPublica;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Configuracoes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors(Cors.POLICY_NAME)]
    public class MensagensPublicasController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ServicoUsuarios _servicoUsuarios;

        public MensagensPublicasController(IMediator mediator, ServicoUsuarios servicoUsuarios)
        {
            _mediator = mediator;
            _servicoUsuarios = servicoUsuarios;
        }

        /// <summary>
        /// Adiciona uma nova mensagem pública
        /// </summary>
        /// <param name="command">Dados da mensagem</param>
        /// <response code="200">Mensagem salva com sucesso</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<ActionResult> Post(AdicionarMensagemPublicaCommand command)
        {
            _servicoUsuarios.AtualizarComoUsuarioSistema();
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Lista as mensagens públicas e mensagens do escritório
        /// </summary>
        /// <response code="200">Listagem de mensagens</response>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var query = new ListarMensagensPublicasQuery();
            return RespostaCasoDeUso(await _mediator.Send(query));
        }

        /// <summary>
        /// Escritório marca interesse em uma mensagem pública
        /// </summary>
        /// <response code="200">Interesse registrado</response>
        /// <response code="404">Mensagem não encontrada</response>
        [HttpPost("{codigo:guid}/marcar-interesse")]
        public async Task<ActionResult> MarcarInteresse(Guid codigo)
        {
            var command = new MarcarInteresseCommand(codigo);
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Reativa uma mensagem para o estado público
        /// </summary>
        /// <param name="codigo">Código da mensagem</param>
        /// <response code="200">Mensagem reativada</response>
        /// <response code="400">Mensagem não encontrada</response>
        [AllowAnonymous]
        [HttpPost("{codigo:guid}/reativar")]
        public async Task<ActionResult> Reativar(Guid codigo)
        {
            _servicoUsuarios.AtualizarComoUsuarioSistema();
            var command = new ReativarMensagemPublicaCommand(codigo);
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Aceita o escritório atual de uma mensagem
        /// </summary>
        /// <param name="codigo">Código da mensagem</param>
        /// <response code="200">Mensagem aceita</response>
        /// <response code="404">Mensagem não encontrada</response>
        [AllowAnonymous]
        [HttpPost("{codigo:guid}/aceitar")]
        public async Task<ActionResult> AceitarEscritorio(Guid codigo)
        {
            _servicoUsuarios.AtualizarComoUsuarioSistema();
            var command = new AceitarEscritorioCommand(codigo);
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Remove uma mensagem pública
        /// </summary>
        /// <param name="codigo">Código da mensagem</param>
        /// <response code="204">Mensagem removida</response>
        /// <response code="404">Mensagem não encontrada</response>
        [AllowAnonymous]
        [HttpDelete("{codigo:guid}")]
        public async Task<ActionResult> Remover(Guid codigo)
        {
            _servicoUsuarios.AtualizarComoUsuarioSistema();
            var command = new RemoverMensagemPublicaCommand(codigo);
            return RespostaCasoDeUso(await _mediator.Send(command));
        }
    }
}