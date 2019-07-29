using Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.CadastrarProcessoJuridico;
using Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.ListarProcessosJuridicos;
using Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.ObterProcessoJuridico;
using Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.ObterProcessoJuridico.Models;
using Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.RemoverProcessoJuridico;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessosJuridicosController : BaseController
    {
        private readonly IMediator _mediator;

        public ProcessosJuridicosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna uma listagem de processos com poucos dados.
        /// </summary>
        /// <response code="200">Listagem de processos jurídicos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ProcessoJuridicoPreview[]), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            return RespostaCasoDeUso(await _mediator.Send(new ListarProcessosJuridicosQuery()));
        }

        /// <summary>
        /// Retorna um processo específico com todos os campos
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <response code="200">Dados do processo</response>
        /// <response code="404">Processo não encontrado</response>
        [HttpGet("{codigo:guid}")]
        [ProducesResponseType(typeof(ProcessoJuridico), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(Guid codigo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new ObterProcessoJuridicoQuery(codigo)));
        }

        /// <summary>
        /// Cadastra um novo processo jurídico
        /// </summary>
        /// <param name="command">Dados do novo processo</param>
        /// <response code="200">Processo criado</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(CadastrarProcessoJuridicoCommand command)
        {
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Remove um processo jurídico
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <response code="204">Processo removido</response>
        /// <response code="404">Processo não encontrado</response>
        [HttpDelete("{codigo:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid codigo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new RemoverProcessoJuridicoCommand(codigo)));
        }
    }
}