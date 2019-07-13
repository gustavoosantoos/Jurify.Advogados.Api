using Jurify.Advogados.Api.Domain.Enums;
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
        public EnderecoCliente[] Enderecos { get; set; }

        public class EnderecoCliente
        {
            public string Endereco { get; set; }
            public string Observacoes { get; set; }
            public TipoEndereco Tipo { get; set; }
        }
    }
}
