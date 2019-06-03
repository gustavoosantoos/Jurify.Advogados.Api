using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Domain.Base
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public bool Equals(ValueObject other)
        {
            return this.GetType() == other.GetType() &&
                this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ValueObject)) return false;

            return this.Equals(obj as ValueObject);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;

                foreach (var item in this.GetEqualityComponents())
                {
                    hash = HashCode.Combine(hash, item) * 31;
                }

                return hash;
            }
        }
    }
}
