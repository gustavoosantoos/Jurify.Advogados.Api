using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.RemoverMensagemPublica
{
    public class RemoverMensagemPublicaCommandHandler : BaseHandler, IRequestHandler<RemoverMensagemPublicaCommand, RespostaCasoDeUso>
    {
        public RemoverMensagemPublicaCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(RemoverMensagemPublicaCommand request, CancellationToken cancellationToken)
        {
            var mensagem = await Context
                .MensagensPublicas
                .FirstOrDefaultAsync(m => m.Codigo == request.Codigo &&
                                          m.Status != EStatusMensagemPublica.ConfirmadaPeloCliente &&
                                          m.Apagado == false);

            if (mensagem == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            Context.MensagensPublicas.Remove(mensagem);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(mensagem.Codigo);
        }
    }
}
