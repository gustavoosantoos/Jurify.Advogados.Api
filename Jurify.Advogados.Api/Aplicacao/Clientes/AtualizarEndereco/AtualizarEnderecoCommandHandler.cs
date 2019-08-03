using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.AtualizarEndereco
{
    public class AtualizarEnderecoCommandHandler : BaseHandler, IRequestHandler<AtualizarEnderecoCommand, RespostaCasoDeUso>
    {
        public AtualizarEnderecoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AtualizarEnderecoCommand request, CancellationToken cancellationToken)
        {
            var endereco = await Context.Enderecos
                .FirstOrDefaultAsync(e => e.Codigo == request.Codigo &&
                                          e.CodigoCliente == request.CodigoCliente &&
                                          e.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !e.Apagado);

            if (endereco == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            endereco.Atualizar(
                request.Rua,
                request.Numero,
                request.Cidade,
                request.Estado,
                request.Pais,
                request.Cep,
                request.Complemento,
                request.Observacoes,
                request.Tipo
            );

            if (endereco.Invalid)
            {
                return RespostaCasoDeUso.ComFalha(endereco.Notifications);
            }

            await Context.SaveChangesAsync();
            return RespostaCasoDeUso.ComSucesso(endereco.Codigo);
        }
    }
}
