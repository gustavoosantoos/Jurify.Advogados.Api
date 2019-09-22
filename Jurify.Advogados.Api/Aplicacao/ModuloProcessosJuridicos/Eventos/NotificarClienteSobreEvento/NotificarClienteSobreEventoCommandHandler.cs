using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.Servicos;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloProcessosJuridicos.Eventos.NotificarClienteSobreEvento
{
    public class NotificarClienteSobreEventoCommandHandler : BaseHandler, IRequestHandler<NotificarClienteSobreEventoCommand, RespostaCasoDeUso>
    {
        private readonly IServicoDeEmail _servicoEmail;
        private readonly IConfiguration _configuration;

        public NotificarClienteSobreEventoCommandHandler(
            JurifyContext context,
            ServicoUsuarios provedor,
            IServicoDeEmail servicoEmail,
            IConfiguration configuration) : base(context, provedor)
        {
            _servicoEmail = servicoEmail;
            _configuration = configuration;
        }

        public async Task<RespostaCasoDeUso> Handle(NotificarClienteSobreEventoCommand request, CancellationToken cancellationToken)
        {
            var processo = await Context
                .ProcessosJuridicos
                .Include(p => p.Eventos)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p =>
                    p.Codigo == request.CodigoProcesso &&
                    p.CodigoEscritorio == ServicoUsuarios.EscritorioAtual.Codigo &&
                    !p.Apagado
                );

            var evento = processo?.Eventos?.FirstOrDefault(e => e.Codigo == request.CodigoEvento);

            if (processo == null || evento == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            if (processo.Cliente.Email == null)
            {
                return RespostaCasoDeUso.ComFalha("O cliente não possui e-mail cadastrado");
            }

            bool sucessoNoEnvioDeEmail = await _servicoEmail.EnviarEmail(
                _configuration["Email:Remetente"],
                _configuration["Email:Senha"],
                processo.Cliente.Email.Endereco,
                ConstruirAssuntoEmail(),
                ConstruirCorpoEmail(processo, evento)
            );

            if (!sucessoNoEnvioDeEmail)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.InternalServerError);
            }

            return RespostaCasoDeUso.ComSucesso();
        }

        private string ConstruirAssuntoEmail()
        {
            return "Notificação de atualização em processo jurídico";
        }

        private string ConstruirCorpoEmail(ProcessoJuridico processo, EventoProcessoJuridico evento)
        {
            return $@"
<h3>O seu processo jurídico com número {processo.Numero.Numero} tem uma nova atualização: </h3><br/>

<strong>Evento:</strong> {evento.Titulo.Valor} <br /><br />

<strong>Dia e horário:</strong> {evento.DataHora.Valor.ToString("dd/MM/yyyy HH:mm")} <br/><br/>

{evento.Descricao.Valor} <br /><br />

Entre em contato com o seu advogado para maiores informações. <br /><br/>

<strong>Jurify.</strong>";
        }
    }
}
