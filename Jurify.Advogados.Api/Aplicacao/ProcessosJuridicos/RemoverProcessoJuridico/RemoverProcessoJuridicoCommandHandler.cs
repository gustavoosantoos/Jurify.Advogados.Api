using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.RemoverProcessoJuridico
{
    public class RemoverProcessoJuridicoCommandHandler : BaseHandler, IRequestHandler<RemoverProcessoJuridicoCommand, RespostaCasoDeUso>
    {
        public RemoverProcessoJuridicoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(RemoverProcessoJuridicoCommand request, CancellationToken cancellationToken)
        {
            var processo = await Context.ProcessosJuridicos
               .FirstOrDefaultAsync(c => c.Codigo == request.Codigo &&
                                         c.CodigoEscritorio == Provedor.EscritorioAtual.Codigo &&
                                         !c.Apagado);

            if (processo == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            Context.ProcessosJuridicos.Remove(processo);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso();
        }
    }
}
