using System.Collections.Generic;
using Jurify.Advogados.Api.Domain.Base;

namespace Jurify.Advogados.Api.Domain.ObjetosDeValor
{
    public class DescricaoCasoJuridico : ObjetoDeValor
    {
        public string Value { get; private set; }

        public DescricaoCasoJuridico(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
