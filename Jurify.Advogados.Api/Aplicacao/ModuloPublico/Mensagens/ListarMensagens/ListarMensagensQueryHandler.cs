using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.ListarMensagens
{
    public class ListarMensagensQueryHandler : BaseHandler, IRequestHandler<ListarMensagensQuery, RespostaCasoDeUso>
    {
        public ListarMensagensQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ListarMensagensQuery request, CancellationToken cancellationToken)
        {
            var mensagens = await Context
                .MensagensRecebidas
                .Where(m => m.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo && !m.Apagado)
                .Select(m => new Mensagem
                {
                    Codigo = m.Codigo,
                    ContatoCliente = m.ContatoCliente,
                    CpfCliente = m.CpfCliente.Numero,
                    NomeCliente = m.NomeCliente,
                    Texto = m.Mensagem.Valor
                })
                .ToListAsync();

            foreach (var m in mensagens)
            {
                var cliente = Context.Clientes.FirstOrDefault(c => c.CPF.Numero == m.CpfCliente);
                
                if (cliente != null)
                {
                    m.CodigoCliente = cliente.Codigo;
                    m.NomeCliente = cliente.Nome.ObterNomeCompleto();
                }
            }

            return RespostaCasoDeUso.ComSucesso(mensagens);
        }
    }
}
