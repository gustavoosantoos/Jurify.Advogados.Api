using System.Collections.Generic;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.ListarMensagens
{
    public class ListagemMensagens
    {
        public int MensagensLidas { get; set; }
        public int MensagensPendentes { get; set; }
        public IEnumerable<Mensagem> Mensagens { get; set; }
    }
}
