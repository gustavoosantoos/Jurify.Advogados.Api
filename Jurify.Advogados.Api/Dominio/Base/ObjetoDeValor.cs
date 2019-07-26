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
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is ObjetoDeValor)) return false;

            return this == (obj as ObjetoDeValor);
        }

        public static bool operator ==(ObjetoDeValor objetoA, ObjetoDeValor objetoB)
        {
            if (ReferenceEquals(objetoA, objetoB))
                return true;

            if (objetoA is null || objetoB is null)
                return false;

            return objetoA.GetType() == objetoB.GetType() &&
                objetoA.ObterComponentesIgualdade().SequenceEqual(objetoB.ObterComponentesIgualdade());
        }

        public static bool operator !=(ObjetoDeValor objetoA, ObjetoDeValor objetoB)
        {
            return !(objetoA == objetoB);
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
