﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia
{
    public class JurifyContext : DbContext
    {
        private readonly IConfiguration _configuracoes;
        private ProvedorUsuarioAtual _provedor;
        
        public JurifyContext(ProvedorUsuarioAtual provedorUsuario, IConfiguration configuracoes)
        {
            _configuracoes = configuracoes;
            _provedor = provedorUsuario;
        }

        public DbSet<Cliente> Clientes { get; private set; }
        //public DbSet<CasoJuridico> CasosJuridicos { get; private set; }
        //public DbSet<EventoCasoJuridico> EventosCasoJuridico { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
            var codigoUsuarioAtual = _provedor.Usuario.Codigo;
            var codigoEscritorioAtual = _provedor.Usuario.Escritorio.Codigo;

            foreach (var entrada in ChangeTracker.Entries())
            {
                var now = DateTime.UtcNow;

                if (!(entrada.Entity is Entidade))
                    continue;

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
