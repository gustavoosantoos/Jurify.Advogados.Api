using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.AtualizarEndereco
{
    public class AtualizarEnderecoCommandHandler : BaseHandler, IRequestHandler<AtualizarEnderecoCommand, RespostaCasoDeUso>
    {
        public AtualizarEnderecoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(AtualizarEnderecoCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
