using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloAgenda.Agenda.AdicionarCompromisso
{
    public class AdicionarCompromissoCommandHandler : BaseHandler, IRequestHandler<AdicionarCompromissoCommand, RespostaCasoDeUso>
    {
        public AdicionarCompromissoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AdicionarCompromissoCommand request, CancellationToken cancellationToken)
        {
            var entity = new CompromissoAgenda(
                new DescricaoCurta(request.Titulo),
                new Descricao(request.Descricao),
                new HorarioCompromisso(request.Inicio, request.Final),
                request.CodigoCliente,
                ServicoUsuarios.UsuarioAtual.Codigo
            );

            if (entity.Invalid)
            {
                return RespostaCasoDeUso.ComFalha(entity.Notifications);
            }
            
            if (entity.CodigoCliente.HasValue)
            {
                var clienteExiste = await Context.Clientes
                    .AnyAsync(c => c.Codigo == request.CodigoCliente &&
                                   c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                   !c.Apagado);

                if (!clienteExiste)
                    return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            Context.CompromissosAgenda.Add(entity);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(entity.Codigo);
        }
    }
}
