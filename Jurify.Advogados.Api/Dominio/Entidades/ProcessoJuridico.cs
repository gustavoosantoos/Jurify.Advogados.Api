using Jurify.Advogados.Api.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class ProcessoJuridico : Entidade
    {
        private readonly List<EventoProcessoJuridico> _eventos;

        public Guid IdAdvogadoResponsavel { get; private set; }
        public Guid IdCliente { get; private set; }

        public Cliente Cliente { get; private set; }
        public IReadOnlyCollection<EventoProcessoJuridico> Eventos => _eventos;

        public ProcessoJuridico(Guid idAdvogadoResponsavel, Guid idCliente)
        {
            IdAdvogadoResponsavel = idAdvogadoResponsavel;
            IdCliente = idCliente;

            _eventos = new List<EventoProcessoJuridico>();
        }

        protected override void Validar()
        {
            throw new NotImplementedException();
        }
    }
}
