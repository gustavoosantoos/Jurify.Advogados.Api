using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.NotificarClienteSobreEvento
{
    public class NotificarClienteSobreEventoCommand : IRequest<RespostaCasoDeUso>
    {
        public NotificarClienteSobreEventoCommand(Guid codigoProcesso, Guid codigoEvento)
        {
            CodigoProcesso = codigoProcesso;
            CodigoEvento = codigoEvento;
        }

        public Guid CodigoProcesso { get; set; }
        public Guid CodigoEvento { get; set; }
    }
}
