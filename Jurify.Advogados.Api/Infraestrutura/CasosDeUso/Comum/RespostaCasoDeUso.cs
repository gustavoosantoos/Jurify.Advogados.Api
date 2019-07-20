using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum
{
    public class RespostaCasoDeUso
    {
        public bool Sucesso { get; private set; }
        public Notification[] Erros { get; private set; }
        public object Dados { get; private set; }
        public HttpStatusCode? StatusCode { get; private set; }

        public static RespostaCasoDeUso ComFalha(IEnumerable<Notification> erros)
        {
            return new RespostaCasoDeUso
            {
                Sucesso = false,
                Erros = erros.ToArray()
            };
        }

        public static RespostaCasoDeUso ComSucesso(object dados = null)
        {
            return new RespostaCasoDeUso
            {
                Sucesso = true,
                Dados = dados
            };
        }

        public static RespostaCasoDeUso ComStatusCode(HttpStatusCode statusCode, object resposta = null)
        {
            return new RespostaCasoDeUso
            {
                StatusCode = statusCode,
                Dados = resposta
            };
        }
    }
}
