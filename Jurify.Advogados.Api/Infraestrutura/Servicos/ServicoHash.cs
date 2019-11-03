using Jurify.Advogados.Api.Dominio.Servicos;
using System.Security.Cryptography;
using System.Text;

namespace Jurify.Advogados.Api.Infraestrutura.Servicos
{
    public class ServicoHash : IServicoHash
    {
        public string GerarHash(string entrada)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(entrada));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
