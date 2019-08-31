using Jurify.Advogados.Api.Dominio.Servicos;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Anexos.BaixarAnexo
{
    public class BaixarAnexoQueryHandler : BaseHandler, IRequestHandler<BaixarAnexoQuery, RespostaCasoDeUso>
    {
        private readonly IServicoDeArmazenamento _servicoDeArmazenamento;

        public BaixarAnexoQueryHandler(
            JurifyContext context,
            ServicoUsuarios provedor,
            IServicoDeArmazenamento servicoDeArmazenamento) : base(context, provedor)
        {
            _servicoDeArmazenamento = servicoDeArmazenamento;
        }

        public async Task<RespostaCasoDeUso> Handle(BaixarAnexoQuery request, CancellationToken cancellationToken)
        {
            var anexo = await Context.AnexosClientes
                .FirstOrDefaultAsync(c => c.Codigo == request.CodigoAnexo &&
                                          c.CodigoCliente == request.CodigoCliente &&
                                          c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !c.Apagado);

            if (anexo == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            var streamArquivo = await _servicoDeArmazenamento.ObterArquivo(anexo.Identificador);

            return RespostaCasoDeUso.ComSucesso(new Anexo(anexo.NomeArquivo, streamArquivo));
        }
    }
}
