using System.Collections.Generic;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Listar.Models
{
    public class ListagemClientes
    {
        public int ClientesAtivos { get; set; }
        public int ClientesComProcessoAtivo { get; set; }
        public IEnumerable<ClientePreview> Clientes { get; set; }
    }
}
