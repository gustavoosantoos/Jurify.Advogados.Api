using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.ListarMensagens
{
    public class Mensagem
    {
        public Guid Codigo { get; set; }
        public Guid? CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string ContatoCliente { get; set; }
        public string Texto { get; set; }
    }
}
