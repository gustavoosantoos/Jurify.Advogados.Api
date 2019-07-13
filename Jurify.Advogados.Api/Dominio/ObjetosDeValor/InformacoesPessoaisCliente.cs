using Jurify.Advogados.Api.Domain.Base;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Domain.ObjetosDeValor
{
    public class InformacoesPessoaisCliente : ObjetoDeValor
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime? DataNascimento { get; private set; }

        public InformacoesPessoaisCliente(string nome, string sobrenome, DateTime? dataNascimento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nome;
            yield return Sobrenome;
            yield return DataNascimento;
        }
    }
}
