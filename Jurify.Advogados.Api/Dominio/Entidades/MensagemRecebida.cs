using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class MensagemRecebida : Entidade
    {
        public string NomeCliente { get; private set; }
        public string ContatoCliente { get; private set; }
        public CPF CpfCliente { get; private set; }
        public Descricao Mensagem { get; private set; }

        protected MensagemRecebida()
        {

        }

        public MensagemRecebida(Guid codigoEscritorio,
                                string nomeCliente,
                                string contatoCliente,
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
        
        protected override void Validar()
        {
            AddNotifications(CpfCliente, Mensagem);
        }
    }
}
