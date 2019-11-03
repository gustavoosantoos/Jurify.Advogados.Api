using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ListarMensagensPublicas
{
    public class ListarMensagensPublicasQueryHandler : BaseHandler, IRequestHandler<ListarMensagensPublicasQuery, RespostaCasoDeUso>
    {
        public ListarMensagensPublicasQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(ListarMensagensPublicasQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
