using Jurify.Advogados.Api.Domain.Base;
using Jurify.Advogados.Api.Domain.Enums;
using Jurify.Advogados.Api.Domain.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Domain.Entidades
{
    public class Cliente : Entidade
    {
        private readonly ICollection<Endereco> _enderecos;

        public InformacoesPessoaisCliente InformacoesPessoais { get; private set; }
        public IReadOnlyCollection<Endereco> Enderecos => _enderecos.ToArray();

        public Cliente(InformacoesPessoaisCliente informacoesPessoais)
        {
            InformacoesPessoais = informacoesPessoais;
            _enderecos = new List<Endereco>();
        }

        public void AtualizarNome(string nome, string sobrenome)
        {
            InformacoesPessoais = new InformacoesPessoaisCliente(nome, sobrenome, InformacoesPessoais.DataNascimento);
        }

        public void AtualizarDataNascimento(DateTime? dataNascimento)
        {
            InformacoesPessoais = new InformacoesPessoaisCliente(InformacoesPessoais.Nome, InformacoesPessoais.Sobrenome, dataNascimento);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            _enderecos.Add(endereco);
        }

        public void RemoverEndereco(Endereco endereco)
        {
            _enderecos.Remove(endereco);
        }
    }
}
