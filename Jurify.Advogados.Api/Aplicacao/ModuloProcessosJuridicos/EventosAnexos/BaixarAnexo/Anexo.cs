using System.IO;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.EventosAnexos.BaixarAnexo
{
    public class Anexo
    {
        public Anexo(string nomeDoArquivo, Stream arquivo)
        {
            NomeDoArquivo = nomeDoArquivo;
            Arquivo = arquivo;
        }

        public string NomeDoArquivo { get; set; }
        public Stream Arquivo { get; set; }
    }
}
