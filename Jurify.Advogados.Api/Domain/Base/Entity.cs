using System;

namespace Jurify.Advogados.Api.Domain.Base
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid IdEscritorio { get; protected set; }
        public Guid Id { get; protected set; }

        public DateTime Criacao { get; protected set; }
        public DateTime? UltimaAlteracao { get; protected set; }
        public Guid? CodigoUsuarioUltimaAlteracao { get; protected set; }
        public bool Apagado { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(Guid idEscritorio, Guid id)
        {
            IdEscritorio = idEscritorio;
            Id = id;
            Criacao = DateTime.UtcNow;
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
