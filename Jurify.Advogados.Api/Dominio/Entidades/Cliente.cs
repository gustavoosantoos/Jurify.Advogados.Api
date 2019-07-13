using Jurify.Advogados.Api.Domain.Base;
using Jurify.Advogados.Api.Domain.Enums;
using Jurify.Advogados.Api.Domain.ObjetosDeValor;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Domain.Entidades
{
    public class Cliente : Entidade
    {
        public InformacoesPessoaisCliente InformacoesPessoais { get; private set; }
        public List<EnderecoCliente> Enderecos { get; private set; }

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
            Enderecos.Add(new EnderecoCliente(endereco, string.Empty, tipo));
        }

        public void RemoverEndereco(string endereco, TipoEndereco tipo)
        {
            Enderecos.Remove(new EnderecoCliente(endereco, string.Empty, tipo));
        }
    }
}
