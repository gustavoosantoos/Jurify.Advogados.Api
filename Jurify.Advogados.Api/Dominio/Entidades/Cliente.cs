using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class Cliente : Entidade
    {
        private readonly List<Endereco> _enderecos;

        public Nome Nome { get; private set; }
        public RG RG { get; private set; }
        public CPF CPF { get; private set; }
        public DateTime? DataNascimento { get; private set; }

        public IReadOnlyCollection<Endereco> Enderecos => _enderecos;

        protected Cliente()
        {
            _enderecos = new List<Endereco>();
        }

        public Cliente(Nome nome, RG rg, CPF cpf, DateTime? dataNascimento, List<Endereco> enderecos)
        {
            Nome = nome;
            RG = rg;
            CPF = cpf;
            DataNascimento = dataNascimento;
            _enderecos = enderecos;

            Validar();
        }

        protected override void Validar()
        {
            AddNotifications(Nome, CPF, RG);

            AddNotifications(new Contract()
                .IsTrue(DataNascimento < DateTime.Now, "DataNascimento", "A data de nascimento deve estar no passado")
                .IsTrue(DataNascimento != default, "DataNascimento", "A data de nascimento deve possuir um valor")   
            );

            AddNotifications(_enderecos.ToArray());
        }
    }
}
