using Jurify.Advogados.Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia.Mapeamento
{
    public class MapeamentoCompromissosAgenda : IEntityTypeConfiguration<CompromissoAgenda>
    {
        public void Configure(EntityTypeBuilder<CompromissoAgenda> builder)
        {
            builder.ToTable("agenda_compromissos");
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Codigo).HasColumnName("codigo");
            builder.Property(e => e.CodigoEscritorio).HasColumnName("codigo_escritorio");

            builder.OwnsOne(e => e.Titulo, titulo =>
            {
                titulo.Property(t => t.Valor).HasColumnName("titulo");
                titulo.Ignore(t => t.Notifications);
            });

            builder.OwnsOne(e => e.Descricao, descricao =>
            {
                descricao.Property(d => d.Valor).HasColumnName("descricao");
                descricao.Ignore(d => d.Notifications);
            });

            builder.OwnsOne(e => e.Horario, horario =>
            {
                horario.Property(h => h.Inicio).HasColumnName("data_hora_inicio_compromisso");
                horario.Property(h => h.Final).HasColumnName("data_hora_final_compromisso");
                horario.Ignore(h => h.Notifications);
            });

            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");

            builder.Ignore(e => e.Notifications);
        }
    }
}
