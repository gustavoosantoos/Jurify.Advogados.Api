using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.CadastrarProcessoJuridico
{
    public class CadastrarProcessoJuridicoCommandHandler : BaseHandler, IRequestHandler<CadastrarProcessoJuridicoCommand, RespostaCasoDeUso>
    {
        public CadastrarProcessoJuridicoCommandHandler(JurifyContext context, ProvedorUsuarioAtual provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(CadastrarProcessoJuridicoCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
