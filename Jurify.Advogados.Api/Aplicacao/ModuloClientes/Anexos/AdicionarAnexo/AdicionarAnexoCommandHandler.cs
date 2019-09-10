using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Servicos;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.AdicionarAnexo
{
    public class AdicionarAnexoCommandHandler : BaseHandler, IRequestHandler<AdicionarAnexoCommand, RespostaCasoDeUso>
    {
        private readonly IServicoDeArmazenamento _servicoDeArmazenamento;

        public AdicionarAnexoCommandHandler(
            JurifyContext context,
            ServicoUsuarios provedor,
            IServicoDeArmazenamento servicoDeArmazenamento) : base(context, provedor)
        {
            _servicoDeArmazenamento = servicoDeArmazenamento;
        }

        public async Task<RespostaCasoDeUso> Handle(AdicionarAnexoCommand request, CancellationToken cancellationToken)
        {
            Cliente cliente = await ObterCliente(request.CodigoCliente);

            if (cliente == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            AnexoCliente anexo = await SalvarAnexoNaCloud(request);
            cliente.AdicionarAnexo(anexo);

            if (cliente.Invalid)
            {
                await _servicoDeArmazenamento.RemoverArquivo(anexo.Identificador);
                return RespostaCasoDeUso.ComFalha(cliente.Notifications);
            }

            await Context.SaveChangesAsync();
            return RespostaCasoDeUso.ComSucesso(anexo.Codigo);
        }

        private async Task<Cliente> ObterCliente(Guid id)
        {
            return await Context.Clientes
               .FirstOrDefaultAsync(c => c.Codigo == id &&
                                         c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                         !c.Apagado);
        }

        private async Task<AnexoCliente> SalvarAnexoNaCloud(AdicionarAnexoCommand command)
        {
            var identificadorArquivo = $"anexos-clientes-{Guid.NewGuid()}";
            var urlArquivo = await _servicoDeArmazenamento.SalvarArquivo(identificadorArquivo, command.Arquivo);
            return new AnexoCliente(command.CodigoCliente, command.NomeArquivo, identificadorArquivo, urlArquivo);
        }
    }
}
