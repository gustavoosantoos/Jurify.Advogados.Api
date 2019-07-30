using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;

namespace Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum
{
    public abstract class BaseHandler
    {
        public BaseHandler(JurifyContext context, ServicoUsuarios provedor)
        {
            Context = context;
            Provedor = provedor;
        }

        protected JurifyContext Context { get; }
        protected ServicoUsuarios Provedor { get; }
    }
}
