using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Atualizar
{
    public class AtualizarProcessoJuridicoCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid Codigo { get; set; }
        public Guid CodigoCliente { get; set; }
        public Guid? CodigoAdvogadoResponsavel { get; set; }
        public string NumeroProcesso { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EStatusProcessoJuridico Status { get; set; }
        public ETipoDePapelProcessoJuridico TipoDePapel { get; set; }

    }
}
