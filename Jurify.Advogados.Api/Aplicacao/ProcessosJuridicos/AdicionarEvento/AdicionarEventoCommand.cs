using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.AdicionarEvento
{
    public class AdicionarEventoCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid CodigoProcessoJuridico { get; set; }
        public string Descricao { get; set; }
        public AnexoEventoProcessoJuridico[] Anexos { get; set; }

        public EventoProcessoJuridico AsEntity()
        {
            var evento = new EventoProcessoJuridico(
                CodigoProcessoJuridico,
                new Descricao(Descricao)
            );

            foreach (var anexo in Anexos)
            {
                evento.AdicionarAnexo(new Dominio.ObjetosDeValor.AnexoEventoProcessoJuridico(
                    evento.Codigo,
                    anexo.NomeArquivo,
                    anexo.Url
                ));
            }

            return evento;
        }
    }
}
