using Jurify.Advogados.Api.Aplicacao.Clientes.Listar.Models;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.Listar
{
    public class ListarClientesQueryHandler : BaseHandler, IRequestHandler<ListarClientesQuery, RespostaCasoDeUso>
    {
        public ListarClientesQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ListarClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await Context.Clientes
                .Where(c => c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo && !c.Apagado)
                .Select(c => new ClientePreview()
                {
                    Codigo = c.Codigo,
                    Nome = c.Nome.PrimeiroNome,
                    Sobrenome = c.Nome.Sobrenome,
                    DataNascimento = c.DataNascimento.Data,
                    Email = c.Email.Endereco
                })
                .ToListAsync();

            return RespostaCasoDeUso.ComSucesso(clientes);
        }
    }
}
