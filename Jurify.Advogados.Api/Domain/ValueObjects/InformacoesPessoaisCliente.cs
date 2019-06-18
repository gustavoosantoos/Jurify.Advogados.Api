using Jurify.Advogados.Api.Domain.Base;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Domain.ValueObjects
{
    public class InformacoesPessoaisCliente : ValueObject
    {
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime? DataNascimento { get; private set; }

        private InformacoesPessoaisCliente(string nome, string sobrenome, DateTime? dataNascimento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
        }

        public InformacoesPessoaisCliente ComNome(string nome) => new InformacoesPessoaisCliente(nome, Sobrenome, DataNascimento);
        public InformacoesPessoaisCliente ComSobrenome(string sobrenome) => new InformacoesPessoaisCliente(Nome, sobrenome, DataNascimento);
        public InformacoesPessoaisCliente ComDataNascimento(DateTime? dataNascimento) => new InformacoesPessoaisCliente(Nome, Sobrenome, dataNascimento);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nome;
            yield return Sobrenome;
            yield return DataNascimento;
        }
    }
}
