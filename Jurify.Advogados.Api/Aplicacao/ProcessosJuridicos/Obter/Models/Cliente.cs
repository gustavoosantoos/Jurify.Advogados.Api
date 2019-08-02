using System;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Obter.Models
{
    public class Cliente
    {
        public Guid Codigo { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public int? Idade { get; set; }
    }
}
