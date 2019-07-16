using System;
using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Domain.Entidades;
using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia
{
    public class JurifyContext : DbContext
    {
        private readonly IConfiguration _configuracoes;
        private readonly Guid _codigoUsuarioAtual;
        private readonly Guid _codigoEscritorioAtual;

        public JurifyContext(ProvedorUsuarioAtual provedorUsuario, IConfiguration configuracoes)
        {
            _configuracoes = configuracoes;
            _codigoUsuarioAtual = provedorUsuario.Usuario.Codigo;
            _codigoEscritorioAtual = provedorUsuario.Usuario.Escritorio.Codigo;
        }

        public JurifyContext(ProvedorUsuarioAtual provedorUsuario, DbContextOptions<JurifyContext> options) : base(options)
        {
            _codigoUsuarioAtual = provedorUsuario.Usuario.Codigo;
            _codigoEscritorioAtual = provedorUsuario.Usuario.Escritorio.Codigo;
        }

        public DbSet<Cliente> Clientes { get; private set; }
        public DbSet<CasoJuridico> CasosJuridicos { get; private set; }
        public DbSet<EventoCasoJuridico> EventosCasoJuridico { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuracoes.GetConnectionString("Default"));
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
            foreach (var entrada in ChangeTracker.Entries())
            {
                var now = DateTime.UtcNow;

                switch (entrada.State)
                {
                    case EntityState.Added:
                        entrada.CurrentValues[nameof(Entidade.DataCriacao)] = now;
                        entrada.CurrentValues[nameof(Entidade.CodigoEscritorio)] = _codigoEscritorioAtual;
                        entrada.CurrentValues[nameof(Entidade.CodigoUsuarioUltimaAlteracao)] = _codigoUsuarioAtual;
                        entrada.CurrentValues[nameof(Entidade.DataUltimaAlteracao)] = now;
                        break;
                    case EntityState.Modified:
                        entrada.CurrentValues[nameof(Entidade.CodigoUsuarioUltimaAlteracao)] = _codigoUsuarioAtual;
                        entrada.CurrentValues[nameof(Entidade.DataUltimaAlteracao)] = now;
                        break;
                    case EntityState.Deleted:
                        entrada.CurrentValues[nameof(Entidade.CodigoUsuarioUltimaAlteracao)] = _codigoUsuarioAtual;
                        entrada.CurrentValues[nameof(Entidade.DataUltimaAlteracao)] = now;
                        entrada.CurrentValues[nameof(Entidade.Apagado)] = true;
                        entrada.State = EntityState.Modified;
                        break;
                }
            }
        }
    }
}
