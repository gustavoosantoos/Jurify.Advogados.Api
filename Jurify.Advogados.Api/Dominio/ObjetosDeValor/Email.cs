using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class Email : ObjetoDeValor
    {
        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            Endereco = endereco;

            AddNotifications(new Contract()
                .IfNotNull(Endereco, c => c.IsEmailOrEmpty(Endereco, "Email.Endereco", "E-mail inválido"))
                .HasMaxLengthIfNotNullOrEmpty(Endereco, 256, "Email.Endereco", "O e-mail deve ter ao máximo 256 caracteres")
            );
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Endereco;
        }
    }
}
