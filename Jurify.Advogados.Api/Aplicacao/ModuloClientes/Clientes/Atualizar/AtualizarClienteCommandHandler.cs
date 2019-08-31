using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloClientes.Clientes.Atualizar
{
    public class AtualizarClienteCommandHandler : BaseHandler, IRequestHandler<AtualizarClienteCommand, RespostaCasoDeUso>
    {
        public AtualizarClienteCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await Context.Clientes
                .FirstOrDefaultAsync(c => c.Codigo == request.Codigo &&
                                          c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !c.Apagado);

            if (cliente == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            cliente.AtualizarNome(new Nome(request.Nome, request.Sobrenome));
            cliente.AtualizarNascimento(new DataNascimento(request.DataNascimento));
            cliente.AtualizarRG(new RG(request.RG));
            cliente.AtualizarCPF(new CPF(request.CPF));
            cliente.AtualizarEmail(new Email(request.Email));

            if (cliente.Invalid)
                return RespostaCasoDeUso.ComFalha(cliente.Notifications);

            await Context.SaveChangesAsync();
            return RespostaCasoDeUso.ComSucesso(cliente.Codigo);
        }
    }
}
