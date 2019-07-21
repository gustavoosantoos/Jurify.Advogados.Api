using Jurify.Advogados.Api.Dominio.Base;
using System;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Exceptions
{
    public class DomainException : Exception
    {
        public string[] Erros { get; private set; }

        public DomainException(Entidade entidade) : base($"O objeto do tipo {entidade.GetType()} é inválido")
        {
            Erros = entidade.Notifications.Select(n => n.Message).ToArray();
        }

        public DomainException(ObjetoDeValor objeto) : base($"O objeto do tipo {objeto.GetType()} é inválido")
        {
            Erros = objeto.Notifications.Select(n => n.Message).ToArray();
        }
    }
}
