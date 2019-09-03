using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Dominio.Exceptions;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class ProcessoJuridico : Entidade
    {
        private readonly List<EventoProcessoJuridico> _eventos;

        public NumeroProcessoJuridico Numero { get; private set; }
        public DescricaoCurta Titulo { get; private set; }
        public Descricao Descricao { get; private set; }
        public string JuizResponsavel { get; private set; }
        public EEstadoBrasileiro UF { get; private set; }
        public EStatusProcessoJuridico Status { get; private set; }
        public ETipoDePapelProcessoJuridico TipoDePapel { get; private set; }

        public Guid? CodigoAdvogadoResponsavel { get; private set; }
        public Guid CodigoCliente { get; private set; }
        public Cliente Cliente { get; private set; }
        public IReadOnlyCollection<EventoProcessoJuridico> Eventos => _eventos;

        protected ProcessoJuridico()
        {
            _eventos = new List<EventoProcessoJuridico>();
        }

        public ProcessoJuridico(Guid? idAdvogadoResponsavel,
                                Guid idCliente,
                                NumeroProcessoJuridico numero,
                                DescricaoCurta titulo,
                                Descricao descricao,
                                string juizResponsavel,
                                EEstadoBrasileiro uf,
                                EStatusProcessoJuridico status,
                                ETipoDePapelProcessoJuridico tipoDePapel)
        {
            CodigoAdvogadoResponsavel = idAdvogadoResponsavel;
            CodigoCliente = idCliente;
            Numero = numero;
            UF = uf;
            Titulo = titulo;
            Descricao = descricao;
            JuizResponsavel = juizResponsavel;
            Status = status;
            TipoDePapel = tipoDePapel;

            _eventos = new List<EventoProcessoJuridico>();
            Validar();
        }

        protected override void Validar()
        {
            AddNotifications(Numero, Titulo, Descricao);
            AddNotifications(_eventos.ToArray());
        }

        public void AdicionarEvento(EventoProcessoJuridico evento)
        {
            AddNotifications(evento);
            _eventos.Add(evento);
        }

        public void AtualizarCliente(Guid codigoCliente)
        {
            CodigoCliente = codigoCliente;
        }

        public void AtualizarAdvogadoResponsavel(Guid? codigoAdvogadoResponsavel)
        {
            CodigoAdvogadoResponsavel = codigoAdvogadoResponsavel;
        }

        public void AtualizarNumero(NumeroProcessoJuridico numero, EEstadoBrasileiro uf)
        {
            AddNotifications(numero, uf);
            Numero = numero;
            UF = uf;
        }

        public void AtualizarTitulo(DescricaoCurta titulo)
        {
            AddNotifications(titulo);
            Titulo = titulo;
        }

        public void AtualizarDescricao(Descricao descricao)
        {
            AddNotifications(descricao);
            Descricao = descricao;
        }

        public void AtualizarJuizResponsavel(string novoJuiz)
        {
            JuizResponsavel = novoJuiz;
        }

        public void AtualizarStatus(EStatusProcessoJuridico status)
        {
            Status = status;
        }

        public void AtualizarTipo(ETipoDePapelProcessoJuridico tipo)
        {
            TipoDePapel = tipo;
        }
    }
}
