﻿using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Listar
{
    public class ListarProcessosJuridicosQueryHandler : BaseHandler, IRequestHandler<ListarProcessosJuridicosQuery, RespostaCasoDeUso>
    {
        public ListarProcessosJuridicosQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ListarProcessosJuridicosQuery request, CancellationToken cancellationToken)
        {
            var usuarioAtual = await ServicoUsuarios.ObterInformacoesDeUsuario(ServicoUsuarios.UsuarioAtual.Codigo);
            var processos = await Context.ProcessosJuridicos
                .Include(p => p.Cliente)
                .Where(p => p.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo && !p.Apagado && (p.CodigoAdvogadoResponsavel == usuarioAtual.Codigo || usuarioAtual.EhAdministrador))
                .Select(p => new ProcessoJuridicoPreview
                {
                    Codigo = p.Codigo,
                    CodigoAdvogadoResponsavel = p.CodigoAdvogadoResponsavel,
                    CodigoCliente = p.CodigoCliente,
                    NomeCliente = p.Cliente.Nome.PrimeiroNome,
                    NumeroProcesso = p.Numero.Numero,
                    Titulo = p.Titulo.Valor,
                    Status = p.Status,
                    UF = p.UF.UF,
                    TipoDePapel = p.TipoDePapel,
                    DataCriacao = p.DataCriacao,
                    DataUltimaAtualizacao = p.DataUltimaAlteracao
                })
                .ToListAsync();

            Task.WaitAll(processos.Select(async processo =>
            {
                if (!processo.CodigoAdvogadoResponsavel.HasValue)
                    return;

                processo.NomeAdvogadoResponsavel = (await ServicoUsuarios
                                                   .ObterInformacoesDeUsuario(processo.CodigoAdvogadoResponsavel.Value))
                                                   .ObterNomeCompleto();
            }).ToArray());

            var listagem = new ListagemProcessos
            {
                QuantidadeProcessos = processos.Count,
                QuantidadeProcessosAtivos = processos.Count(p => p.Status != EStatusProcessoJuridico.Finalizado),
                Processos = processos
            };

            return RespostaCasoDeUso.ComSucesso(listagem);
        }
    }
}
