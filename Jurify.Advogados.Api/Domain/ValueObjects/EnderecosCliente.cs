using Jurify.Advogados.Api.Domain.Enums;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Jurify.Advogados.Api.Domain.ValueObjects
{
    public class EnderecosCliente
    {
        protected HashSet<(string, TipoEndereco)> Enderecos { get; private set; }

        public ImmutableHashSet<(string, TipoEndereco)> Lista() => Enderecos.ToImmutableHashSet();
    }
}
