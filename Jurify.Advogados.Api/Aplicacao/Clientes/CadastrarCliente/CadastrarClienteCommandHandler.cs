using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.CadastrarCliente
{
    public class CadastrarClienteCommandHandler : IRequestHandler<CadastrarClienteCommand, RespostaCasoDeUso>
    {
        private readonly JurifyContext _context;

        public CadastrarClienteCommandHandler(JurifyContext context)
        {
            _context = context;
        }

        public async Task<RespostaCasoDeUso> Handle(CadastrarClienteCommand request, CancellationToken cancellationToken)
        {
            var nome = new Nome(request.Nome, request.Sobrenome);
            var informacoesPessoais = new InformacoesPessoaisCliente(nome, request.DataNascimento);
            var enderecos = request.Enderecos.Select(e => new Endereco(
                e.Rua,
                e.Numero,
                e.Cidade,
                e.Estado,
                e.Pais,
                e.Cidade,
                e.Complemento,
                e.Observacoes,
                e.Tipo
            ));

            var cliente = new Cliente(informacoesPessoais, enderecos);

            await _context.Clientes.AddAsync(cliente, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return RespostaCasoDeUso.ComSucesso(cliente.Codigo);
        }
    }
}
