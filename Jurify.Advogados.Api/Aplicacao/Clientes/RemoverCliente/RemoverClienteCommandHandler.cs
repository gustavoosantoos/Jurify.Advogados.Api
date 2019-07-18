using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.RemoverCliente
{
    public class RemoverClienteCommandHandler : IRequestHandler<RemoverClienteCommand, RespostaCasoDeUso>
    {
        private readonly JurifyContext _context;
        private readonly ProvedorUsuarioAtual _provedor;

        public RemoverClienteCommandHandler(JurifyContext context, ProvedorUsuarioAtual provedor)
        {
            _context = context;
            _provedor = provedor;
        }

        public async Task<RespostaCasoDeUso> Handle(RemoverClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _context
                .Clientes
                .FirstOrDefaultAsync(c => c.Codigo == request.Codigo &&
                                          c.CodigoEscritorio == _provedor.Escritorio.Codigo &&
                                          !c.Apagado);

            if (cliente == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso();
        }
    }
}
