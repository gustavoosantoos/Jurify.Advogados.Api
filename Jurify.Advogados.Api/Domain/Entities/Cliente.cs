using Jurify.Advogados.Api.Domain.Base;
using Jurify.Advogados.Api.Domain.ValueObjects;

namespace Jurify.Advogados.Api.Domain.Entities
{
    public class Cliente : Entity
    {
        public InformacoesPessoaisCliente InformacoesPessoais { get; set; }
    }
}
