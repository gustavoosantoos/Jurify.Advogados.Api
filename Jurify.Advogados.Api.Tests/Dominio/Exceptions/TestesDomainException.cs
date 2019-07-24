using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Dominio.Exceptions;
using Xunit;

namespace Jurify.Advogados.Api.Tests.Dominio.Exceptions
{
    public class TestesDomainException
    {
        [Fact]
        public void ExcecaoDeveConterNotificacoesDaEntidade()
        {
            var entidade = new Endereco(null, null, null, null, null, null, null, null, ETipoEndereco.Residencial);
            var exception = new DomainException(entidade);

            Assert.Equal(entidade.Notifications.Count, exception.Erros.Length);
        }
    }
}
