using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class DataHoraEventoProcessoJuridico : ObjetoDeValor
    {
        public DateTime Valor { get; set; }

        public DataHoraEventoProcessoJuridico(DateTime dataHoraEvento)
        {
            Valor = dataHoraEvento;

            AddNotifications(new Contract()
                .AreNotEquals(Valor, default, "DataHoraEvento", "A data/hora do evento deve possuir um valor")
            );
        }

        public bool EhDataFutura()
        {
            return DateTime.Now <= Valor;
        }

        public bool EhDataPassada()
        {
            return !EhDataFutura();
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Valor;
        }
    }
}
