using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Enderecos.AdicionarEndereco
{
    public class AdicionarEnderecoCommand : IRequest<RespostaCasoDeUso>
    {
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

        public Endereco AsEntity()
        {
            return new Endereco(
                Rua,
                Numero,
                Cidade,
                Estado,
                Pais,
                Cep?.Replace("-", string.Empty),
                Complemento,
                Observacoes,
                Tipo
            );
        }
    }
}
