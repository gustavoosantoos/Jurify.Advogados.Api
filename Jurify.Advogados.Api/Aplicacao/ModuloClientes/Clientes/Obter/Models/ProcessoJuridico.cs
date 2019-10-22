using System;
using Jurify.Advogados.Api.Dominio.Enums;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Obter.Models
{
    public class ProcessoJuridico
    {
        public Guid Codigo { get;  set; }

        public string Numero { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string JuizResponsavel { get; set; }
        public string UF { get; set; }
        public string Status { get; set; }
        public string TipoDePapel { get; set; }

        public Guid? CodigoAdvogadoResponsavel { get; set; }
        public Guid CodigoCliente { get; set; }
    }
}
