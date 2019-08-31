using Jurify.Advogados.Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia.Mapeamento
{
    public class MapeamentoAnexosClientes : IEntityTypeConfiguration<AnexoCliente>
    {
        public void Configure(EntityTypeBuilder<AnexoCliente> builder)
        {
            builder.ToTable("clientes_anexos");
            builder.HasKey(e => e.Codigo);

            builder.Property(e => e.Codigo).HasColumnName("codigo");
            builder.Property(e => e.CodigoEscritorio).HasColumnName("codigo_escritorio");
            builder.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

            builder.Property(e => e.NomeArquivo).HasColumnName("nome_arquivo");
            builder.Property(e => e.Identificador).HasColumnName("identificador_arquivo");
            builder.Property(e => e.Url).HasColumnName("url");
            
            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");

            builder.Ignore(e => e.Notifications);
        }
    }
}
