using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Remover
{
    public class RemoverProcessoJuridicoCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverProcessoJuridicoCommand(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
