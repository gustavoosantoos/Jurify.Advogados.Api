using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using MediatR;
using System;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.MensagensPublicas.AdicionarMensagemPublica
{
    public class AdicionarMensagemPublicaCommand : IRequest<RespostaCasoDeUso>
    {
        public Guid CodigoEscritorio { get; set; }
        public string Nome { get; set; }
        public string Contato { get; set; }
        public string CPF { get; set; }
        public string Mensagem { get; set; }

        public MensagemPublica AsEntity()
        {
            return new MensagemPublica(
                CodigoEscritorio,
                Nome,
                new Email(Contato),
                new CPF(CPF),
                new Descricao(Mensagem)
            );
        }
    }
}
