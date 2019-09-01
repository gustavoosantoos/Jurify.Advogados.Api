using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloCadastros.EstadosBrasileiros.Listar
{
    public class ListarEstadosBrasileirosQueryHandler : BaseHandler, IRequestHandler<ListarEstadosBrasileirosQuery, RespostaCasoDeUso>
    {
        public ListarEstadosBrasileirosQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(ListarEstadosBrasileirosQuery request, CancellationToken cancellationToken)
        {
            var estados = EEstadoBrasileiro.ObterTodos().Select(e => new EstadoBrasileiro
            {
                Codigo = e.Codigo,
                Nome = e.Nome,
                UF = e.UF
            });

            return Task.FromResult(RespostaCasoDeUso.ComSucesso(estados));
        }
    }
}
