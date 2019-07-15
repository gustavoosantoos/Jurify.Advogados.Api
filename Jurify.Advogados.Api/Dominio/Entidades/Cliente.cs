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
        private readonly ICollection<EnderecoCliente> _enderecos;

        public InformacoesPessoaisCliente InformacoesPessoais { get; private set; }
        public IReadOnlyCollection<EnderecoCliente> Enderecos => _enderecos.ToArray();

        public Cliente(InformacoesPessoaisCliente informacoesPessoais)
        {
            InformacoesPessoais = informacoesPessoais;
            _enderecos = new List<EnderecoCliente>();
        }

        public void AtualizarNome(string nome, string sobrenome)
        {
            InformacoesPessoais = new InformacoesPessoaisCliente(nome, sobrenome, InformacoesPessoais.DataNascimento);
        }

        public void AtualizarDataNascimento(DateTime? dataNascimento)
        {
            InformacoesPessoais = new InformacoesPessoaisCliente(InformacoesPessoais.Nome, InformacoesPessoais.Sobrenome, dataNascimento);
        }

        public void AdicionarEndereco(string endereco, TipoEndereco tipo)
        {
            _enderecos.Add(new EnderecoCliente(endereco, string.Empty, tipo));
        }

        public void RemoverEndereco(string endereco, TipoEndereco tipo)
        {
            _enderecos.Remove(new EnderecoCliente(endereco, string.Empty, tipo));
        }
    }
}
