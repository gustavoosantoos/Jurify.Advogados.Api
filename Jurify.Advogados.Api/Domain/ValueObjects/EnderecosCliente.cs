using Jurify.Advogados.Api.Domain.Enums;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Jurify.Advogados.Api.Domain.ValueObjects
{
    public class EnderecosCliente
    {
        protected HashSet<(string, TipoEndereco)> Enderecos { get; private set; }

        public EnderecosCliente()
        {
            Enderecos = new HashSet<(string, TipoEndereco)>();
        }

        public void AdicionarEndereco(string endereco, TipoEndereco tipo)
        {
            Enderecos.Add((endereco, tipo));
        }

        public void RemoverEndereco(string endereco, TipoEndereco tipo)
        {
            Enderecos.Remove((endereco, tipo));
        }

        public ImmutableHashSet<(string, TipoEndereco)> Lista() => Enderecos.ToImmutableHashSet();
    }
}
