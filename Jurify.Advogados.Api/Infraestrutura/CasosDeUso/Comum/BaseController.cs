using Microsoft.AspNetCore.Mvc;

namespace Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum
{
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult RespostaCasoDeUso(RespostaCasoDeUso resposta)
        {
            if (resposta.StatusCode.HasValue)
            {
                return StatusCode((int) resposta.StatusCode.Value, resposta.Dados);
            }

            return resposta.Sucesso ? Ok(resposta.Dados) : BadRequest(resposta.Erros) as ActionResult;
        }
    }
}
