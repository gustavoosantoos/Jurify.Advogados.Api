using Flunt.Br.Validation;
using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class CPF : ObjetoDeValor
    {
        public string Numero { get; private set; }

        public CPF(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract()
                .IfNotNull(Numero, c => c.IsCpf(Numero, "CPF.Numero", "CPF inválido"))
            );
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Numero;
        }
    }
}
