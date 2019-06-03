using System;

namespace Jurify.Advogados.Api.Domain.Base
{
    public abstract class Entity : IEquatable<Entity>
    {
        protected int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is Entity)) return false;

            return Equals(obj as Entity);
        }

        public bool Equals(Entity other)
        {
            return this.GetType() == other.GetType() &&
                this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Id);
        }
    }
}
