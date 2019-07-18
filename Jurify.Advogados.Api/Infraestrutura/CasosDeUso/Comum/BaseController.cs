using Microsoft.AspNetCore.Mvc;

namespace Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum
{
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult RespostaCasoDeUso(RespostaCasoDeUso resposta)
        {
            return resposta.Sucesso ? Ok(resposta.Dados) : BadRequest(resposta.Erros) as ActionResult;
        }
    }
}
