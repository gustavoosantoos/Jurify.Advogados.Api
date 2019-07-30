using System;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao.ModeloAutenticador
{
    public partial class Usuario
    {
        public Guid OfficeId { get; set; }
        public Office Office { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public object[] Claims { get; set; }
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
    }
}
