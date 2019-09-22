using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Dominio.Servicos
{
    public interface IServicoDeEmail
    {
        Task<bool> EnviarEmail(
            string remetente,
            string senha,
            string destinatario,
            string assunto,
            string conteudo);
    }
}
