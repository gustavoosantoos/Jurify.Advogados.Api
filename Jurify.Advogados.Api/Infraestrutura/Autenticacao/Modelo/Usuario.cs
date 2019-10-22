using System;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao.Modelo
{
    public class Usuario
    {
        public Usuario(Guid codigo, string nome, string sobrenome)
        {
            Codigo = codigo;
            Nome = nome;
            Sobrenome = sobrenome;
        }

        public Usuario(Guid codigo, string nome, string sobrenome, bool ehAdministrador )
        {
            Codigo = codigo;
            Nome = nome;
            Sobrenome = sobrenome;
            EhAdministrador = ehAdministrador;
        }

        public Guid Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public bool EhAdministrador { get; private set; }

        public string ObterNomeCompleto()
        {
            return $"{Nome} {Sobrenome}";
        }
    }
}
