using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.EventosAnexos.RemoverAnexo
{
    public class RemoverAnexoCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverAnexoCommand(Guid codigoProcesso, Guid codigoEvento, Guid codigoAnexo)
        {
            CodigoProcesso = codigoProcesso;
            CodigoEvento = codigoEvento;
            CodigoAnexo = codigoAnexo;
        }

        public Guid CodigoProcesso { get; set; }
        public Guid CodigoEvento { get; set; }
        public Guid CodigoAnexo { get; set; }
    }
}
