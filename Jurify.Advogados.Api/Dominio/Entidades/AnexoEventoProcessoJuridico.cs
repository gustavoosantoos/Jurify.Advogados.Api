using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Entidades;
using System;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class AnexoEventoProcessoJuridico : Entidade
    {
        public Guid CodigoEvento { get; private set; }

        public EventoProcessoJuridico Evento { get; private set; }
        public string NomeArquivo { get; private set; }
        public string Url { get; private set; }

        protected AnexoEventoProcessoJuridico()
        {

        }

        public AnexoEventoProcessoJuridico(Guid codigoEvento, string nomeArquivo, string url)
        {
            CodigoEscritorio = codigoEvento;
            NomeArquivo = nomeArquivo;
            Url = url;

            AddNotifications(new Contract()
                .IsNotNullOrEmpty(NomeArquivo, "AnexoCasoJuridico.NomeArquivo", "Nome do arquivo não deve ser vazio")
                .IsUrl(Url, "AnexoCasoJuridico.NomeArquivo", "Url do arquivo inválida")
            );
        }

        protected override void Validar()
        {

        }
    }
}
