using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.AdicionarEvento;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.AtualizarEvento;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.RemoverEvento;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.EventosAnexos.AdicionarAnexo;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.EventosAnexos.BaixarAnexo;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.EventosAnexos.RemoverAnexo;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Atualizar;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Cadastrar;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Listar;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Obter;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Obter.Models;
using Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Remover;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Configuracoes;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Controllers
{
    [ApiController]
    [EnableCors(Cors.POLICY_NAME)]
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
        /// Atualiza os dados básicos de um processo jurídico
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <param name="command">Dados do processo</param>
        /// <response code="200">Processo atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Processo não encontrado</response>
        [HttpPut("{codigo:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(Guid codigo, AtualizarProcessoJuridicoCommand command)
        {
            command.Codigo = codigo;
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

        /// <summary>
        /// Adiciona um evento ao processo jurídico
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <param name="command">Novo evento</param>
        /// <response code="200">Evento adicionado</response>
        /// <response code="400">Evento inválido</response>
        /// <response code="404">Processo não encontrado</response>
        [HttpPost("{codigo:guid}/eventos")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PostEvento([FromRoute] Guid codigo, AdicionarEventoCommand command)
        {
            command.CodigoProcessoJuridico = codigo;
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        /// <summary>
        /// Atualiza um evento de um processo jurídico
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <param name="codigoEvento">Código do evento</param>
        /// <param name="command">Dados do evento</param>
        /// <response code="200">Evento atualizado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Processo e/ou evento não encontrado</response>
        [HttpPut("{codigo:guid}/eventos/{codigoEvento:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutEvento(Guid codigo, Guid codigoEvento, AtualizarEventoCommand command)
        {
            command.CodigoProcessoJuridico = codigo;
            command.CodigoEvento = codigoEvento;
            return RespostaCasoDeUso(await _mediator.Send(command));
        }


        /// <summary>
        /// Remove um evento no processo jurídico
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <param name="codigoEvento">Código do evento</param>
        /// <response code="204">Evento removido</response>
        /// <response code="404">Processo e/ou evento não encontrado</response>
        [HttpDelete("{codigo:guid}/eventos/{codigoEvento:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteEvento(Guid codigo, Guid codigoEvento)
        {
            return RespostaCasoDeUso(await _mediator.Send(new RemoverEventoCommand(codigo, codigoEvento)));
        }

        /// <summary>
        /// Obtem um anexo de um evento e inicia o download
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <param name="codigoEvento">Código do evento</param>
        /// <param name="codigoAnexo">Código do anexo</param>
        /// <response code="200">Arquivo a ser baixado</response>
        /// <response code="404">Evento ou anexo não encontrado</response>
        [HttpGet("{codigo:guid}/eventos/{codigoEvento:guid}/anexos/{codigoAnexo:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAnexo(Guid codigo, Guid codigoEvento, Guid codigoAnexo)
        {
            var query = new BaixarAnexoQuery(codigo, codigoEvento, codigoAnexo);
            var response = await _mediator.Send(query);

            if (response.Sucesso)
            {
                var anexo = (Aplicacao.ModuloProcessosJuridicos.EventosAnexos.BaixarAnexo.Anexo)response.Dados;
                return File(anexo.Arquivo, "application/octet-stream", anexo.NomeDoArquivo);
            }

            return RespostaCasoDeUso(response);
        }

        /// <summary>
        /// Recebe um arquivo para anexar a um evento
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <param name="codigoEvento">Código do evento</param>
        /// <param name="file">Arquivo</param>
        /// <response code="200">Código do anexo inserido</response>
        /// <response code="404">Evento não encontrado</response>
        /// <response code="500">Erro ao fazer upload do anexo</response>
        [HttpPost("{codigo:guid}/eventos/{codigoEvento:guid}/anexos")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostAnexo(Guid codigo, Guid codigoEvento, IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var command = new AdicionarAnexoCommand(codigo, codigoEvento, file.FileName, stream);
                return RespostaCasoDeUso(await _mediator.Send(command));
            }
        }

        /// <summary>
        /// Remove um anexo de um evento de um processo jurídico
        /// </summary>
        /// <param name="codigo">Código do processo</param>
        /// <param name="codigoEvento">Código do evento</param>
        /// <param name="codigoAnexo">Código do anexo</param>
        /// <response code="204">Anexo removido com sucesso</response>
        /// <response code="404">Evento ou anexo não encontrado</response>
        /// <response code="500">Erro ao remover anexo</response>
        [HttpDelete("{codigo:guid}/eventos/{codigoEvento:guid}/anexos/{codigoAnexo:guid}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAnexo(Guid codigo, Guid codigoEvento, Guid codigoAnexo)
        {
            return RespostaCasoDeUso(await _mediator.Send(new RemoverAnexoCommand(codigo, codigoEvento, codigoAnexo)));
        }
    }
}