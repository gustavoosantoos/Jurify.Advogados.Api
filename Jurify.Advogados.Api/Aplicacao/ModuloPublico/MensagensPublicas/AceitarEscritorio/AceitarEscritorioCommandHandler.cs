using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.AceitarEscritorio
{
    public class AceitarEscritorioCommandHandler : BaseHandler, IRequestHandler<AceitarEscritorioCommand, RespostaCasoDeUso>
    {
        public AceitarEscritorioCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AceitarEscritorioCommand request, CancellationToken cancellationToken)
        {
            var mensagem = await Context
                .MensagensPublicas
                .FirstOrDefaultAsync(m => m.Codigo == request.Codigo &&
                                          m.Status == EStatusMensagemPublica.EscritorioInteressado &&
                                          m.Apagado == false);

            if (mensagem == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            mensagem.ConfirmarEscritorio();
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(mensagem.Codigo);
        }
    }
}
