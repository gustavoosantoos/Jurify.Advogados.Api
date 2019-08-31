using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Enderecos.AtualizarEndereco
{
    public class AtualizarEnderecoCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid Codigo { get; set; }
        public Guid CodigoCliente { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
        public string Observacoes { get; set; }
        public ETipoEndereco Tipo { get; set; }
    }
}
