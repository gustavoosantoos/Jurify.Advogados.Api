using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.MarcarInteresse
{
    public class MarcarInteresseCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid CodigoMensagem { get; set; }
    }
}
