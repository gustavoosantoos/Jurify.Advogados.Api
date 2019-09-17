using Jurify.Advogados.Api.Aplicacao.ModuloAgenda.Agenda.AdicionarCompromisso;
using Jurify.Advogados.Api.Aplicacao.ModuloAgenda.Agenda.ListarCompromissos;
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
    [EnableCors(Cors.POLICY_NAME)]
    [Route("api/[controller]")]
    public class AgendaController : BaseController
    {
        private readonly IMediator _mediator;

        public AgendaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna a listagem de compromissos do usuário atual
        /// </summary>
        /// <response code="200">Listagem de compromissos do usuário.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Compromisso[]), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            return RespostaCasoDeUso(await _mediator.Send(new ListarCompromissosQuery()));
        }

        /// <summary>
        /// Endpoint para salvar um novo compromisso na agenda
        /// </summary>
        /// <param name="command">Dados do novo compromisso</param>
        /// <response code="200">Compromisso salvo com sucesso</response>
        /// <response code="400">Dados do compromisso inválidos</response>
        /// <response code="404">Cliente à vincular não encontrado</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Post(AdicionarCompromissoCommand command)
        {
            return RespostaCasoDeUso(await _mediator.Send(command));
        }
    }
}