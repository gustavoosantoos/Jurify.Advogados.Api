using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.AceitarEscritorio
{
    public class AceitarEscritorioCommand : IRequest<RespostaCasoDeUso>
    {
        public AceitarEscritorioCommand(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
