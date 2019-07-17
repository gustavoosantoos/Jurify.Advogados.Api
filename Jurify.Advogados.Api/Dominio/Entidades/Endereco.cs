using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Enums;
using System;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class Endereco : Entidade
    {
        protected Endereco()
        {
        }

        public Endereco(
            string rua,
            string numero,
            string cidade,
            string estado,
            string pais,
            string cep,
            string complemento,
            string observacoes,
            TipoEndereco tipo)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Cep = cep;
            Complemento = complemento;
            Observacoes = observacoes;
            Tipo = tipo;
        }

        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public string Cep { get; private set; }
        public string Complemento { get; private set; }
        public string Observacoes { get; private set; }
        public TipoEndereco Tipo { get; private set; }

        public Guid CodigoCliente { get; private set; }
    }
}
