using Jurify.Advogados.Api.Domain.Enums;
using Jurify.Advogados.Api.Dominio.Base;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class Endereco : Entidade
    {
        public Endereco(
            string rua,
            string numero,
            string cidade,
            string estado,
            string pais,
            string cep,
            string observacoes,
            TipoEndereco tipo)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Cep = cep;
            Observacoes = observacoes;
            Tipo = tipo;
        }

        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string Pais { get; private set; }
        public string Cep { get; private set; }
        public string Observacoes { get; private set; }
        public TipoEndereco Tipo { get; private set; }


        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Rua;
            yield return Numero;
            yield return Cidade;
            yield return Estado;
            yield return Pais;
            yield return Cep;
            yield return Tipo;
            yield return Observacoes;
        }
    }
}
