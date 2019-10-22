using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao.ModeloAutenticador
{
    public class Permissao : ValueObject
    {
        public string Nome { get; private set; }
        public string Valor { get; private set; }

        protected Permissao() { }

        public Permissao(string nome, string valor)
        {
            Nome = nome;
            Valor = valor;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nome.ToUpper();
            yield return Valor.ToUpper();
        }
    }
}
