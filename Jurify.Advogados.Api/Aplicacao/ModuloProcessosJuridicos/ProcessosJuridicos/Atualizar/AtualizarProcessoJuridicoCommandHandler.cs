using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.ProcessosJuridicos.Atualizar
{
    public class AtualizarProcessoJuridicoCommandHandler : BaseHandler, IRequestHandler<AtualizarProcessoJuridicoCommand, RespostaCasoDeUso>
    {
        public AtualizarProcessoJuridicoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AtualizarProcessoJuridicoCommand request, CancellationToken cancellationToken)
        {
            var processo = await Context.ProcessosJuridicos
                .FirstOrDefaultAsync(c => c.Codigo == request.Codigo &&
                                          c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                          !c.Apagado);

            if (processo == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            processo.AtualizarCliente(request.CodigoCliente);
            processo.AtualizarAdvogadoResponsavel(request.CodigoAdvogadoResponsavel);
            processo.AtualizarTitulo(new DescricaoCurta(request.Titulo));
            processo.AtualizarDescricao(new Descricao(request.Descricao));
            processo.AtualizarNumero(new NumeroProcessoJuridico(request.NumeroProcesso), EEstadoBrasileiro.ObterPorCodigo(request.CodigoUF));
            processo.AtualizarStatus(request.Status);
            processo.AtualizarTipo(request.TipoDePapel);

            if (processo.Invalid)
                return RespostaCasoDeUso.ComFalha(processo.Notifications);

            await Context.SaveChangesAsync();
            return RespostaCasoDeUso.ComSucesso(processo.Codigo);
        }
    }
}
