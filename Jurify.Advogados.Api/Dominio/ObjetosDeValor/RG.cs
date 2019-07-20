using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class RG : ObjetoDeValor
    {
        public string Numero { get; private set; }

        public RG(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract()
                .HasMaxLengthIfNotNullOrEmpty(Numero, 15, "RG.Numero", "O RG deve ter no máximo 15 caracteres")
            );
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Numero;
        }
    }
}
