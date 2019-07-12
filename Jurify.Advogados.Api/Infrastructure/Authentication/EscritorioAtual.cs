using System;

namespace Jurify.Advogados.Api.Infrastructure.Authentication
{
    public class EscritorioAtual
    {
        public EscritorioAtual(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
    }
}
