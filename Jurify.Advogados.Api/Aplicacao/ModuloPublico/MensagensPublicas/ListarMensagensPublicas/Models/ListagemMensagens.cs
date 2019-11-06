using System.Collections.Generic;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ListarMensagensPublicas.Models
{
    public class ListagemMensagens
    {
        public ListagemMensagens(IEnumerable<Mensagem> mensagensMural, IEnumerable<Mensagem> mensagensEscritorio)
        {
            MensagensMural = mensagensMural;
            MensagensEscritorio = mensagensEscritorio;
        }

        public IEnumerable<Mensagem> MensagensMural { get; set; }
        public IEnumerable<Mensagem> MensagensEscritorio { get; set; }
    }
}
