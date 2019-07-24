using Jurify.Advogados.Api.Dominio.Base;
using System;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Exceptions
{
    public class DomainException : Exception
    {
        public string[] Erros { get; private set; }

        public DomainException(Entidade entidade) : base($"O objeto do tipo {entidade?.GetType()} é inválido")
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "A entidade não deve ser nula");

            Erros = entidade.Notifications.Select(n => n.Message).ToArray();
        }

        public DomainException(ObjetoDeValor objeto) : base($"O objeto do tipo {objeto?.GetType()} é inválido")
        {
            if (objeto == null)
                throw new ArgumentNullException(nameof(objeto), "O objeto de valor não deve ser nulo");

            Erros = objeto.Notifications.Select(n => n.Message).ToArray();
        }
    }
}
