using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infrastructure.Authentication
{
    public class ProvedorUsuarioAtualFilter : IActionFilter
    {
        private readonly ProvedorUsuarioAtual _provedor;

        public ProvedorUsuarioAtualFilter(ProvedorUsuarioAtual provedor)
        {
            _provedor = provedor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claims = context.HttpContext.User;
           
            var usuario = new UsuarioAtual(
                Guid.Parse(claims.FindFirst("user_id").Value),
                claims.FindFirst("user_first_name").Value,
                claims.FindFirst("user_last_name").Value,
                new EscritorioAtual(
                    Guid.Parse(claims.FindFirst("office_id").Value),
                    claims.FindFirst("office_name").Value
                )
            );

            _provedor.AtualizarUsuario(usuario);
            _provedor.AtualizarUsuario(usuario);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
