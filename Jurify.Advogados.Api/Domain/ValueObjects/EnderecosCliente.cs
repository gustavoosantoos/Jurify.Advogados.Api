using Jurify.Advogados.Api.Domain.Base;
using Jurify.Advogados.Api.Domain.Enums;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Jurify.Advogados.Api.Domain.ValueObjects
{
    public class EnderecosCliente : ValueObject
    {
        public ImmutableHashSet<(string, TipoEndereco)> Enderecos { get; private set; }

        public EnderecosCliente(ImmutableHashSet<(string, TipoEndereco)> enderecos)
        {
            Enderecos = enderecos;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Enderecos;
        }
    }
}
