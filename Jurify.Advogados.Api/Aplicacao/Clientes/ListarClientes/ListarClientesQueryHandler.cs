using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
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
        private readonly ProvedorUsuarioAtual _provedor;

        public ListarClientesQueryHandler(JurifyContext context, ProvedorUsuarioAtual provedor)
        {
            _context = context;
            _provedor = provedor;
        }

        public async Task<RespostaCasoDeUso> Handle(ListarClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _context
                .Clientes
                .Where(c => c.CodigoEscritorio == _provedor.Escritorio.Codigo && !c.Apagado)
                .Select(c => new ClientePreview()
                    {
                        Codigo = c.Codigo,
                        Nome = c.Nome.PrimeiroNome,
                        Sobrenome = c.Nome.Sobrenome,
                        DataNascimento = c.DataNascimento
                    })
                .ToListAsync();

            return RespostaCasoDeUso.ComSucesso(clientes);
        }
    }
}
