using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.CadastrarCliente
{
    public class CadastrarClienteCommandHandler : BaseHandler, IRequestHandler<CadastrarClienteCommand, RespostaCasoDeUso>
    {
        public CadastrarClienteCommandHandler(JurifyContext context, ProvedorUsuarioAtual provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(CadastrarClienteCommand request, CancellationToken cancellationToken)
        {
            var nome = new Nome(request.Nome, request.Sobrenome);
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

            var cliente = new Cliente(nome, request.DataNascimento, enderecos.ToList());

            await Context.Clientes.AddAsync(cliente, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);

            return RespostaCasoDeUso.ComSucesso(cliente.Codigo);
        }
    }
}
