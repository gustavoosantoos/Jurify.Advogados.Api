﻿using Flunt.Notifications;
using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Exceptions;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class MensagemPublica : Entidade
    {
        public string NomeCliente { get; private set; }
        public Email ContatoCliente { get; private set; }
        public CPF CpfCliente { get; private set; }
        public Descricao Mensagem { get; private set; }
        public bool EmAnalise { get; private set; }
        public string Token { get; private set; }

        protected MensagemPublica()
        {

        }

        public MensagemPublica(Guid codigoEscritorio,
                               string nomeCliente,
                               Email contatoCliente,
                               CPF cpfCliente,
                               Descricao mensagem)
        {
            CodigoEscritorio = codigoEscritorio;
            NomeCliente = nomeCliente;
            ContatoCliente = contatoCliente;
            CpfCliente = cpfCliente;
            Mensagem = mensagem;

            Validar();
        }

        public void AssociarEscritorio(Guid codigoEscritorio, string tokenReativacao)
        {
            CodigoEscritorio = codigoEscritorio;
            EmAnalise = true;
            Token = tokenReativacao;
        }

        public void ReativarMensagem()
        {
            CodigoEscritorio = Guid.Empty;
            EmAnalise = false;
            Token = null;
        }

        public void EncerrarProcesso()
        {
            Validar();
            EmAnalise = false;
            Apagado = true;
        }

        protected override void Validar()
        {
            AddNotifications(ContatoCliente, CpfCliente, Mensagem);
        }
    }
}
