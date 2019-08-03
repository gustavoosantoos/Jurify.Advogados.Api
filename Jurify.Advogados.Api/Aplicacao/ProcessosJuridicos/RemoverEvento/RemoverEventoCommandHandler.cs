using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.RemoverEvento
{
    public class RemoverEventoCommandHandler : BaseHandler, IRequestHandler<RemoverEventoCommand, RespostaCasoDeUso>
    {
        public RemoverEventoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(RemoverEventoCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
