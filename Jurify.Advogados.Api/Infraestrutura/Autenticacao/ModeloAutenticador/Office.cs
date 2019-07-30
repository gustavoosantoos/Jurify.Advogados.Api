using System;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao.ModeloAutenticador
{
    public partial class Office
    {
        public Info Info { get; set; }
        public object[] Users { get; set; }
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
    }
}
