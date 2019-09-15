using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class EventoProcessoJuridico : Entidade
    {
        private readonly List<AnexoEventoProcessoJuridico> _anexos;

        public Guid CodigoProcesso { get; private set; }
        public DescricaoCurta Titulo { get; private set; }
        public Descricao Descricao { get; private set; }
        public DataHoraEventoProcessoJuridico DataHora { get; private set; }

        public ProcessoJuridico Processo { get; private set; }
        public IReadOnlyCollection<AnexoEventoProcessoJuridico> Anexos => _anexos;

        protected EventoProcessoJuridico()
        {
            _anexos = new List<AnexoEventoProcessoJuridico>();
        }

        public EventoProcessoJuridico(Guid codigoProcesso, DescricaoCurta titulo, Descricao descricao, DataHoraEventoProcessoJuridico dataHora)
        {
            CodigoProcesso = codigoProcesso;
            Titulo = titulo;
            Descricao = descricao;
            DataHora = dataHora;
            _anexos = new List<AnexoEventoProcessoJuridico>();

            Validar();
        }

        public void AtualizarDescricao(Descricao novaDescricao)
        {
            AddNotifications(novaDescricao);
            Descricao = novaDescricao;
        }

        public void AdicionarAnexo(AnexoEventoProcessoJuridico anexo)
        {
            AddNotifications(anexo);
            _anexos.Add(anexo);
        }

        protected override void Validar()
        {
            AddNotifications(Titulo, Descricao, DataHora);
        }
    }
}
