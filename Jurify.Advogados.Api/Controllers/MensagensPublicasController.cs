using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.AceitarEscritorio;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.AdicionarMensagemPublica;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ListarMensagensPublicas;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.MarcarInteresse;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ReativarMensagemPublica;
using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.RemoverMensagemPublica;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Configuracoes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

        public MensagensPublicasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(AdicionarMensagemPublicaCommand command)
        {
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var query = new ListarMensagensPublicasQuery();
            return RespostaCasoDeUso(await _mediator.Send(query));
        }

        [HttpPost("{codigo:guid}/marcar-interesse")]
        public async Task<ActionResult> MarcarInteresse(Guid codigo)
        {
            var command = new MarcarInteresseCommand(codigo);
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpPost("{codigo:guid}/reativar")]
        public async Task<ActionResult> Reativar(Guid codigo)
        {
            var command = new ReativarMensagemPublicaCommand(codigo);
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpPost("{codigo:guid}/aceitar")]
        public async Task<ActionResult> AceitarEscritorio(Guid codigo)
        {
            var command = new AceitarEscritorioCommand(codigo);
            return RespostaCasoDeUso(await _mediator.Send(command));
        }

        [AllowAnonymous]
        [HttpDelete("{codigo:guid}")]
        public async Task<ActionResult> Remover(Guid codigo)
        {
            var command = new RemoverMensagemPublicaCommand(codigo);
            return RespostaCasoDeUso(await _mediator.Send(command));
        }
    }
}