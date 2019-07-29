using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.ListarProcessosJuridicos
{
    public class ListarProcessosJuridicosQueryHandler : BaseHandler, IRequestHandler<ListarProcessosJuridicosQuery, RespostaCasoDeUso>
    {
        public ListarProcessosJuridicosQueryHandler(JurifyContext context, ProvedorUsuarioAtual provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ListarProcessosJuridicosQuery request, CancellationToken cancellationToken)
        {
            var processos = await Context.ProcessosJuridicos
                .Where(p => p.CodigoEscritorio == Provedor.Escritorio.Codigo && !p.Apagado)
                .Select(p => new ProcessoJuridicoPreview
                {
                    Codigo = p.Codigo,
                    CodigoAdvogadoResponsavel = p.CodigoAdvogadoResponsavel,
                    NumeroProcesso = p.Numero.Numero,
                    Titulo = p.Titulo.Valor,
                    Status = p.Status,
                    TipoDePapel = p.TipoDePapel,
                    DataCriacao = p.DataCriacao,
                    DataUltimaAtualizacao = p.DataUltimaAlteracao
                })
                .ToListAsync();

            return RespostaCasoDeUso.ComSucesso(processos);
        }
    }
}
