using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Cadastrar
{
    public class CadastrarProcessoJuridicoCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid CodigoCliente { get; set; }
        public Guid? CodigoAdvogadoResponsavel { get; set; }
        public string NumeroProcesso { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public EStatusProcessoJuridico Status { get; set; }
        public ETipoDePapelProcessoJuridico TipoDePapel { get; set; }

        public ProcessoJuridico AsEntity()
        {
            return new ProcessoJuridico(
                CodigoAdvogadoResponsavel,
                CodigoCliente,
                new NumeroProcessoJuridico(NumeroProcesso),
                new DescricaoCurta(Titulo),
                new Descricao(Descricao),
                Status,
                TipoDePapel
            );
        }
    }
}
