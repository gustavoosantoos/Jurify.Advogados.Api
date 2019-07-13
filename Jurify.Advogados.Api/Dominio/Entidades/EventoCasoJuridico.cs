using Jurify.Advogados.Api.Domain.Base;
using Jurify.Advogados.Api.Domain.ObjetosDeValor;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Domain.Entidades
{
    public class EventoCasoJuridico : Entidade
    {
        public CasoJuridico CasoJuridico { get; private set; }
        public DescricaoCasoJuridico Descricao { get; private set; }
        public List<AnexoCasoJuridico> Anexos { get; private set; }
    }
}
