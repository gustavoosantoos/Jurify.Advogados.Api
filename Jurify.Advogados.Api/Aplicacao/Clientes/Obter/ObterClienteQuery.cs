using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.Obter
{
    public class ObterClienteQuery : IRequest<RespostaCasoDeUso>
    {
        public ObterClienteQuery(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
