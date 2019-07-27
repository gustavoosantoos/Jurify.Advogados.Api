using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.CadastrarProcessoJuridico
{
    public class CadastrarProcessoJuridicoCommandHandler : BaseHandler, IRequestHandler<CadastrarProcessoJuridicoCommand, RespostaCasoDeUso>
    {
        public CadastrarProcessoJuridicoCommandHandler(JurifyContext context, ProvedorUsuarioAtual provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(CadastrarProcessoJuridicoCommand request, CancellationToken cancellationToken)
        {
            var processo = request.AsEntity();
            if (processo.Invalid)
                return RespostaCasoDeUso.ComFalha(processo.Notifications);

            var clienteExiste = await Context.Clientes
                .AnyAsync(c => c.Codigo == request.CodigoCliente &&
                               c.CodigoEscritorio == Provedor.Escritorio.Codigo &&
                               !c.Apagado);

            if (!clienteExiste)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound, "Cliente não encontrado");

            var processoExiste = await Context.ProcessosJuridicos
                .AnyAsync(p => p.Numero.Numero == request.NumeroProcesso &&
                               p.CodigoEscritorio == Provedor.Escritorio.Codigo &&
                               !p.Apagado);

            if (processoExiste)
                return RespostaCasoDeUso.ComFalha("Já existe um processo com o mesmo número");

            await Context.ProcessosJuridicos.AddAsync(processo);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(processo.Codigo);
        }
    }
}
