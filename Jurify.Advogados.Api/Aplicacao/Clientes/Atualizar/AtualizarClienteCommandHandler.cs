using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.Atualizar
{
    public class AtualizarClienteCommandHandler : BaseHandler, IRequestHandler<AtualizarClienteCommand, RespostaCasoDeUso>
    {
        public AtualizarClienteCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
