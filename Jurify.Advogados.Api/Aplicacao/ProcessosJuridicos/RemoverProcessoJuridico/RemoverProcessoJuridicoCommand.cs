using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.RemoverProcessoJuridico
{
    public class RemoverProcessoJuridicoCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverProcessoJuridicoCommand(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
