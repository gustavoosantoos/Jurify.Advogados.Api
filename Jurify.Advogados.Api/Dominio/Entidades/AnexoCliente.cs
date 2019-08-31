using Jurify.Advogados.Api.Dominio.Base;
using System;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class AnexoCliente : Entidade
    {
        public string NomeArquivo { get; private set; }
        public string Identificador { get; set; }
        public string Url { get; private set; }

        public Guid CodigoCliente { get; set; }

        protected AnexoCliente()
        {
        }

        public AnexoCliente(Guid codigoCliente, string nomeArquivo, string identificador,string url)
        {
            CodigoCliente = codigoCliente;
            NomeArquivo = nomeArquivo;
            Identificador = identificador;
            Url = url;

            Validar();
        }

        protected override void Validar()
        {

        }
    }
}
