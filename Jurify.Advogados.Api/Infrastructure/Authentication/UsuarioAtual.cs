using System;

namespace Jurify.Advogados.Api.Infrastructure.Authentication
{
    public class UsuarioAtual
    {
        public UsuarioAtual(Guid id, string nome, string sobrenome, EscritorioAtual escritorio)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Escritorio = escritorio;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public EscritorioAtual Escritorio { get; private set; }

    }
}
