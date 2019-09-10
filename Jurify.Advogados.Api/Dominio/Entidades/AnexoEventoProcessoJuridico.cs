using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using System;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class AnexoEventoProcessoJuridico : Entidade
    {
        public string NomeArquivo { get; private set; }
        public string Identificador { get; set; }
        public string Url { get; private set; }

        public Guid CodigoEvento { get; private set; }
        public EventoProcessoJuridico Evento { get; private set; }

        protected AnexoEventoProcessoJuridico()
        {

        }

        public AnexoEventoProcessoJuridico(Guid codigoEvento, string nomeArquivo, string identificador, string url)
        {
            CodigoEscritorio = codigoEvento;
            NomeArquivo = nomeArquivo;
            Identificador = identificador;
            Url = url;

            Validar();
        }

        protected override void Validar()
        {
            AddNotifications(new Contract()
              .IsNotNullOrEmpty(NomeArquivo, "AnexoCasoJuridico.NomeArquivo", "Nome do arquivo não deve ser vazio")
              .IsUrl(Url, "AnexoCasoJuridico.NomeArquivo", "Url do arquivo inválida")
            );
        }
    }
}
