using System;

namespace Jurify.Advogados.Api.Domain.Base
{
    public abstract class Entity : IEquatable<Entity>
    {
        protected Guid IdEscritorio { get; set; }
        protected Guid Id { get; set; }

        protected Entity()
        {
        }

        protected Entity(Guid idEscritorio, Guid id)
        {
            IdEscritorio = idEscritorio;
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is Entity)) return false;

            return Equals(obj as Entity);
        }

        public bool Equals(Entity other)
        {
            return
                GetType() == other.GetType() &&
                IdEscritorio == other.IdEscritorio &&
                Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), IdEscritorio, Id);
        }
    }
}
