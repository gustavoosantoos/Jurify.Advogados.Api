using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.Obter.Models
{
    public class Cliente
    {
        public Guid Codigo { get; set; }
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }

        public async static Task<Cliente> FromEntity(Dominio.Entidades.Cliente entidade, ServicoUsuarios servico)
        {
            var usuarioUltimaAlteracao = await servico.ObterInformacoesDeUsuario(entidade.CodigoUsuarioUltimaAlteracao);

            var enderecos = entidade.Enderecos.Select(e => new Endereco
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
                DataUltimaAlteracao = e.DataUltimaAlteracao
            });

            return new Cliente
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
                NomeUsuarioUltimaAlteracao = usuarioUltimaAlteracao.ObterNomeCompleto()
            };
        }
    }
}
