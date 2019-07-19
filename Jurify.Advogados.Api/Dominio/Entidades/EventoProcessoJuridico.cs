using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class EventoProcessoJuridico : Entidade
    {
        private readonly ICollection<AnexoProcessoJuridico> _anexos;

        public ProcessoJuridico CasoJuridico { get; private set; }
        public Descricao Descricao { get; private set; }
        public IReadOnlyCollection<AnexoProcessoJuridico> Anexos => _anexos.ToArray();

        public EventoProcessoJuridico(Descricao descricao)
        {
            Descricao = descricao;
            _anexos = new List<AnexoProcessoJuridico>();
        }
    }
}
