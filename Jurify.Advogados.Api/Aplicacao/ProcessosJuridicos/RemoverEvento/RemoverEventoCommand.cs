using System;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.RemoverEvento
{
    public class RemoverEventoCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid CodigoProcesso { get; set; }
        public Guid CodigoEvento { get; set; }

        public RemoverEventoCommand(Guid codigoProcesso, Guid codigoEvento)
        {
            CodigoProcesso = codigoProcesso;
            CodigoEvento = codigoEvento;
        }
    }
}
