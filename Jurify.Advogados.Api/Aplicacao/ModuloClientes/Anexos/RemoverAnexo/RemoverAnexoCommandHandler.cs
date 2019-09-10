﻿using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Dominio.Servicos;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.RemoverAnexo
{
    public class RemoverAnexoCommandHandler : BaseHandler, IRequestHandler<RemoverAnexoCommand, RespostaCasoDeUso>
    {
        private readonly IServicoDeArmazenamento _servicoDeArmazenamento;

        public RemoverAnexoCommandHandler(
            JurifyContext context,
            ServicoUsuarios provedor,
            IServicoDeArmazenamento servicoDeArmazenamento) : base(context, provedor)
        {
            _servicoDeArmazenamento = servicoDeArmazenamento;
        }

        public async Task<RespostaCasoDeUso> Handle(RemoverAnexoCommand request, CancellationToken cancellationToken)
        {
            var anexo = await Context.AnexosClientes
                .FirstOrDefaultAsync(c => c.Codigo == request.CodigoAnexo &&
                                          c.CodigoCliente == request.CodigoCliente &&
                                          c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !c.Apagado);

            if (anexo == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            var removeuArquivo = await _servicoDeArmazenamento.RemoverArquivo(anexo.Identificador);

            if (!removeuArquivo)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.InternalServerError);
            }

            Context.AnexosClientes.Remove(anexo);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso();
        }
    }
}