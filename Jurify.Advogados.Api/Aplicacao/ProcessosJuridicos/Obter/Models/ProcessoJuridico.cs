﻿using Jurify.Advogados.Api.Dominio.Enums;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Obter.Models
{
    public class ProcessoJuridico
    {
        public Guid Codigo { get; set; }
        public Guid? CodigoAdvogadoResponsavel { get; set; }
        public string NomeAdvogadoResponsavel { get; set; }
        public string Numero { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EStatusProcessoJuridico Status { get; set; }
        public ETipoDePapelProcessoJuridico TipoDePapel { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }

        public Cliente Cliente { get; set; }

        public static ProcessoJuridico FromEntity(Dominio.Entidades.ProcessoJuridico entidade)
        {
            var cliente = new Cliente
            {
                Codigo = entidade.Cliente.Codigo,
                NomeCompleto = entidade.Cliente.Nome.ObterNomeCompleto(),
                CPF = entidade.Cliente.CPF.Numero,
                Idade = entidade.Cliente.DataNascimento.ObterIdade()
            };

            return new ProcessoJuridico
            {
                Codigo = entidade.Codigo,
                CodigoAdvogadoResponsavel = entidade.CodigoAdvogadoResponsavel,
                Numero = entidade.Numero.Numero,
                Titulo = entidade.Titulo.Valor,
                Descricao = entidade.Descricao.Valor,
                Cliente = cliente,
                Status = entidade.Status,
                TipoDePapel = entidade.TipoDePapel,
                DataCriacao = entidade.DataCriacao,
                DataUltimaAlteracao = entidade.DataUltimaAlteracao
            };
        }
    }
}