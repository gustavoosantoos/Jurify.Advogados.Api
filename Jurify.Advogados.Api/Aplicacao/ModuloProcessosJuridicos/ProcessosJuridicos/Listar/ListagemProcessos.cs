using System.Collections.Generic;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Listar
{
    public class ListagemProcessos
    {
        public int QuantidadeProcessos { get; set; }
        public int QuantidadeProcessosAtivos { get; set; }
        public IEnumerable<ProcessoJuridicoPreview> Processos { get; set; }
    }
}
