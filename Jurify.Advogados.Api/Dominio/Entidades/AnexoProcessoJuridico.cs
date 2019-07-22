using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class AnexoProcessoJuridico : Entidade
    {
        public string NomeArquivo { get; private set; }
        public string Url { get; private set; }

        public AnexoProcessoJuridico(string nomeArquivo, string url)
        {
            NomeArquivo = nomeArquivo;
            Url = url;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(NomeArquivo, "AnexoCasoJuridico.NomeArquivo", "Nome do arquivo não deve ser vazio")
                .IsUrl(Url, "AnexoCasoJuridico.NomeArquivo", "Url do arquivo inválida")
            );
        }

        protected override void Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}
