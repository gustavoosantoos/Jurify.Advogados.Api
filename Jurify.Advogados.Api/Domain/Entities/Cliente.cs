using Jurify.Advogados.Api.Domain.Base;
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
            InformacoesPessoais = InformacoesPessoais.ComNome(nome).ComSobrenome(sobrenome);
        }

        public void AtualizarDataNascimento(DateTime? dataNascimento)
        {
            InformacoesPessoais = InformacoesPessoais.ComDataNascimento(dataNascimento);
        }

        public 
    }
}
