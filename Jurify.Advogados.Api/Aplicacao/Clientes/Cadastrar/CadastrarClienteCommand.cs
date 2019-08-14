using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;
using System.Linq;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.Cadastrar
{
    public partial class CadastrarClienteCommand : IRequest<RespostaCasoDeUso>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public EnderecoCliente[] Enderecos { get; set; } = new EnderecoCliente[0];

        public Cliente AsEntity()
        {
            var nome = new Nome(Nome, Sobrenome);
            var rg = new RG(RG);
            var cpf = new CPF(CPF);
            var dataNascimento = new DataNascimento(DataNascimento);
            var email = new Email(Email);
            var enderecos = Enderecos.Select(e => new Endereco(
                e.Rua,
                e.Numero,
                e.Cidade,
                e.Estado,
                e.Pais,
                e.Cep?.Replace("-", string.Empty),
                e.Complemento,
                e.Observacoes,
                e.Tipo
            ));

            return new Cliente(nome, rg, cpf, dataNascimento, email, enderecos.ToList());
        }
    }
}
