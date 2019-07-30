using System;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao.Modelo
{
    public class Escritorio
    {
        public Escritorio(Guid codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }

        public Guid Codigo { get; private set; }
        public string Nome { get; private set; }
    }
}
