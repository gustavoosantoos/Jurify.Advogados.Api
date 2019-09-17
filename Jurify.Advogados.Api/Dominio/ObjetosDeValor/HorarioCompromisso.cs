using System;
using System.Collections.Generic;
using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class HorarioCompromisso : ObjetoDeValor
    {
        public DateTime Inicio { get; private set; }
        public DateTime? Final { get; private set; }

        public bool CompromissoNoDiaTodo => Inicio.TimeOfDay == TimeSpan.Zero;

        protected HorarioCompromisso() { }

        public HorarioCompromisso(DateTime inicio, DateTime? final)
        {
            Inicio = inicio;
            Final = final;

            AddNotifications(new Contract()
                .IsTrue(Inicio > DateTime.Now, "InicioCompromisso", "O início do compromisso deve ser no futuro")
                .IsTrue(!Final.HasValue || Final.Value > Inicio, "FinalCompromisso", "O final do compromisso deve ser após o início")
                .IsTrue(!Final.HasValue || Inicio.Date == Final?.Date, "Compromisso", "O final do compromisso deve estar no mesmo dia do início")
            );
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Inicio;
            yield return Final;
        }
    }
}
