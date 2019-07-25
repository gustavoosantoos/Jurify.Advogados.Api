using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class DescricaoCurta : ObjetoDeValor
    {
        public string Valor { get; private set; }

        public DescricaoCurta(string valor)
        {
            Valor = valor;

            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(Valor, "Descricao", "A descrição não deve ser vazia")
                .HasMaxLen(Valor, 100, "Descricao", "A descrição deve ter ao máximo 100 caracteres")
            );
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Valor;
        }
    }
}
