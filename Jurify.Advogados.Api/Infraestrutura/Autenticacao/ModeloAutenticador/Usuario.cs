using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao.ModeloAutenticador
{
    public partial class Usuario
    {
        public Guid Codigo { get; set; }
        public Guid CodigoEscritorio { get; set; }
        public Office Office { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public PersonalInfo InformacoesPessoais { get; set; }
        public List<Permissao> Permissoes { get;  set; }
        public bool Apagado { get; set; }
    }
}
