using Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.AdicionarClienteAPartirDeMensagem;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.AdicionarNovaMensagem;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.ListarMensagens;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.RemoverMensagem;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao.Modelo;
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
    public class EscritoriosController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ServicoUsuarios _servicoUsuarios;

        public EscritoriosController(IMediator mediator, ServicoUsuarios servicoUsuarios)
        {
            _mediator = mediator;
            _servicoUsuarios = servicoUsuarios;
        }

        /// <summary>
        /// Lista as mensagens pendentes de um escritório
        /// </summary>
        /// <response code="200">Listagem de mensagens.</response>
        [HttpGet("mensagens")]
        [ProducesResponseType(typeof(ListagemMensagens), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            return RespostaCasoDeUso(await _mediator.Send(new ListarMensagensQuery()));
        }

        /// <summary>
        /// Criar um cliente a partir de uma mensagem recebida
        /// </summary>
        /// <param name="codigo">Código da mensagem</param>
        /// <response code="200">Código do cliente criado.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Mensagem não encontrada.</response>
        [HttpPost("mensagens/{codigo:guid}/criar-cliente")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CriarClienteDaMensagem(Guid codigo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new AdicionarClienteCommand(codigo)));
        }

        /// <summary>
        /// Remove uma mensagem recebida
        /// </summary>
        /// <param name="codigo">Código da mensagem</param>
        /// <response code="204">Mensagem removida.</response>
        /// <response code="404">Mensagem não encontrada.</response>
        [HttpDelete("mensagens/{codigo:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoverMensagem(Guid codigo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new RemoverMensagemCommand(codigo)));
        }

        /// <summary>
        /// Adiciona uma nova mensagem para o escritório jurídico
        /// </summary>
        /// <response code="200">Código da mensagem criada.</response>
        /// <response code="400">Dados inválidos.</response>
        [AllowAnonymous]
        [HttpPost("mensagens/{codigo:guid}")]  
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostMensagem(Guid codigo, AdicionarNovaMensagemCommand command)
        {
            var escritorio = new Escritorio(codigo, string.Empty);
            _servicoUsuarios.AtualizarComoUsuarioSistema(escritorio);

            command.CodigoEscritorio = codigo;
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        
        [AllowAnonymous]
        [HttpPost("processos-juridicos")]
        public async Task<ActionResult> PostCasoJuridico(AdicionarNovaMensagemCommand command)
        {
            _servicoUsuarios.AtualizarComoUsuarioSistema();
            return RespostaCasoDeUso(await _mediator.Send(command));
        }
    }
}