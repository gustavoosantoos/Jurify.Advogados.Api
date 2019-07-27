using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class ProcessoJuridico : Entidade
    {
        private readonly List<EventoProcessoJuridico> _eventos;

        public Guid CodigoAdvogadoResponsavel { get; private set; }
        public Guid CodigoCliente { get; private set; }

        public NumeroProcessoJuridico Numero { get; private set; }
        public Cliente Cliente { get; private set; }
        public DescricaoCurta Titulo { get; private set; }
        public Descricao Descricao { get; private set; }
        public EStatusProcessoJuridico Status { get; private set; }
        public ETipoDePapelProcessoJuridico TipoDePapel { get; private set; }
        public IReadOnlyCollection<EventoProcessoJuridico> Eventos => _eventos;

        protected ProcessoJuridico()
        {
            _eventos = new List<EventoProcessoJuridico>();
        }

        public ProcessoJuridico(Guid idAdvogadoResponsavel, Guid idCliente, EStatusProcessoJuridico status, ETipoDePapelProcessoJuridico tipoDePapel)
        {
            CodigoAdvogadoResponsavel = idAdvogadoResponsavel;
            CodigoCliente = idCliente;
            Status = status;
            TipoDePapel = tipoDePapel;

            _eventos = new List<EventoProcessoJuridico>();
        }

        protected override void Validar()
        {
            AddNotifications(Numero, Titulo, Descricao);
        }
    }
}
