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

            builder.OwnsOne(e => e.RG, rg =>
            {
                rg.Property(r => r.Numero).HasColumnName("rg");
                rg.Ignore(r => r.Notifications);
            });

            builder.OwnsOne(e => e.CPF, cpf =>
            {
                cpf.Property(c => c.Numero).HasColumnName("cpf");
                cpf.Ignore(c => c.Notifications);
            });

            builder.OwnsOne(e => e.DataNascimento, data =>
            {
                data.Property(d => d.Data).HasColumnName("data_nascimento");
                data.Ignore(d => d.Notifications);
            });

            builder.OwnsOne(e => e.Email, email =>
            {
                email.Property(e => e.Endereco).HasColumnName("email");
                email.Ignore(e => e.Notifications);
            });

            builder.HasMany(e => e.Enderecos).WithOne().HasForeignKey(e => e.CodigoCliente);
            builder.Metadata.FindNavigation(nameof(Cliente.Enderecos)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");

            builder.Ignore(e => e.Notifications);
        }
    }
}
