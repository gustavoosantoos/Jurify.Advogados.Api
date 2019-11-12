using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Enums;
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
        public EStatusMensagemPublica Status { get; private set; }
        

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
            Status = EStatusMensagemPublica.Publica;

            Validar();
        }

        public void AssociarEscritorio(Guid codigoEscritorio)
        {
            if (Status != EStatusMensagemPublica.Publica)
            {
                AddNotification("Status", "Não é possível associar um escritório para uma mensagem não pública");
                throw new DomainException(this);
            }

            CodigoEscritorio = codigoEscritorio;
            Status = EStatusMensagemPublica.EscritorioInteressado;
        }

        public void ConfirmarEscritorio()
        {
            if (Status != EStatusMensagemPublica.EscritorioInteressado)
            {
                AddNotification("Status", "Não é possível confirmar o vínculo em uma mensagem fora de análise");
                throw new DomainException(this);
            }

            Status = EStatusMensagemPublica.ConfirmadaPeloCliente;
        }

        public void RejeitarEscritorio()
        {
            if (Status != EStatusMensagemPublica.EscritorioInteressado)
            {
                AddNotification("Status", "Não é possível rejeitar o vínculo em uma mensagem fora de análise");
                throw new DomainException(this);
            }

            CodigoEscritorio = Guid.Empty;
            Status = EStatusMensagemPublica.Publica;
        }


        protected override void Validar()
        {
            AddNotifications(ContatoCliente, CpfCliente, Mensagem);
        }
    }
}
