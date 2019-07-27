using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia.Mapeamento
{
    public class MapeamentoAnexosEventosProcessosJuridicos : IEntityTypeConfiguration<AnexoEventoProcessoJuridico>
    {
        public void Configure(EntityTypeBuilder<AnexoEventoProcessoJuridico> builder)
        {
            builder.ToTable("processos_juridicos_eventos_anexos");
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Codigo).HasColumnName("codigo");
            builder.Property(e => e.CodigoEscritorio).HasColumnName("codigo_escritorio");
            builder.Property(e => e.CodigoEvento).HasColumnName("codigo_evento");

            builder.Property(e => e.NomeArquivo).HasColumnName("nome");
            builder.Property(e => e.Url).HasColumnName("url");

            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");
            builder.Ignore(e => e.Notifications);
        }
    }
}
