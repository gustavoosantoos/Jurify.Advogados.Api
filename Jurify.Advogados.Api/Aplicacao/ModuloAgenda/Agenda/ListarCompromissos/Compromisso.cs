using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloAgenda.Agenda.ListarCompromissos
{
    public class Compromisso
    {
        public Guid Codigo { get; set; }
        public string Titulo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime? Final { get; set; }
    }
}
