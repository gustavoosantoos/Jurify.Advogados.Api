using Jurify.Advogados.Api.Dominio.Base;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class InformacoesPessoaisCliente : ObjetoDeValor
    {
        public Nome Nome { get; private set; }
        public DateTime? DataNascimento { get; private set; }

        protected InformacoesPessoaisCliente()
        {

        }

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
