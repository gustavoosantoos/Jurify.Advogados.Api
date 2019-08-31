using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.RemoverAnexo
{
    public class RemoverAnexoCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverAnexoCommand(Guid codigoCliente, Guid codigoAnexo)
        {
            CodigoCliente = codigoCliente;
            CodigoAnexo = codigoAnexo;
        }

        public Guid CodigoCliente { get; set; }
        public Guid CodigoAnexo { get; set; }
    }
}
