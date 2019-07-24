using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Xunit;

namespace Jurify.Advogados.Api.Tests.Dominio.Base
{
    public class TestesObjetoDeValor
    {
        [Fact]
        public void ObjetoDeValorDeveSerIgualASiMesmo()
        {
            var nomeA = new Nome("teste", "sobrenome teste");

            Assert.Equal(nomeA, nomeA);
            Assert.True(nomeA.Equals(nomeA));
            Assert.True(nomeA.Equals(nomeA));
        }

        [Fact]
        public void ObjetosDeValorComMesmosValoresDevemSerIguais()
        {
            var nomeA = new Nome("teste", "sobrenome teste");
            var nomeB = new Nome("teste", "sobrenome teste");

            Assert.Equal(nomeA, nomeB);
            Assert.True(nomeA == nomeB);
            Assert.True(nomeA.Equals(nomeB));
            Assert.True(nomeB.Equals(nomeA));
            Assert.False(nomeA != nomeB);
        }

        [Fact]
        public void ObjetosDeValorComValoresDiferentesDevemSerDiferentes()
        {
            var nomeA = new Nome("teste", "sobrenome teste");
            var nomeB = new Nome("teste", "sobrenome teste 2");

            Assert.NotEqual(nomeA, nomeB);
            Assert.False(nomeA == nomeB);
            Assert.False(nomeA.Equals(nomeB));
            Assert.False(nomeB.Equals(nomeA));
            Assert.True(nomeA != nomeB);
        }

        [Fact]
        public void ObjetoDeValorDeveGerarHashCodeIdempotente()
        {
            var nome = new Nome("teste", "sobrenome");
            Assert.Equal(nome.GetHashCode(), nome.GetHashCode());
        }

        [Fact]
        public void ObjetoDeValorDeveGerarHashCodeIgualSeForemOsMesmosValores()
        {
            var nomeA = new Nome("teste", "sobrenome");
            var nomeB = new Nome("teste", "sobrenome");

            Assert.Equal(nomeA.GetHashCode(), nomeB.GetHashCode());
        }

        [Fact]
        public void ObjetosDeValoresComValoresDiferentesDevemGerarHashCodesDiferentes()
        {
            var nomeA = new Nome("teste", "sobrenome");
            var nomeB = new Nome("teste", "sobrenome 2");

            Assert.NotEqual(nomeA.GetHashCode(), nomeB.GetHashCode());
        }
    }
}
