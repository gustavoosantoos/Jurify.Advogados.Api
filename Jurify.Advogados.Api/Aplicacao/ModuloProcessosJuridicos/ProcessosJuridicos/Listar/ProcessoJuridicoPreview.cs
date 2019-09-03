using Jurify.Advogados.Api.Dominio.Enums;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Listar
{
    public class ProcessoJuridicoPreview
    {
        public Guid Codigo { get; set; }
        public Guid? CodigoAdvogadoResponsavel { get; set; }
        public string NomeAdvogadoResponsavel { get; set; }
        public string NumeroProcesso { get; set; }
        public string Titulo { get; set; }
        public EEstadoBrasileiro UF { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public EStatusProcessoJuridico Status { get; set; }
        public ETipoDePapelProcessoJuridico TipoDePapel { get; set; }
    }
}
