using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Domain.ObjetosDeValor
{
    public class InformacoesPessoaisCliente : ObjetoDeValor
    {
        public Nome Nome { get; private set; }
        public DateTime? DataNascimento { get; private set; }

        public InformacoesPessoaisCliente(Nome nome, DateTime? dataNascimento)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Nome;
            yield return DataNascimento;
        }
    }
}
