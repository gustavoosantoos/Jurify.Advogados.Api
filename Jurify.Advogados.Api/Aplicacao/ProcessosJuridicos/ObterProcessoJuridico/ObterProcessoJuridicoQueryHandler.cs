using Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.ObterProcessoJuridico.Models;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.ObterProcessoJuridico
{
    public class ObterProcessoJuridicoQueryHandler : BaseHandler, IRequestHandler<ObterProcessoJuridicoQuery, RespostaCasoDeUso>
    {
        public ObterProcessoJuridicoQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ObterProcessoJuridicoQuery request, CancellationToken cancellationToken)
        {
            var processo = await Context.ProcessosJuridicos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.Codigo == request.Codigo &&
                                     p.CodigoEscritorio == Provedor.EscritorioAtual.Codigo &&
                                     !p.Apagado);

            if (processo == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            return RespostaCasoDeUso.ComSucesso(await ProcessoJuridico.FromEntity(processo, Provedor));
        }
    }
}
