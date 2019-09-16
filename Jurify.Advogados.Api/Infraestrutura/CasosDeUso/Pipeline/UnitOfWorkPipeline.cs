using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;

namespace Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Pipeline
{
    public class UnitOfWorkPipeline : IPipelineBehavior<IRequest, RespostaCasoDeUso>
    {
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkPipeline(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RespostaCasoDeUso> Handle(IRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<RespostaCasoDeUso> next)
        {
            var response = await next();
            await _unitOfWork.SalvarAlteracoesAsync();

            return response;
        }
    }
}
