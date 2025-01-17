﻿using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class Descricao : ObjetoDeValor
    {
        public string Valor { get; private set; }

        private Descricao()
        {
            Valor = string.Empty;
        }

        public Descricao(string valor)
        {
            Valor = valor;

            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(Valor, "Descricao", "A descrição não deve ser vazia")
                .HasMaxLengthIfNotNullOrEmpty(Valor, 3000, "Descricao", "A descrição deve ter ao máximo 3000 caracteres")
            );
        }

        public static Descricao CriarDescricaoVazia() => new Descricao();

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Valor;
        }
    }
}
