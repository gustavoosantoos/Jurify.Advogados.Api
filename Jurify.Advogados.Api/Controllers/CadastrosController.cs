using Jurify.Advogados.Api.Aplicacao.ModuloCadastros.EstadosBrasileiros.Listar;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Configuracoes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors(Cors.POLICY_NAME)]
    [Route("api/[controller]")]
    public class CadastrosController : BaseController
    {
        private readonly IMediator _mediator;

        public CadastrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("estados-brasileiros")]
        public async Task<ActionResult> GetEstadosBrasileiros()
        {
            return RespostaCasoDeUso(await _mediator.Send(new ListarEstadosBrasileirosQuery()));
        }
    }
}