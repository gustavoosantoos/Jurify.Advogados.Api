using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.AdicionarAnexo;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.BaixarAnexo;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.RemoverAnexo;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Atualizar;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Cadastrar;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Listar;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Listar.Models;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Obter;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Obter.Models;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Remover;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Enderecos.AdicionarEndereco;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Enderecos.AtualizarEndereco;
using Jurify.Advogados.Api.Aplicacao.ModuloClientes.Enderecos.RemoverEndereco;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Configuracoes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors(Cors.POLICY_NAME)]
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
        [ProducesResponseType(typeof(ListagemClientes), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
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
        /// Atualiza um cliente
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <param name="command">Dados do cliente</param>
        /// <response code="200">Código do cliente atualizado</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpPut("{codigo:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromRoute] Guid codigo, AtualizarClienteCommand command)
        {
            command.Codigo = codigo;
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

        /// <summary>
        /// Adiciona um endereço ao cliente
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <param name="command">Novo endereço</param>
        /// <response code="200">Endereço adicionado</response>
        /// <response code="400">Endereço inválido</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpPost("{codigo:guid}/enderecos")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PostEndereco([FromRoute] Guid codigo, AdicionarEnderecoCommand command)
        {
            command.CodigoCliente = codigo;
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Atualiza um endereço do cliente
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <param name="codigoEndereco">Código do endereço</param>
        /// <param name="command">Dados do endereço</param>
        /// <response code="200">Código do endereço atualizado</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Cliente e/ou endereço não encontrado</response>
        [HttpPut("{codigo:guid}/enderecos/{codigoEndereco:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutEndereco([FromRoute] Guid codigo, [FromRoute] Guid codigoEndereco, AtualizarEnderecoCommand command)
        {
            command.Codigo = codigoEndereco;
            command.CodigoCliente = codigo;
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Remove um endereço do cliente
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <param name="codigoEndereco">Código do endereço</param>
        /// <response code="204">Endereço removido</response>
        /// <response code="404">Endereço ou cliente não encontrado</response>
        [HttpDelete("{codigo:guid}/enderecos/{codigoEndereco:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteEndereco(Guid codigo, Guid codigoEndereco)
        {
            return RespostaCasoDeUso(await _mediator.Send(new RemoverEnderecoCommand(codigo, codigoEndereco)));
        }

        /// <summary>
        /// Obtem um anexo do cliente e inicia o download
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <param name="codigoAnexo">Código do anexo</param>
        /// <response code="200">Arquivo a ser baixado</response>
        /// <response code="404">Cliente ou anexo não encontrado</response>
        [HttpGet("{codigo:guid}/anexos/{codigoAnexo:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAnexo(Guid codigo, Guid codigoAnexo)
        {
            var query = new BaixarAnexoQuery(codigo, codigoAnexo);
            var response = await _mediator.Send(query);

            if (response.Sucesso)
            {
                var anexo = (Aplicacao.ModuloClientes.Anexos.BaixarAnexo.Anexo) response.Dados;
                return File(anexo.Arquivo, "application/octet-stream", anexo.NomeDoArquivo);
            }

            return RespostaCasoDeUso(response);
        }

        /// <summary>
        /// Recebe um arquivo para anexar ao cliente
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <param name="file">Arquivo</param>
        /// <response code="200">Código do anexo inserido</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="500">Erro ao fazer upload do anexo</response>
        [HttpPost("{codigo:guid}/anexos")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostAnexo(Guid codigo, IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var command = new AdicionarAnexoCommand(codigo, file.FileName, stream);
                return RespostaCasoDeUso(await _mediator.Send(command));
            }
        }

        /// <summary>
        /// Remove um anexo do cliente
        /// </summary>
        /// <param name="codigo">Código do cliente</param>
        /// <param name="codigoAnexo">Código do anexo</param>
        /// <response code="204">Anexo removido com sucesso</response>
        /// <response code="404">Cliente ou anexo não encontrado</response>
        /// <response code="500">Erro ao remover anexo</response>
        [HttpDelete("{codigo:guid}/anexos/{codigoAnexo:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAnexo(Guid codigo, Guid codigoAnexo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new RemoverAnexoCommand(codigo, codigoAnexo)));
        }
    }
}