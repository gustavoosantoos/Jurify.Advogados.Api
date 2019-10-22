using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.RemoverMensagem
{
    public class RemoverMensagemCommandHandler : BaseHandler, IRequestHandler<RemoverMensagemCommand, RespostaCasoDeUso>
    {
        public RemoverMensagemCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(RemoverMensagemCommand request, CancellationToken cancellationToken)
        {
            var mensagem = await Context
                .MensagensRecebidas
                .FirstOrDefaultAsync(m => m.Codigo == request.Codigo &&
                                          m.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !m.Apagado);

            if (mensagem == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            Context.MensagensRecebidas.Remove(mensagem);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso();
        }
    }
}
