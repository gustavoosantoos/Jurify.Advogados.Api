using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Domain.Base
{
    public abstract class ObjetoDeValor : IEquatable<ObjetoDeValor>
    {
        protected abstract IEnumerable<object> ObterComponentesIgualdade();

        public bool Equals(ObjetoDeValor other)
        {
            return this.GetType() == other.GetType() &&
                this.ObterComponentesIgualdade().SequenceEqual(other.ObterComponentesIgualdade());
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ObjetoDeValor)) return false;

            return this.Equals(obj as ObjetoDeValor);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;

                foreach (var item in this.ObterComponentesIgualdade())
                {
                    hash = HashCode.Combine(hash, item) * 31;
                }

                return hash;
            }
        }
    }
}
