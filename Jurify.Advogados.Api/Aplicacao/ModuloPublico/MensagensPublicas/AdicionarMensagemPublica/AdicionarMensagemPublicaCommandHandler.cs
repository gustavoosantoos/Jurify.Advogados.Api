using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.AdicionarMensagemPublica
{
    public class AdicionarMensagemPublicaCommandHandler : BaseHandler, IRequestHandler<AdicionarMensagemPublicaCommand, RespostaCasoDeUso>
    {
        public AdicionarMensagemPublicaCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AdicionarMensagemPublicaCommand request, CancellationToken cancellationToken)
        {
            var mensagem = request.AsEntity();

            if (mensagem.Invalid)
            {
                return RespostaCasoDeUso.ComFalha(mensagem.Notifications);
            }

            await Context.MensagensPublicas.AddAsync(mensagem);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(mensagem.Codigo);
        }
    }
}
