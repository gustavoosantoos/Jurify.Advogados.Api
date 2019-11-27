using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Dominio.Servicos;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.MarcarInteresse
{
    public class MarcarInteresseCommandHandler : BaseHandler, IRequestHandler<MarcarInteresseCommand, RespostaCasoDeUso>
    {
        private readonly IServicoDeEmail _servicoDeEmail;
        private readonly IConfiguration _configuration;

        public MarcarInteresseCommandHandler(JurifyContext context,
                                             ServicoUsuarios provedor,
                                             IServicoDeEmail servicoDeEmail,
                                             IConfiguration configuration) : base(context, provedor)
        {
            _servicoDeEmail = servicoDeEmail;
            _configuration = configuration;
        }

        public async Task<RespostaCasoDeUso> Handle(MarcarInteresseCommand request, CancellationToken cancellationToken)
        {
            var mensagem = await Context
                .MensagensPublicas
                .FirstOrDefaultAsync(m => m.Codigo == request.Codigo &&
                                          m.Status == EStatusMensagemPublica.Publica &&
                                          m.CodigoEscritorio == Guid.Empty &&
                                          m.Apagado == false);

            if (mensagem == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            mensagem.AssociarEscritorio(ServicoUsuarios.EscritorioAtual.Codigo);

            await Context.SaveChangesAsync();
            NotificarCliente(mensagem);

            return RespostaCasoDeUso.ComSucesso(mensagem.Codigo);
        }

        private void NotificarCliente(MensagemPublica mensagem)
        {
            _servicoDeEmail.EnviarEmail(
                _configuration["Email:Remetente"],
                _configuration["Email:Senha"],
                mensagem.ContatoCliente.Endereco,
                ConstruirAssuntoEmail(),
                ConstruirCorpoEmail(mensagem)
            );
        }

        private string ConstruirAssuntoEmail()
        {
            return "Um escritório se interessou no seu caso.";
        }

        private string ConstruirCorpoEmail(MensagemPublica mensagem)
        {
            return $@"
<h3>O escritório {ServicoUsuarios.EscritorioAtual.Nome} registrou interesse no seu caso</h3>
A partir de agora, seu caso não está mais visível publicamente, aguarde o contato do escritório acima, ou:
<br/><br/>
<a href='https://jurify.azurewebsites.net/acoes-mensagens/reativar-mensagem/{mensagem.Codigo}'>Reative seu caso publicamente</a>,<br/>
<a href='https://jurify.azurewebsites.net/acoes-mensagens/aceitar-advogado/{mensagem.Codigo}'>Aceite o escritório acima como o responsável pelo seu caso</a> ou<br/>
<a href='https://jurify.azurewebsites.net/acoes-mensagens/remover-mensagem/{mensagem.Codigo}'>Remova definitivamente seu caso</a><br/><br/>
<strong>Jurify.</strong>";
        }
    }
}
