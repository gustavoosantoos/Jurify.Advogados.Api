using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.CadastrarCliente
{
    public class CadastrarClienteCommandHandler : IRequestHandler<CadastrarClienteCommand, RespostaCasoDeUso>
    {
        public Task<RespostaCasoDeUso> Handle(CadastrarClienteCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(RespostaCasoDeUso.ComSucesso());
        }
    }
}
