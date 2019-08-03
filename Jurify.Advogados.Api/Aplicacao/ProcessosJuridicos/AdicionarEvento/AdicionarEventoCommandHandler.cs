using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.AdicionarEvento
{
    public class AdicionarEventoCommandHandler : BaseHandler, IRequestHandler<AdicionarEventoCommand, RespostaCasoDeUso>
    {
        public AdicionarEventoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(AdicionarEventoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
