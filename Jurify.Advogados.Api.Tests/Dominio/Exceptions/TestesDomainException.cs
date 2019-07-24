using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Dominio.Exceptions;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;
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

        [Fact]
        public void ExcecaoDeveLancarArgumentNullExceptionQuandoEntidadeForNula()
        {
            Entidade entidade = null;
            Assert.Throws<ArgumentNullException>(() => new DomainException(entidade));
        }

        [Fact]
        public void ExcecaoDeveConterNotificacoesDoObjetoDeValor()
        {
            var objeto = new Nome(null, null);
            var exception = new DomainException(objeto);

            Assert.Equal(objeto.Notifications.Count, exception.Erros.Length);
        }

        [Fact]
        public void ExcecaoDeveLancarArgumentNullExceptionQuandoObjetoDeValorForNulo()
        {
            ObjetoDeValor objeto = null;
            Assert.Throws<ArgumentNullException>(() => new DomainException(objeto));
        }
    }
}
