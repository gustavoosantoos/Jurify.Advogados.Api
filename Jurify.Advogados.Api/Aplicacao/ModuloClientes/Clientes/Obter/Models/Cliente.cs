using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Dominio.Entidades;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Obter.Models
{
    public class Cliente
    {
        public Guid Codigo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }
        public IEnumerable<Anexo> Anexos { get; set; }
        public IEnumerable<ProcessoJuridico> ProcessosJuridicos { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }

        public async static Task<Cliente> FromEntity(Dominio.Entidades.Cliente entidade, ServicoUsuarios servico)
        {
            var usuarioUltimaAlteracao = await servico.ObterInformacoesDeUsuario(entidade.CodigoUsuarioUltimaAlteracao);

            var enderecos = entidade.Enderecos.Select(async e =>
            {
                var usuario = await servico.ObterInformacoesDeUsuario(e.CodigoUsuarioUltimaAlteracao);
                return new Endereco
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
                    NomeUsuarioUltimaAlteracao = usuario.ObterNomeCompleto()
                };
            });

            var processos = entidade.Processos.Select(p => new ProcessoJuridico
                {
                    Codigo = p.Codigo,
                    Numero = p.Numero.Numero,
                    Titulo = p.Titulo.Valor,
                    Descricao = p.Descricao.Valor,
                    JuizResponsavel = p.JuizResponsavel,
                    UF = p.UF.UF,
                    Status = p.Status.ToString(),
                    TipoDePapel = p.TipoDePapel.ToString(),
                    CodigoAdvogadoResponsavel = p.CodigoAdvogadoResponsavel,
                    CodigoCliente = p.CodigoCliente
            });


            var anexos = entidade.Anexos.Select(a => new Anexo {
                Codigo = a.Codigo,
                NomeArquivo = a.NomeArquivo
            });

            return new Cliente
            {
                Codigo = entidade.Codigo,
                Nome = entidade.Nome.PrimeiroNome,
                Sobrenome = entidade.Nome.Sobrenome,
                DataNascimento = entidade.DataNascimento.Data,
                Email = entidade.Email.Endereco,
                RG = entidade.RG.Numero,
                CPF = entidade.CPF.Numero,
                Enderecos = await Task.WhenAll(enderecos),
                Anexos = anexos,
                ProcessosJuridicos = processos,
                DataCriacao = entidade.DataCriacao,
                DataUltimaAlteracao = entidade.DataUltimaAlteracao,
                NomeUsuarioUltimaAlteracao = usuarioUltimaAlteracao.ObterNomeCompleto()
            };
        }
    }
}
