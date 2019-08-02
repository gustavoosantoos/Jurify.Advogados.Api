using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Obter
{
    public class ObterProcessoJuridicoQuery : IRequest<RespostaCasoDeUso>
    {
        public ObterProcessoJuridicoQuery(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
