using Jurify.Advogados.Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia.Mapeamento
{
    public class MapeamentoClientes : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("clientes");
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Codigo).HasColumnName("codigo");

            builder.Property(e => e.CodigoEscritorio).HasColumnName("codigo_escritorio");

            builder.OwnsOne(e => e.Nome, nome =>
            {
                nome.Property(n => n.PrimeiroNome).HasColumnName("nome");
                nome.Property(n => n.Sobrenome).HasColumnName("sobrenome");
                nome.Ignore(n => n.Notifications);
            });

            builder.Property(e => e.DataNascimento).HasColumnName("data_nascimento");

            builder.HasMany<Endereco>("_enderecos").WithOne().HasForeignKey(e => e.CodigoCliente);

            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");

            builder.Ignore(e => e.Enderecos);
            builder.Ignore(e => e.Notifications);
        }
    }
}
