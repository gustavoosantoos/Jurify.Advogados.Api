using Jurify.Advogados.Api.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia
{
    public class JurifyContext : DbContext
    {
        public JurifyContext(DbContextOptions<JurifyContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; private set; }
        public DbSet<CasoJuridico> CasosJuridicos { get; private set; }
        public DbSet<EventoCasoJuridico> EventosCasoJuridico { get; private set; }
    }
}
