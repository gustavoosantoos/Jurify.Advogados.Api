﻿using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Exceptions;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class Cliente : Entidade
    {
        private readonly List<Endereco> _enderecos;
        private readonly List<ProcessoJuridico> _processos;
        private readonly List<AnexoCliente> _anexos;

        public Nome Nome { get; private set; }
        public RG RG { get; private set; }
        public CPF CPF { get; private set; }
        public DataNascimento DataNascimento { get; private set; }
        public Email Email { get; private set; }

        public IReadOnlyCollection<Endereco> Enderecos => _enderecos.Where(e => !e.Apagado).ToList();
        public IReadOnlyCollection<ProcessoJuridico> Processos => _processos.Where(p => !p.Apagado).ToList();
        public IReadOnlyCollection<AnexoCliente> Anexos => _anexos.Where(a => !a.Apagado).ToList();

        protected Cliente()
        {
            _enderecos = new List<Endereco>();
            _processos = new List<ProcessoJuridico>();
            _anexos = new List<AnexoCliente>();
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

        public void AdicionarAnexo(AnexoCliente novoAnexo)
        {
            AddNotifications(novoAnexo);
            _anexos.Add(novoAnexo);
        } 

        protected override void Validar()
        {
            AddNotifications(Nome, CPF, RG, DataNascimento, Email);
            AddNotifications(_enderecos.ToArray());
        }
    }
}
