using Jurify.Advogados.Api.Domain.Base;
using Jurify.Advogados.Api.Domain.Enums;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Domain.ValueObjects
{
    public class EnderecoCliente : ValueObject
    {
        public string Endereco { get; private set; }
        public string Observacoes { get; private set; }
        public TipoEndereco Tipo { get; private set; }

        public EnderecoCliente(string endereco, string observacoes, TipoEndereco tipo)
        {
            Endereco = endereco;
            Observacoes = observacoes;
            Tipo = tipo;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Endereco;
            yield return Observacoes;
            yield return Tipo;
        }
    }
}
