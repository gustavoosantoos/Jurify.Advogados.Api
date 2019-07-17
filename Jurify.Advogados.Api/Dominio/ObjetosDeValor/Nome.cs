using System.Collections.Generic;
using Jurify.Advogados.Api.Dominio.Base;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class Nome : ObjetoDeValor
    {
        public string PrimeiroNome { get; private set; }
        public string Sobrenome { get; private set; }

        protected Nome()
        {

        }

        public Nome(string primeiroNome, string ultimoNome)
        {
            PrimeiroNome = primeiroNome;
            Sobrenome = ultimoNome;
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return PrimeiroNome;
            yield return Sobrenome;
        }
    }
}
