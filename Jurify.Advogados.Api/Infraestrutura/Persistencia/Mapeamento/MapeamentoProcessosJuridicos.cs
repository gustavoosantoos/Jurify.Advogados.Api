using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia.Mapeamento
{
    public class MapeamentoProcessosJuridicos : IEntityTypeConfiguration<ProcessoJuridico>
    {
        public void Configure(EntityTypeBuilder<ProcessoJuridico> builder)
        {
            builder.ToTable("processos_juridicos");
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Codigo).HasColumnName("codigo");
            builder.Property(e => e.CodigoEscritorio).HasColumnName("codigo_escritorio");
            builder.Property(e => e.CodigoAdvogadoResponsavel).HasColumnName("codigo_advogado_responsavel");
            builder.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

            builder.OwnsOne(e => e.Numero, numero =>
            {
                numero.Property(n => n.Numero).HasColumnName("numero");
                numero.Ignore(n => n.Notifications);
            });

            builder.OwnsOne(e => e.Titulo, titulo =>
            {
                titulo.Property(t => t.Valor).HasColumnName("descricao_curta");
                titulo.Ignore(t => t.Notifications);
            });

            builder.OwnsOne(e => e.Descricao, descricao =>
            {
                descricao.Property(d => d.Valor).HasColumnName("descricao");
                descricao.Ignore(d => d.Notifications);
            });

            builder.Property(e => e.UF).HasColumnName("uf").HasConversion(
                v => v.UF,
                v => EEstadoBrasileiro.ObterPorUF(v)
            );

            builder.Property(e => e.JuizResponsavel).HasColumnName("juiz_responsavel");
            builder.Property(e => e.Status).HasColumnName("status");
            builder.Property(e => e.TipoDePapel).HasColumnName("tipo_papel");

            builder.HasMany(e => e.Eventos).WithOne(e => e.Processo).HasForeignKey(e => e.CodigoProcesso);
            builder.Metadata.FindNavigation(nameof(ProcessoJuridico.Eventos)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");
            builder.Ignore(e => e.Notifications);
        }
    }
}
