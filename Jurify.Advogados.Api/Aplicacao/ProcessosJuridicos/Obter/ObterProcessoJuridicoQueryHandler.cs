using Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Obter.Models;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Obter
{
    public class ObterProcessoJuridicoQueryHandler : BaseHandler, IRequestHandler<ObterProcessoJuridicoQuery, RespostaCasoDeUso>
    {
        public ObterProcessoJuridicoQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ObterProcessoJuridicoQuery request, CancellationToken cancellationToken)
        {
            var processo = await Context.ProcessosJuridicos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.Codigo == request.Codigo &&
                                     p.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                     !p.Apagado);

            if (processo == null)
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);

            var usuarioUltimaAlteracao = await ServicoUsuarios.ObterInformacoesDeUsuario(processo.CodigoUsuarioUltimaAlteracao);
            var processoDto = ProcessoJuridico.FromEntity(processo);
            processoDto.NomeUsuarioUltimaAlteracao = usuarioUltimaAlteracao.ObterNomeCompleto();

            if (processoDto.CodigoAdvogadoResponsavel.HasValue)
            {
                var usuarioAdvogadoResponsavel = await ServicoUsuarios.ObterInformacoesDeUsuario(processoDto.CodigoAdvogadoResponsavel.Value);
                processoDto.NomeAdvogadoResponsavel = usuarioUltimaAlteracao.ObterNomeCompleto();
            }

            return RespostaCasoDeUso.ComSucesso(processoDto);
        }
    }
}
