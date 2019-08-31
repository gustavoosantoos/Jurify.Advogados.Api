using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;
using System.IO;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.AdicionarAnexo
{
    public class AdicionarAnexoCommand : IRequest<RespostaCasoDeUso>
    {
        public AdicionarAnexoCommand(Guid codigoCliente, string nomeArquivo, Stream arquivo)
        {
            CodigoCliente = codigoCliente;
            NomeArquivo = nomeArquivo;
            Arquivo = arquivo;
        }

        public Guid CodigoCliente { get; set; }
        public string NomeArquivo { get; set; }
        public Stream Arquivo { get; set; }
    }
}
