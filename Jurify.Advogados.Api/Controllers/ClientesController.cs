using Jurify.Advogados.Api.Aplicacao.Clientes.CadastrarCliente;
using Jurify.Advogados.Api.Aplicacao.Clientes.ListarClientes;
using Jurify.Advogados.Api.Aplicacao.Clientes.ObterCliente;
using Jurify.Advogados.Api.Aplicacao.Clientes.RemoverCliente;
using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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

        [HttpGet]
        [ProducesResponseType(typeof(ClientePreview[]), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            return RespostaCasoDeUso(await _mediator.Send(new ListarClientesQuery()));
        }

        [HttpGet("{codigo:guid}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(Guid codigo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new ObterClienteQuery(codigo)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(CadastrarClienteCommand command)
        {
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        [HttpDelete("{codigo:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid codigo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new RemoverClienteCommand(codigo)));
        }
    }
}