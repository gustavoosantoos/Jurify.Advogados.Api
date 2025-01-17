﻿using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Obter.Models
{
    public class ProcessoJuridico
    {
        public Guid Codigo { get; set; }
        public Guid? CodigoAdvogadoResponsavel { get; set; }
        public string NomeAdvogadoResponsavel { get; set; }
        public string Numero { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string JuizResponsavel { get; set; }
        public int CodigoUF { get; set; }
        public EStatusProcessoJuridico Status { get; set; }
        public ETipoDePapelProcessoJuridico TipoDePapel { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }

        public Cliente Cliente { get; set; }
        public IEnumerable<Evento> Eventos { get; set; }

        public static ProcessoJuridico FromEntity(Dominio.Entidades.ProcessoJuridico entidade)
        {
            var cliente = new Cliente
            {
                Codigo = entidade.Cliente.Codigo,
                NomeCompleto = entidade.Cliente.Nome.ObterNomeCompleto(),
                CPF = entidade.Cliente.CPF.Numero,
                Idade = entidade.Cliente.DataNascimento.ObterIdade()
            };

            var eventos = entidade.Eventos.Select(e => new Evento
            {
                Codigo = e.Codigo,
                Titulo = e.Titulo.Valor,
                Descricao = e.Descricao.Valor,
                DataHoraEvento = e.DataHora.Valor,
                DataCriacao = e.DataCriacao,
                Anexos = e.Anexos.Select(a => new Anexo
                {
                    Codigo = a.Codigo,
                    NomeArquivo = a.NomeArquivo,
                    Url = a.Url
                })
            }).OrderByDescending(e => e.DataHoraEvento);

            return new ProcessoJuridico
            {
                Codigo = entidade.Codigo,
                CodigoAdvogadoResponsavel = entidade.CodigoAdvogadoResponsavel,
                Numero = entidade.Numero.Numero,
                Titulo = entidade.Titulo.Valor,
                Descricao = entidade.Descricao.Valor,
                JuizResponsavel = entidade.JuizResponsavel,
                Cliente = cliente,
                CodigoUF = entidade.UF.Codigo,
                Status = entidade.Status,
                TipoDePapel = entidade.TipoDePapel,
                DataCriacao = entidade.DataCriacao,
                DataUltimaAlteracao = entidade.DataUltimaAlteracao,
                Eventos = eventos
            };
        }
    }
}
