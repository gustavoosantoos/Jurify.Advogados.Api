using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.AtualizarEvento
{
    public class AtualizarEventoCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid CodigoProcessoJuridico { get; set; }
        public Guid CodigoEvento { get; set; }
        public string Descricao { get; set; }
    }
}
