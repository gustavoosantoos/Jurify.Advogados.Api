using System;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao
{
    public class UsuarioAtual
    {
        public UsuarioAtual(Guid codigo, string nome, string sobrenome, EscritorioAtual escritorio)
        {
            Codigo = codigo;
            Nome = nome;
            Sobrenome = sobrenome;
            Escritorio = escritorio;
        }

        public Guid Codigo { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public EscritorioAtual Escritorio { get; private set; }

    }
}
