using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.ListarClientes
{
    public class ListarClientesQueryHandler : IRequestHandler<ListarClientesQuery, RespostaCasoDeUso>
    {
        private readonly JurifyContext _context;

        public ListarClientesQueryHandler(JurifyContext context)
        {
            _context = context;
        }

        public async Task<RespostaCasoDeUso> Handle(ListarClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _context
                .Clientes
                .Select(c => new ClientePreview()
                    {
                        Codigo = c.Codigo,
                        Nome = c.InformacoesPessoais.Nome.PrimeiroNome,
                        Sobrenome = c.InformacoesPessoais.Nome.Sobrenome,
                        DataNascimento = c.InformacoesPessoais.DataNascimento
                    })
                .ToListAsync();

            return RespostaCasoDeUso.ComSucesso(clientes);
        }
    }
}
