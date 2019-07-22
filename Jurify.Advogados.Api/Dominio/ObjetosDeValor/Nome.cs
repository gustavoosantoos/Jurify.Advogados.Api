using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class Nome : ObjetoDeValor
    {
        public string PrimeiroNome { get; private set; }
        public string Sobrenome { get; private set; }

        protected Nome()
        {

        }

        public Nome(string primeiroNome, string ultimoNome)
        {
            PrimeiroNome = primeiroNome;
            Sobrenome = ultimoNome;

            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(PrimeiroNome, "Nome.PrimeiroNome", "O nome não deve ser vazio")
                    .HasMaxLen(PrimeiroNome, 100, "Nome.PrimeiroNome", "O nome deve ter ao máximo 100 caracteres")
                .IsNotNullOrWhiteSpace(Sobrenome, "Nome.Sobrenome", "O sobrenome não deve ser vazio")
                    .HasMaxLen(Sobrenome, 200, "Nome.Sobrenome", "O sobrenome deve ter ao máximo 200 caracteres")
            );
        }

        public string ObterNomeCompleto()
        {
            return $"{PrimeiroNome} {Sobrenome}";
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return PrimeiroNome;
            yield return Sobrenome;
        }
    }
}
