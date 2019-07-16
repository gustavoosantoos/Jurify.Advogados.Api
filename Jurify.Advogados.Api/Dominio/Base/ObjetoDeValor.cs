using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Base
{
    public abstract class ObjetoDeValor : Notifiable, IEquatable<ObjetoDeValor>
    {
        protected abstract IEnumerable<object> ObterComponentesIgualdade();

        public bool Equals(ObjetoDeValor other)
        {
            return GetType() == other.GetType() &&
                ObterComponentesIgualdade().SequenceEqual(other.ObterComponentesIgualdade());
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ObjetoDeValor)) return false;

            return Equals(obj as ObjetoDeValor);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;

                foreach (var item in ObterComponentesIgualdade())
                {
                    hash = HashCode.Combine(hash, item) * 31;
                }

                return hash;
            }
        }
    }
}
