using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.BaixarAnexo
{
    public class BaixarAnexoQuery : IRequest<RespostaCasoDeUso>
    {
        public BaixarAnexoQuery(Guid codigoCliente, Guid codigoAnexo)
        {
            CodigoAnexo = codigoAnexo;
            CodigoCliente = codigoCliente;
        }

        public Guid CodigoAnexo { get; set; }
        public Guid CodigoCliente { get; set; }
    }
}
