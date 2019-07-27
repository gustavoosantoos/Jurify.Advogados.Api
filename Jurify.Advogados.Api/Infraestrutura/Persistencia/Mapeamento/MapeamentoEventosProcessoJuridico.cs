using Jurify.Advogados.Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia.Mapeamento
{
    public class MapeamentoEventosProcessoJuridico : IEntityTypeConfiguration<EventoProcessoJuridico>
    {
        public void Configure(EntityTypeBuilder<EventoProcessoJuridico> builder)
        {
            builder.ToTable("processos_juridicos_eventos");
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Codigo).HasColumnName("codigo");
            builder.Property(e => e.CodigoEscritorio).HasColumnName("codigo_escritorio");
            builder.Property(e => e.CodigoProcesso).HasColumnName("codigo_processo_juridico");

            builder.OwnsOne(e => e.Descricao, descricao => 
            {
                descricao.Property(d => d.Valor).HasColumnName("descricao");
                descricao.Ignore(d => d.Notifications);
            });

            builder.HasMany(e => e.Anexos).WithOne(a => a.Evento).HasForeignKey(a => a.CodigoEvento);

            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");
            builder.Ignore(e => e.Notifications);
        }
    }
}
