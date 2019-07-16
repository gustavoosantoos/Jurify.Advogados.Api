using Jurify.Advogados.Api.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia
{
    public class JurifyContext : DbContext
    {
        private readonly IConfiguration _configuracoes;

        public JurifyContext(IConfiguration configuracoes)
        {
            _configuracoes = configuracoes;
        }

        public JurifyContext(DbContextOptions<JurifyContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; private set; }
        public DbSet<CasoJuridico> CasosJuridicos { get; private set; }
        public DbSet<EventoCasoJuridico> EventosCasoJuridico { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuracoes.GetConnectionString("Default"));
        }
    }
}
