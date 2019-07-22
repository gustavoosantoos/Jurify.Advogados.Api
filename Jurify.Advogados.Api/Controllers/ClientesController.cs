using Jurify.Advogados.Api.Aplicacao.Clientes.CadastrarCliente;
using Jurify.Advogados.Api.Aplicacao.Clientes.ListarClientes;
using Jurify.Advogados.Api.Aplicacao.Clientes.ObterCliente;
using Jurify.Advogados.Api.Aplicacao.Clientes.RemoverCliente;
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
    public class ClientesController : BaseController
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retorna uma listagem de clientes com poucos dados.
        /// </summary>
        /// <response code="200">Listagem de clientes.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ClientePreview[]), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            return RespostaCasoDeUso(await _mediator.Send(new ListarClientesQuery()));
        }

        /// <summary>
        /// Retorna um cliente específico com todos os campos
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <response code="200">Dados do cliente</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpGet("{codigo:guid}")]
        [ProducesResponseType(typeof(ClienteCompleto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(Guid codigo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new ObterClienteQuery(codigo)));
        }

        /// <summary>
        /// Cadastra um novo cliente
        /// </summary>
        /// <param name="command">Dados do cliente</param>
        /// <response code="200">Cliente criado</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(CadastrarClienteCommand command)
        {
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Remove um cliente
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <response code="204">Cliente removido</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpDelete("{codigo:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid codigo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new RemoverClienteCommand(codigo)));
        }
    }
}