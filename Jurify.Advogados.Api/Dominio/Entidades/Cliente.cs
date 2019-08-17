using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Exceptions;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class Cliente : Entidade
    {
        private readonly List<Endereco> _enderecos;
        private readonly List<ProcessoJuridico> _processos;

        public Nome Nome { get; private set; }
        public RG RG { get; private set; }
        public CPF CPF { get; private set; }
        public DataNascimento DataNascimento { get; private set; }
        public Email Email { get; private set; }

        public IReadOnlyCollection<Endereco> Enderecos => _enderecos;
        public IReadOnlyCollection<ProcessoJuridico> Processos => _processos;

        protected Cliente()
        {
            _enderecos = new List<Endereco>();
            _processos = new List<ProcessoJuridico>();
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
            AddNotifications(endereco);
            _enderecos.Add(endereco);
        }

        public void AtualizarNome(Nome nome)
        {
            AddNotifications(nome);
            Nome = nome;
        }

        public void AtualizarNascimento(DataNascimento dataNascimento)
        {
            AddNotifications(dataNascimento);
            DataNascimento = dataNascimento;
        }

        public void AtualizarRG(RG rg)
        {
            AddNotifications(rg);
            RG = rg;
        }

        public void AtualizarCPF(CPF cpf)
        {
            AddNotifications(cpf);
            CPF = cpf;
        }

        public void AtualizarEmail(Email email)
        {
            AddNotifications(email);
            Email = email;
        }

        protected override void Validar()
        {
            AddNotifications(Nome, CPF, RG, DataNascimento, Email);
            AddNotifications(_enderecos.ToArray());
        }
    }
}
