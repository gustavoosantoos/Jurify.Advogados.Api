using Jurify.Advogados.Api.Domain.Base;
using Jurify.Advogados.Api.Domain.Enums;
using Jurify.Advogados.Api.Domain.ValueObjects;
using System;

namespace Jurify.Advogados.Api.Domain.Entities
{
    public class Cliente : Entity
    {
        public InformacoesPessoaisCliente InformacoesPessoais { get; private set; }
        public EnderecosCliente Enderecos { get; private set; }

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
            Enderecos = new EnderecosCliente(Enderecos.Enderecos.Add((endereco, tipo)));
        }

        public void RemoverEndereco(string endereco, TipoEndereco tipo)
        {
            Enderecos = new EnderecosCliente(Enderecos.Enderecos.Remove((endereco, tipo)));
        }
    }
}
