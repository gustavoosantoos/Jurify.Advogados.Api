using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.EventosAnexos.BaixarAnexo
{
    public class BaixarAnexoQuery : IRequest<RespostaCasoDeUso>
    {
        public BaixarAnexoQuery(Guid codigoProcesso, Guid codigoEvento, Guid codigoAnexo)
        {
            CodigoProcesso = codigoProcesso;
            CodigoAnexo = codigoAnexo;
            CodigoEvento = codigoEvento;
        }

        public Guid CodigoProcesso { get; set; }
        public Guid CodigoAnexo { get; set; }
        public Guid CodigoEvento { get; set; }
    }
}
