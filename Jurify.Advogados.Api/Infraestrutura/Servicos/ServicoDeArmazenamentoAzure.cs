using Jurify.Advogados.Api.Dominio.Servicos;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infraestrutura.Servicos
{
    public class ServicoDeArmazenamentoAzure : IServicoDeArmazenamento
    {
        private readonly CloudStorageAccount _account;
        private readonly CloudBlobClient _client;

        public ServicoDeArmazenamentoAzure(IConfiguration configuration)
        {
            _account = CloudStorageAccount.Parse(configuration.GetConnectionString("Storage"));
            _client = _account.CreateCloudBlobClient();
        }

        public async Task<string> SalvarArquivo(string nomeArquivo, Stream arquivo)
        {
            try
            {
                CloudBlobContainer container = _client.GetContainerReference("crm-files");
                CloudBlockBlob arquivoCloud = container.GetBlockBlobReference(nomeArquivo);
                await arquivoCloud.UploadFromStreamAsync(arquivo);
                return arquivoCloud.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                // TO-DO: Loggar erro
                // return null;
                throw;
            }
        }

        public async Task<Stream> ObterArquivo(string nomeArquivo)
        {
            CloudBlobContainer container = _client.GetContainerReference("crm-files");
            CloudBlockBlob arquivoCloud = container.GetBlockBlobReference(nomeArquivo);
            MemoryStream memStream = new MemoryStream();
            await arquivoCloud.DownloadToStreamAsync(memStream);

            return memStream;
        }
    }
}
