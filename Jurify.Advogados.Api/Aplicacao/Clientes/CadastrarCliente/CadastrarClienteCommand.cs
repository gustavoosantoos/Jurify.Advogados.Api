using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.CadastrarCliente
{
    public class CadastrarClienteCommand : IRequest<RespostaCasoDeUso>
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public EnderecoCliente[] Enderecos { get; set; } = new EnderecoCliente[0];

        public class EnderecoCliente
        {
            public string Rua { get; set; }
            public string Numero { get; set; }
            public string Complemento { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string Pais { get; set; }
            public string Cep { get; set; }
            public string Observacoes { get; set; }
            public TipoEndereco Tipo { get; set; }
        }
    }
}
