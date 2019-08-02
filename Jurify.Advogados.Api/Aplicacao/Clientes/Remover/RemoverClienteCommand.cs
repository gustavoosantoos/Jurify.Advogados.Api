using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.Remover
{
    public class RemoverClienteCommand : IRequest<RespostaCasoDeUso>
    {
        public RemoverClienteCommand()
        {
        }

        public RemoverClienteCommand(Guid codigo)
        {
            Codigo = codigo;
        }

        public Guid Codigo { get; set; }
    }
}
