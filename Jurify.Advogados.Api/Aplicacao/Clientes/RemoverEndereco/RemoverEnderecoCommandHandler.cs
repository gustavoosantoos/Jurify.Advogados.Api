using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.RemoverEndereco
{
    public class RemoverEnderecoCommandHandler : BaseHandler, IRequestHandler<RemoverEnderecoCommand, RespostaCasoDeUso>
    {
        public RemoverEnderecoCommandHandler(JurifyContext context, ProvedorUsuarioAtual provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(RemoverEnderecoCommand request, CancellationToken cancellationToken)
        {
            var endereco = await Context.Enderecos
                .FirstOrDefaultAsync(e => e.Codigo == request.Codigo &&
                                          e.CodigoEscritorio == Provedor.Escritorio.Codigo &&
                                          !e.Apagado);

            if (endereco == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            Context.Enderecos.Remove(endereco);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso();
        }
    }
}
