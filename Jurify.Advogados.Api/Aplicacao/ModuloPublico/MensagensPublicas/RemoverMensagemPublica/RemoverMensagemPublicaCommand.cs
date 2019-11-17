using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.RemoverMensagemPublica
{
    public class RemoverMensagemPublicaCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverMensagemPublicaCommand(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
