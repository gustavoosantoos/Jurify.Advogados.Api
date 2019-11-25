using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ListarMensagensPublicas.Models
{
    public class Mensagem
    {
        public Guid Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Texto { get; set; }
    }
}