using Flunt.Notifications;
using System;

namespace Jurify.Advogados.Api.Dominio.Base
{
    public abstract class Entidade : Notifiable, IEquatable<Entidade>
    {
        public Guid CodigoEscritorio { get; protected set; }
        public Guid Codigo { get; protected set; }

        public DateTime DataCriacao { get; protected set; }
        public DateTime DataUltimaAlteracao { get; protected set; }
        public Guid CodigoUsuarioUltimaAlteracao { get; protected set; }
        public bool Apagado { get; protected set; }

        protected Entidade()
        {
            Codigo = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is Entidade)) return false;

            return Equals(obj as Entidade);
        }

        public bool Equals(Entidade other)
        {
            return
                GetType() == other.GetType() &&
                CodigoEscritorio == other.CodigoEscritorio &&
                Codigo == other.Codigo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), CodigoEscritorio, Codigo);
        }

        protected abstract void Validar();
    }
}
