using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.RemoverMensagem
{
    public class RemoverMensagemCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverMensagemCommand(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
