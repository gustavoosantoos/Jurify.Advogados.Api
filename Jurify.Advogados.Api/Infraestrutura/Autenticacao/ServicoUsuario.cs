using Jurify.Advogados.Api.Infraestrutura.Autenticacao.Modelo;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao
{
    public class ServicoUsuarios
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public Usuario UsuarioAtual { get; private set; }
        public Escritorio EscritorioAtual { get; private set; }

        public ServicoUsuarios(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public void AtualizarUsuario(Usuario usuario, Escritorio escritorio)
        {
            if (UsuarioAtual != null || usuario == null || EscritorioAtual != null || escritorio == null)
                throw new ArgumentException("Não é possível atualizar o usuário atual");

            UsuarioAtual = usuario;
            EscritorioAtual = escritorio;
        }

        public async Task<Usuario> ObterInformacoesDeUsuario(Guid codigoUsuario)
        {
            var url = $"Account/UserInfo/{EscritorioAtual.Codigo}/{codigoUsuario}";
            var response = await _clientFactory.CreateClient("AUTENTICADOR_API").GetStringAsync(url);
            var dados = JsonConvert.DeserializeObject<ModeloAutenticador.Usuario>(response);

            return new Usuario(dados.Id, dados.PersonalInfo.FirstName, dados.PersonalInfo.LastName);
        }
    }
}
