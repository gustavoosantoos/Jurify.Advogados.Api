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
            var cliente = await Context.Clientes
                .FirstOrDefaultAsync(c => c.Codigo == request.CodigoCliente &&
                                          c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !c.Apagado);

            if (cliente == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            var identificadorArquivo = ConstruirNomeArquivo();
            var urlArquivo = await _servicoDeArmazenamento.SalvarArquivo(identificadorArquivo, request.Arquivo);

            var anexo = new AnexoCliente(request.CodigoCliente, request.NomeArquivo, identificadorArquivo, urlArquivo);

            if (anexo.Invalid)
            {
                await _servicoDeArmazenamento.RemoverArquivo(urlArquivo);
                return RespostaCasoDeUso.ComFalha(cliente.Notifications);
            }

            cliente.AdicionarAnexo(anexo);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(anexo.Codigo);
        }

        private string ConstruirNomeArquivo()
        {
            return $"anexos-clientes-{Guid.NewGuid()}";
        }
    }
}
