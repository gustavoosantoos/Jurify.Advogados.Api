using Jurify.Advogados.Api.Dominio.Enums;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloDashboard.Obter
{
    public class ObterDadosQueryHandler : BaseHandler, IRequestHandler<ObterDadosQuery, RespostaCasoDeUso>
    {
        public ObterDadosQueryHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(ObterDadosQuery request, CancellationToken cancellationToken)
        {
            var clientes = await Context
                .Clientes
                .CountAsync(c => c.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                            !c.Apagado);

            var processos = await Context
                .ProcessosJuridicos
                .CountAsync(p => p.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                 p.Status != EStatusProcessoJuridico.Finalizado &&
                                 !p.Apagado);

            var proximoDomingo = ProximoDiaDaSemana(DateTime.Today, DayOfWeek.Sunday);
            
            var compromissos = await Context
                .CompromissosAgenda
                .CountAsync(p => p.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                                 p.CodigoAdvogado == ServicoUsuarios.UsuarioAtual.Codigo &&
                                 p.Horario.Inicio > DateTime.Today &&
                                 p.Horario.Inicio < proximoDomingo &&
                                 !p.Apagado);

            return RespostaCasoDeUso.ComSucesso(new
            {
                clientes,
                processos,
                compromissos
            });
        }

        private DateTime ProximoDiaDaSemana(DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }
    }
}
