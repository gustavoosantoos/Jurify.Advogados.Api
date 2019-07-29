 using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Exceptions;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class Cliente : Entidade
    {
        private readonly List<Endereco> _enderecos;

        public Nome Nome { get; private set; }
        public RG RG { get; private set; }
        public CPF CPF { get; private set; }
        public DataNascimento DataNascimento { get; private set; }
        public Email Email { get; private set; }

        public IReadOnlyCollection<Endereco> Enderecos => _enderecos;

        protected Cliente()
        {
            _enderecos = new List<Endereco>();
        }

        public Cliente(Nome nome, RG rg, CPF cpf, DataNascimento dataNascimento, Email email, List<Endereco> enderecos)
        {
            Nome = nome;
            RG = rg;
            CPF = cpf;
            DataNascimento = dataNascimento;
            Email = email;
            _enderecos = enderecos;

            Validar();
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            if (Invalid)
                throw new DomainException(this);

            if (endereco.Invalid)
                throw new DomainException(endereco);

            _enderecos.Add(endereco);
        }

        protected override void Validar()
        {
            AddNotifications(Nome, CPF, RG, DataNascimento, Email);
            AddNotifications(_enderecos.ToArray());
        }
    }
}
