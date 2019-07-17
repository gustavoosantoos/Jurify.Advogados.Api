using Jurify.Advogados.Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia.Mapeamento
{
    public class MapeamentoEnderecos : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("enderecos");
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Codigo).HasColumnName("codigo");
            builder.Property(e => e.CodigoEscritorio).HasColumnName("codigo_escritorio");
            builder.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");

            builder.Property(e => e.Rua).HasColumnName("rua");
            builder.Property(e => e.Numero).HasColumnName("numero");
            builder.Property(e => e.Cidade).HasColumnName("cidade");
            builder.Property(e => e.Estado).HasColumnName("estado");
            builder.Property(e => e.Pais).HasColumnName("pais");
            builder.Property(e => e.Cep).HasColumnName("cep");
            builder.Property(e => e.Complemento).HasColumnName("complemento");
            builder.Property(e => e.Observacoes).HasColumnName("observacoes");
            builder.Property(e => e.Tipo).HasColumnName("tipo");

            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");

            builder.Ignore(e => e.Notifications);
        }
    }
}
