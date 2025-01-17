﻿using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Enderecos.AdicionarEndereco
{
    public class AdicionarEnderecoCommandHandler : BaseHandler, IRequestHandler<AdicionarEnderecoCommand, RespostaCasoDeUso>
    {
        public AdicionarEnderecoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AdicionarEnderecoCommand request, CancellationToken cancellationToken)
        {
            var cliente = await Context.Clientes
                .FirstOrDefaultAsync(c => c.Codigo == request.CodigoCliente &&
                                          c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !c.Apagado);

            if (cliente == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            var endereco = request.AsEntity();

            if (endereco.Invalid)
            {
                return RespostaCasoDeUso.ComFalha(endereco.Notifications);
            }

            cliente.AdicionarEndereco(endereco);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(endereco.Codigo);
        }
    }
}
