using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloAgenda.Agenda.ListarCompromissos
{
    public class ListarCompromissosQueryHandler : BaseHandler, IRequestHandler<ListarCompromissosQuery, RespostaCasoDeUso>
    {
        public ListarCompromissosQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ListarCompromissosQuery request, CancellationToken cancellationToken)
        {
            var compromissos = await Context
                .CompromissosAgenda
                .Where(c => 
                    c.CodigoAdvogado == ServicoUsuarios.UsuarioAtual.Codigo &&
                    c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                    !c.Apagado
                ).Select(c => new Compromisso {
                    Codigo = c.Codigo,
                    CodigoCliente = c.CodigoCliente,
                    Titulo = c.Titulo.Valor,
                    Descricao = c.Descricao.Valor,
                    Inicio = c.Horario.Inicio,
                    Final  = c.Horario.Final
                }).ToListAsync();

            return RespostaCasoDeUso.ComSucesso(compromissos);
        }
    }
}
