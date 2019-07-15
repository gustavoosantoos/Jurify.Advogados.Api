using Jurify.Advogados.Api.Domain.Base;
using Jurify.Advogados.Api.Domain.ObjetosDeValor;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Domain.Entidades
{
    public class EventoCasoJuridico : Entidade
    {
        private readonly ICollection<AnexoCasoJuridico> _anexos;

        public CasoJuridico CasoJuridico { get; private set; }
        public Descricao Descricao { get; private set; }
        public IReadOnlyCollection<AnexoCasoJuridico> Anexos => _anexos.ToArray();

        public EventoCasoJuridico(Descricao descricao)
        {
            Descricao = descricao;
            _anexos = new List<AnexoCasoJuridico>();
        }
    }
}
