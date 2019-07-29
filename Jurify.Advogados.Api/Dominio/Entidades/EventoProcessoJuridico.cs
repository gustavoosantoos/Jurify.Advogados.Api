﻿using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class EventoProcessoJuridico : Entidade
    {
        private readonly List<AnexoEventoProcessoJuridico> _anexos;

        public Guid CodigoProcesso { get; private set; }

        public ProcessoJuridico Processo { get; private set; }
        public Descricao Descricao { get; private set; }
        public IReadOnlyCollection<AnexoEventoProcessoJuridico> Anexos => _anexos;

        protected EventoProcessoJuridico()
        {
            _anexos = new List<AnexoEventoProcessoJuridico>();
        }

        public EventoProcessoJuridico(Guid codigoProcesso, Descricao descricao)
        {
            CodigoProcesso = codigoProcesso;
            Descricao = descricao;
            _anexos = new List<AnexoEventoProcessoJuridico>();
        }

        protected override void Validar()
        {

        }
    }
}
