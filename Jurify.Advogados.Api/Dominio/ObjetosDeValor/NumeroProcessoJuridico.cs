using System.Collections.Generic;
using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class NumeroProcessoJuridico : ObjetoDeValor
    {
        public string Numero { get; private set; }

        public NumeroProcessoJuridico(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(Numero, "NumeroProcessoJuridico.Numero", "O número do processo jurídico não deve ser vazio")
                .HasMaxLen(Numero, 30, "NumeroProcessoJuridico.Numero", "O número do processo jurídico deve ter ao máximo 30 caracteres")
            );
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Numero;
        }
    }
}
