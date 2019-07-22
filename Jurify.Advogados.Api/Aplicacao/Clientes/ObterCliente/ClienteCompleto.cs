using Jurify.Advogados.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.ObterCliente
{
    public class ClienteCompleto
    {
        public Guid Codigo { get; set; }
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public IEnumerable<EnderecoCliente> Enderecos { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }

        public static ClienteCompleto FromEntity(Cliente entidade)
        {
            var enderecos = entidade.Enderecos.Select(e => new EnderecoCliente
            {
                Codigo = e.Codigo,
                Rua = e.Rua,
                Numero = e.Numero,
                Complemento = e.Complemento,
                Cidade = e.Cidade,
                Estado = e.Estado,
                Pais = e.Pais,
                Cep = e.Cep,
                Observacoes = e.Observacoes,
                Tipo = e.Tipo, 
                DataCriacao = e.DataCriacao,
                DataUltimaAlteracao = e.DataUltimaAlteracao,
                NomeUsuarioUltimaAlteracao = "Indisponível"
            });

            return new ClienteCompleto
            {
                Codigo = entidade.Codigo,
                PrimeiroNome = entidade.Nome.PrimeiroNome,
                Sobrenome = entidade.Nome.Sobrenome,
                DataNascimento = entidade.DataNascimento.Data,
                Email = entidade.Email.Endereco,
                RG = entidade.RG.Numero,
                CPF = entidade.CPF.Numero,
                Enderecos = enderecos,
                DataCriacao = entidade.DataCriacao,
                DataUltimaAlteracao = entidade.DataUltimaAlteracao,
                NomeUsuarioUltimaAlteracao = "Indisponível"
            };
        }
    }
}
