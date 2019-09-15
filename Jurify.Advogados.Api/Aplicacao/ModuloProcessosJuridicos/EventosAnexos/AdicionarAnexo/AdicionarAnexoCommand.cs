using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;
using System.IO;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.EventosAnexos.AdicionarAnexo
{
    public class AdicionarAnexoCommand : IRequest<RespostaCasoDeUso>
    {
        public AdicionarAnexoCommand(Guid codigoProcessoJuridico, Guid codigoEvento, string nomeArquivo, Stream arquivo)
        {
            CodigoProcessoJuridico = codigoProcessoJuridico;
            CodigoEvento = codigoEvento;
            NomeArquivo = nomeArquivo;
            Arquivo = arquivo;
        }

        public Guid CodigoProcessoJuridico { get; set; }
        public Guid CodigoEvento { get; set; }
        public string NomeArquivo { get; set; }
        public Stream Arquivo { get; set; }
    }
}
