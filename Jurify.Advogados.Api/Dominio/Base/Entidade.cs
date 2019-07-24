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
            DataCriacao = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (!(obj is Entidade)) return false;

            return this == (obj as Entidade);
        }

        public bool Equals(Entidade other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), CodigoEscritorio, Codigo);
        }

        public static bool operator ==(Entidade entidadeA, Entidade entidadeB)
        {
            if (ReferenceEquals(entidadeA, entidadeB))
                return true;

            if (entidadeA is null || entidadeB is null)
                return false;

            return
                entidadeA.GetType() == entidadeB.GetType() &&
                entidadeA.CodigoEscritorio == entidadeB.CodigoEscritorio &&
                entidadeA.Codigo == entidadeB.Codigo;
        }

        public static bool operator !=(Entidade entidadeA, Entidade entidadeB)
        {
            return !(entidadeA == entidadeB);
        } 

        protected abstract void Validar();
    }
}
