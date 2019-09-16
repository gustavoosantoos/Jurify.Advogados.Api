using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia
{
    public class UnitOfWork
    {
        public UnitOfWork(JurifyContext context)
        {
            Context = context;
        }

        public JurifyContext Context { get; }

        public void SalvarAlteracoes() => Context.SaveChanges();
        public async Task SalvarAlteracoesAsync() => await Context.SaveChangesAsync();
    }
}
