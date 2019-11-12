using Jurify.Advogados.Api.Dominio.Servicos;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.MarcarInteresse
{
    public class MarcarInteresseCommandHandler : BaseHandler, IRequestHandler<MarcarInteresseCommand, RespostaCasoDeUso>
    {
        private readonly IServicoHash _servicoHash;
        private readonly IServicoDeEmail _servicoDeEmail;

        public MarcarInteresseCommandHandler(JurifyContext context,
                                             ServicoUsuarios provedor,
                                             IServicoHash servicoHash,
                                             IServicoDeEmail servicoDeEmail) : base(context, provedor)
        {
            _servicoHash = servicoHash;
            _servicoDeEmail = servicoDeEmail;
        }

        public async Task<RespostaCasoDeUso> Handle(MarcarInteresseCommand request, CancellationToken cancellationToken)
        {
            var mensagem = await Context
                .MensagensPublicas
                .FirstOrDefaultAsync(m => m.Codigo == request.CodigoMensagem &&
                                          m.EmAnalise == false &&
                                          m.Apagado == false);

            if (mensagem == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            string token = _servicoHash.GerarHash(Guid.NewGuid().ToString());
            mensagem.AssociarEscritorio(ServicoUsuarios.EscritorioAtual.Codigo, token);

            await Context.SaveChangesAsync();
            NoticicarCliente();

            return RespostaCasoDeUso.ComSucesso(mensagem.Codigo);
        }

        private void NoticicarCliente()
        {
            // TO-DO: Enviar e-mail para o cliente notificando o interesse do escritório
            //_servicoDeEmail.EnviarEmail();
        }
    }
}
