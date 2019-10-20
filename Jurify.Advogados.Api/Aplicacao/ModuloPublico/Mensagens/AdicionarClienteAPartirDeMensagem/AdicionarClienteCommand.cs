using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.AdicionarClienteAPartirDeMensagem
{
    public class AdicionarClienteCommand : IRequest<RespostaCasoDeUso>
    {
        public AdicionarClienteCommand(Guid codigoMensagem)
        {
            CodigoMensagem = codigoMensagem;
        }

        public Guid CodigoMensagem { get; set; }
    }
}
