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

        public RemoverEnderecoCommand(Guid codigo, Guid codigoCliente)
        {
            Codigo = codigo;
            CodigoCliente = codigoCliente;
        }

        public Guid Codigo { get; set; }
        public Guid CodigoCliente { get; set; }
    }
}
