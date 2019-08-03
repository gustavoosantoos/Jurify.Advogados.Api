
using Flunt.Br.Validation;
using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Enums;
using System;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class Endereco : Entidade
    {
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public string Cep { get; private set; }
        public string Complemento { get; private set; }
        public string Observacoes { get; private set; }
        public ETipoEndereco Tipo { get; private set; }

        public Guid CodigoCliente { get; private set; }

        protected Endereco()
        {
        }

        public Endereco(string rua, string numero, string cidade, string estado, string pais, string cep, string complemento, string observacoes, ETipoEndereco tipo)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Cep = cep;
            Complemento = complemento;
            Observacoes = observacoes;
            Tipo = tipo;

            Validar();
        }

        public void Atualizar(string rua, string numero, string cidade, string estado, string pais, string cep, string complemento, string observacoes, ETipoEndereco tipo)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Cep = cep;
            Complemento = complemento;
            Observacoes = observacoes;
            Tipo = tipo;

            Validar();
        }

        protected override void Validar()
        {
            AddNotifications(new Contract()
                .IsNotNullOrWhiteSpace(Rua, "Endereco.Rua", "A rua não deve ser vazia")
                    .HasMaxLen(Rua, 300, "Endereco.Rua", "A rua deve ter ao máximo 300 caracteres")
                .IsNotNullOrWhiteSpace(Numero, "Endereco.Numero", "O número não deve ser vazio")
                    .HasMaxLen(Numero, 5, "Endereco.Numero", "O número deve ter ao máximo 5 caracteres")
                .IsNotNullOrWhiteSpace(Cidade, "Endereco.Cidade", "A cidade não deve ser vazia")
                    .HasMaxLen(Cidade, 100, "Endereco.Cidade", "A cidade deve ter ao máximo 100 caracteres")
                .IsNotNullOrWhiteSpace(Estado, "Endereco.Estado", "O estado não deve ser vazio")
                    .HasMaxLen(Estado, 50, "Endereco.Estado", "O estado deve ter ao máximo 50 caracteres")
                .IsNotNullOrWhiteSpace(Pais, "Endereco.Pais", "O país não deve ser vazio")
                    .HasMaxLen(Pais, 50, "Endereco.Pais", "O país não deve ser vazio")
                .IsCep(Cep, "Endereco.Cep", "CEP inválido")
                .HasMaxLengthIfNotNullOrEmpty(Complemento, 100, "Endereco.Complemento", "O complemento deve ter ao máximo 100 caracteres")
                .HasMaxLengthIfNotNullOrEmpty(Observacoes, 1000, "Endereco.Observacoes", "As observacoes devem ter ao máximo 1000 caracteres")
            );
        }
    }
}
