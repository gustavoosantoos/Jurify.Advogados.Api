using System;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao.ModeloAutenticador
{
    public partial class Office
    {
        public Guid Codigo { get; set; }
        public Info Informacoes { get; set; }
        public object[] Usuarios { get; set; }
        public bool Apagado { get; set; }
    }
}
