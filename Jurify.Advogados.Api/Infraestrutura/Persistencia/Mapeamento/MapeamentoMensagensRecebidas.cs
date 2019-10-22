using Jurify.Advogados.Api.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jurify.Advogados.Api.Infraestrutura.Persistencia.Mapeamento
{
    public class MapeamentoMensagensRecebidas : IEntityTypeConfiguration<MensagemRecebida>
    {
        public void Configure(EntityTypeBuilder<MensagemRecebida> builder)
        {
            builder.ToTable("mensagens_recebidas");
            builder.HasKey(e => e.Codigo);
            builder.Property(e => e.Codigo).HasColumnName("codigo");
            builder.Property(e => e.CodigoEscritorio).HasColumnName("codigo_escritorio");

            builder.Property(e => e.NomeCliente).HasColumnName("nome_cliente");
            builder.Property(e => e.ContatoCliente).HasColumnName("contato_cliente");

            builder.OwnsOne(e => e.CpfCliente, cpf =>
            {
                cpf.Property(c => c.Numero).HasColumnName("cpf_cliente");
                cpf.Ignore(c => c.Notifications);
            });

            builder.OwnsOne(e => e.Mensagem, mensagem =>
            {
                mensagem.Property(m => m.Valor).HasColumnName("mensagem");
                mensagem.Ignore(m => m.Notifications);
            });

            builder.Property(e => e.DataCriacao).HasColumnName("data_criacao");
            builder.Property(e => e.DataUltimaAlteracao).HasColumnName("data_ultima_alteracao");
            builder.Property(e => e.CodigoUsuarioUltimaAlteracao).HasColumnName("codigo_usuario_ultima_alteracao");
            builder.Property(e => e.Apagado).HasColumnName("apagado");

            builder.Ignore(e => e.Notifications);
        }
    }
}
