using Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ListarMensagensPublicas.Models;
using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ListarMensagensPublicas
{
    public class ListarMensagensPublicasQueryHandler : BaseHandler, IRequestHandler<ListarMensagensPublicasQuery, RespostaCasoDeUso>
    {
        public ListarMensagensPublicasQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ListarMensagensPublicasQuery request, CancellationToken cancellationToken)
        {
            var mensagensEmAberto = await ObterMensagensPublicas();
            var mensagensDoEscritorio = await ObterMensagensDoEscritorio();
            var listagem = new ListagemMensagens(mensagensEmAberto, mensagensDoEscritorio);
            return RespostaCasoDeUso.ComSucesso(listagem);
        }

        public async Task<List<Mensagem>> ObterMensagensPublicas()
        {
            return await Context
                .MensagensPublicas
                .Where(m => m.CodigoEscritorio == Guid.Empty &&
                            m.Status == EStatusMensagemPublica.Publica &&
                            m.Apagado == false)
                .Select(m => new Mensagem
                {
                    Codigo = m.Codigo,
                    CPF = m.CpfCliente.Numero,
                    Nome = m.NomeCliente,
                    Email = m.ContatoCliente.Endereco
                })
                .ToListAsync();
        }

        public async Task<List<Mensagem>> ObterMensagensDoEscritorio()
        {
            return await Context
                .MensagensPublicas
                .Where(m => m.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo && 
                            m.Apagado == false &&
                            (m.Status == EStatusMensagemPublica.EscritorioInteressado || m.Status == EStatusMensagemPublica.ConfirmadaPeloCliente)
                      )
                .Select(m => new Mensagem
                {
                    Codigo = m.Codigo,
                    CPF = m.CpfCliente.Numero,
                    Nome = m.NomeCliente,
                    Email = m.ContatoCliente.Endereco
                })
                .ToListAsync();
        }

    }
}
