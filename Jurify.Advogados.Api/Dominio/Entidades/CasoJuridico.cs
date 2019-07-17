using Jurify.Advogados.Api.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class CasoJuridico : Entidade
    {
        private readonly ICollection<EventoCasoJuridico> _eventos;

        public Guid IdAdvogadoResponsavel { get; private set; }
        public Guid IdCliente { get; private set; }

        public Cliente Cliente { get; private set; }
        public IReadOnlyCollection<EventoCasoJuridico> Eventos => _eventos.ToArray();

        public CasoJuridico(Guid idAdvogadoResponsavel, Guid idCliente)
        {
            IdAdvogadoResponsavel = idAdvogadoResponsavel;
            IdCliente = idCliente;

            _eventos = new List<EventoCasoJuridico>();
        }
    }
}
