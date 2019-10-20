using Jurify.Advogados.Api.Infraestrutura.Autenticacao.Modelo;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao
{
    public class UsuarioAtualFilter : IActionFilter
    {
        private readonly ServicoUsuarios _provedor;

        public UsuarioAtualFilter(ServicoUsuarios provedor)
        {
            _provedor = provedor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claims = context.HttpContext.User;

            if (!claims.Claims.Any())
            {
                return;
            }

            var usuario = new Usuario(
                Guid.Parse(claims.FindFirst("user_id").Value),
                claims.FindFirst("user_first_name").Value,
                claims.FindFirst("user_last_name").Value
            );

            var escritorio = new Escritorio(
                Guid.Parse(claims.FindFirst("office_id").Value),
                claims.FindFirst("office_name").Value
            );

            _provedor.AtualizarUsuario(usuario, escritorio);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
