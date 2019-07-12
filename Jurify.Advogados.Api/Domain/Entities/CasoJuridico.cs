using Jurify.Advogados.Api.Domain.Base;

namespace Jurify.Advogados.Api.Domain.Entities
{
    public class CasoJuridico : Entity
    {
        public Cliente Cliente { get; protected set; }
    }
}
