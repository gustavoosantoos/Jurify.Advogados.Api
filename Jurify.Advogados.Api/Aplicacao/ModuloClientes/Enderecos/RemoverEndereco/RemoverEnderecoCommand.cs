using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Enderecos.RemoverEndereco
{
    public class RemoverEnderecoCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverEnderecoCommand()
        {
        }

        public RemoverEnderecoCommand(Guid codigoCliente, Guid codigoEndereco)
        {
            CodigoEndereco = codigoEndereco;
            CodigoCliente = codigoCliente;
        }

        public Guid CodigoEndereco { get; set; }
        public Guid CodigoCliente { get; set; }
    }
}
