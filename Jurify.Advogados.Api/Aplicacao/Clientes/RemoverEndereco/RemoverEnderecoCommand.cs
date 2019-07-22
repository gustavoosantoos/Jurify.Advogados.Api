using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.RemoverEndereco
{
    public class RemoverEnderecoCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverEnderecoCommand()
        {
        }

        public RemoverEnderecoCommand(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
