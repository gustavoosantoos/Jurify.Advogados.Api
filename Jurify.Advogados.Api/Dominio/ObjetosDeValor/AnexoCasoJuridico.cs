using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class AnexoCasoJuridico : ObjetoDeValor
    {
        public string NomeArquivo { get; private set; }
        public string Url { get; private set; }

        public AnexoCasoJuridico(string nomeArquivo, string url)
        {
            NomeArquivo = nomeArquivo;
            Url = url;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(NomeArquivo, "AnexoCasoJuridico.NomeArquivo", "Nome do arquivo não deve ser vazio")
                .IsUrl(Url, "AnexoCasoJuridico.NomeArquivo", "Url do arquivo inválida")
            );
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return NomeArquivo;
            yield return Url;
        }
    }
}
