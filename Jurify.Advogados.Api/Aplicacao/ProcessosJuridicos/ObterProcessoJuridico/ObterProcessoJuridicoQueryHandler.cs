using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.ObterProcessoJuridico
{
    public class ObterProcessoJuridicoQueryHandler : BaseHandler, IRequestHandler<ObterProcessoJuridicoQuery, RespostaCasoDeUso>
    {
        public ObterProcessoJuridicoQueryHandler(JurifyContext context, ProvedorUsuarioAtual provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(ObterProcessoJuridicoQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult<RespostaCasoDeUso>(null);
        }
    }
}
