using System.IO;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Dominio.Servicos
{
    public interface IServicoDeArmazenamento
    {
        Task<string> SalvarArquivo(string nomeArquivo, Stream arquivo);
        Task<Stream> ObterArquivo(string nomeArquivo);
        Task<bool> RemoverArquivo(string nomeArquivo);
    }
}
