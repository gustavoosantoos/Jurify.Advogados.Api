using System;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Obter.Models
{
    public class Anexo
    {
        public Guid Codigo { get; set; }
        public string NomeArquivo { get; set; }
        public string Url { get; set; }
    }
}
