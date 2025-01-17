﻿using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.ReativarMensagemPublica
{
    public class ReativarMensagemPublicaCommandHandler : BaseHandler, IRequestHandler<ReativarMensagemPublicaCommand, RespostaCasoDeUso>
    {
        public ReativarMensagemPublicaCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ReativarMensagemPublicaCommand request, CancellationToken cancellationToken)
        {
            var mensagem = await Context
                .MensagensPublicas
                .FirstOrDefaultAsync(m => m.Codigo == request.Codigo &&
                                          m.Status == EStatusMensagemPublica.EscritorioInteressado &&
                                          m.CodigoEscritorio != Guid.Empty &&
                                          m.Apagado == false);

            if (mensagem == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            mensagem.RejeitarEscritorio();
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(mensagem.Codigo);
        }
    }
}
