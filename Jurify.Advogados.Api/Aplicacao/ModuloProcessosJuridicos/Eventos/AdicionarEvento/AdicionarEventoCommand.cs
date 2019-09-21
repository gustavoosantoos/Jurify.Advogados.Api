using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.AdicionarEvento
{
    public class AdicionarEventoCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid CodigoProcessoJuridico { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraEvento { get; set; }
        public bool AdicionarNaAgenda { get; set; }

        public EventoProcessoJuridico AsEntity()
        {
            var descricao = string.IsNullOrEmpty(Descricao) ? 
                Dominio.ObjetosDeValor.Descricao.CriarDescricaoVazia() :
                new Descricao(Descricao);

            var titulo = new DescricaoCurta(Titulo);
            var dataHoraEvento = new DataHoraEventoProcessoJuridico(DataHoraEvento);

            return new EventoProcessoJuridico(CodigoProcessoJuridico, titulo, descricao, dataHoraEvento);
        }
    }
}
