using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ReativarMensagemPublica
{
    public class ReativarMensagemPublicaCommand : IRequest<RespostaCasoDeUso>
    {
        public ReativarMensagemPublicaCommand(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
