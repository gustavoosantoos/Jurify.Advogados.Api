using Jurify.Advogados.Api.Dominio.Enums;
using System;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.ObterCliente
{
    public class EnderecoCliente
    {
        public Guid Codigo { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
        public string Observacoes { get; set; }
        public TipoEndereco Tipo { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }
    }
}
