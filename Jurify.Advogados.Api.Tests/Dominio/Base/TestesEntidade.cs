using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Enums;
using System;
using Xunit;

namespace Jurify.Advogados.Api.Tests.Dominio.Base
{
    public class TestesEntidade
    {
        [Fact]
        public void EntidadeDeveCriarCodigoNovo()
        {
            var entidade = ConstruirEntidade();

            Assert.NotEqual(default, entidade.Codigo);
        }

        [Fact]
        public void EntidadeNaoDeveGerarCodigoEscritorio()
        {
            var entidade = ConstruirEntidade();
            Assert.Equal(default, entidade.CodigoEscritorio);
        }

        [Fact]
        public void EntidadesDevemGerarCodigoDiferentes()
        {
            var entidadeA = ConstruirEntidade();
            var entidadeB = ConstruirEntidade();

            Assert.NotEqual(entidadeA.Codigo, entidadeB.Codigo);
        }

        [Fact]
        public void EntidadeDeveGerarDataDeCriacao()
        {
            var entidade = ConstruirEntidade();
            Assert.NotEqual(default, entidade.DataCriacao);
        }

        [Fact]
        public void EntidadeDeveGerarDataDeCriacaoProximaDoAgora()
        {
            var entidade = ConstruirEntidade();
            var now = DateTime.Now;

            Assert.True(entidade.DataCriacao <= now && entidade.DataCriacao >= now.AddSeconds(-1));
        }

        [Fact]
        public void EntidadeNaoDeveGerarDataDeAlteracao()
        {
            var entidade = ConstruirEntidade();
            Assert.Equal(default, entidade.DataUltimaAlteracao);
        }

        [Fact]
        public void EntidadeNaoDevePossuirCodigoUsuarioPorPadrao()
        {
            var entidade = ConstruirEntidade();
            Assert.Equal(default, entidade.CodigoUsuarioUltimaAlteracao);
        }

        [Fact]
        public void EntidadeNaoDeveEstarApagadaPorPadrao()
        {
            var entidade = ConstruirEntidade();
            Assert.False(entidade.Apagado);
        }

        [Fact]
        public void EntidadesDevemGerarHashCodesUnicos()
        {
            var entidadeA = ConstruirEntidade();
            var entidadeB = ConstruirEntidade();

            Assert.NotEqual(entidadeA.GetHashCode(), entidadeB.GetHashCode());
        }

        [Fact]
        public void EntidadesDeveGerarHashCodeIdempotente()
        {
            var entidade = ConstruirEntidade();

            Assert.Equal(entidade.GetHashCode(), entidade.GetHashCode());
        }

        [Fact]
        public void EntidadeDeveSerIgualASiMesma()
        {
            var entidade = ConstruirEntidade();
            Assert.Equal(entidade, entidade);
            Assert.True(entidade == entidade);
            Assert.True(entidade.Equals(entidade));
            Assert.True(entidade.Equals((object)entidade));
        }

        private Entidade ConstruirEntidade()
        {
            return new Endereco(null, null, null, null, null, null, null, null, ETipoEndereco.Residencial);
        }
    }
}
