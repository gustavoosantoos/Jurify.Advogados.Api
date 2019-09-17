using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Exceptions;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia
{
    public class JurifyContext : DbContext
    {
        private readonly IConfiguration _configuracoes;
        private readonly ServicoUsuarios _provedor;
        private readonly ILoggerFactory _loggerFactory;

        public JurifyContext(ServicoUsuarios provedorUsuario, ILoggerFactory loggerFactory, IConfiguration configuracoes)
        {
            _configuracoes = configuracoes;
            _provedor = provedorUsuario;
            _loggerFactory = loggerFactory;
        }

        public DbSet<Cliente> Clientes { get; private set; }
        public DbSet<Endereco> Enderecos { get; private set; }
        public DbSet<AnexoCliente> AnexosClientes { get; private set; }
        public DbSet<ProcessoJuridico> ProcessosJuridicos { get; private set; }
        public DbSet<EventoProcessoJuridico> EventosProcessoJuridico { get; private set; }
        public DbSet<AnexoEventoProcessoJuridico> AnexosEventosProcessoJuridico { get; private set; }
        public DbSet<CompromissoAgenda> CompromissosAgenda { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.UseNpgsql(_configuracoes.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ManutenirEstadoDasEntradas();
            return base.SaveChanges();
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ManutenirEstadoDasEntradas();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ManutenirEstadoDasEntradas()
        {
            var codigoUsuarioAtual = _provedor.UsuarioAtual.Codigo;
            var codigoEscritorioAtual = _provedor.EscritorioAtual.Codigo;

            foreach (var entrada in ChangeTracker.Entries<Entidade>())
            {
                var now = DateTime.UtcNow;

                if (entrada.Entity.Invalid)
                {
                    throw new DomainException(entrada.Entity);
                }

                if (entrada.State == EntityState.Unchanged)
                {
                    entrada.State = EntityState.Modified;
                }

                switch (entrada.State)
                {
                    case EntityState.Added:
                        entrada.CurrentValues[nameof(Entidade.DataCriacao)] = now;
                        entrada.CurrentValues[nameof(Entidade.CodigoEscritorio)] = codigoEscritorioAtual;
                        entrada.CurrentValues[nameof(Entidade.CodigoUsuarioUltimaAlteracao)] = codigoUsuarioAtual;
                        entrada.CurrentValues[nameof(Entidade.DataUltimaAlteracao)] = now;
                        break;
                    case EntityState.Modified:
                        entrada.CurrentValues[nameof(Entidade.CodigoUsuarioUltimaAlteracao)] = codigoUsuarioAtual;
                        entrada.CurrentValues[nameof(Entidade.DataUltimaAlteracao)] = now;
                        break;
                    case EntityState.Deleted:
                        entrada.CurrentValues[nameof(Entidade.CodigoUsuarioUltimaAlteracao)] = codigoUsuarioAtual;
                        entrada.CurrentValues[nameof(Entidade.DataUltimaAlteracao)] = now;
                        entrada.CurrentValues[nameof(Entidade.Apagado)] = true;
                        entrada.State = EntityState.Modified;
                        break;
                }
            }
        }
    }
}
