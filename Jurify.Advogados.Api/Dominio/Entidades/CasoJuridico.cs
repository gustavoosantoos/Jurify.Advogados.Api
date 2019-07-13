using Jurify.Advogados.Api.Domain.Base;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Domain.Entidades
{
    public class CasoJuridico : Entidade
    {
        public Guid IdAdvogadoResponsavel { get; private set; }
        public Cliente Cliente { get; private set; }
        public IReadOnlyCollection<EventoCasoJuridico> Eventos { get; private set; }
    }
}
