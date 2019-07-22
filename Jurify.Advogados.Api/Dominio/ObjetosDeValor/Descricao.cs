using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class Descricao : ObjetoDeValor
    {
        public string Valor { get; private set; }

        public Descricao(string valor)
        {
            Valor = valor;

            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(Valor, "Descricao", "A descrição não deve ser vazia")
            );
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Valor;
        }
    }
}
