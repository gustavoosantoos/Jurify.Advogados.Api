using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloAgenda.Agenda.AdicionarCompromisso
{
    public class AdicionarCompromissoCommand : IRequest<RespostaCasoDeUso>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Final { get; set; }
        public Guid? CodigoCliente { get; set; }
    }
}
