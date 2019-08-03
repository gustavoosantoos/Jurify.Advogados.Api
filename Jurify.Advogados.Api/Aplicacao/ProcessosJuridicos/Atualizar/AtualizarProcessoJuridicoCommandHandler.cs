﻿using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Atualizar
{
    public class AtualizarProcessoJuridicoCommandHandler : BaseHandler, IRequestHandler<AtualizarProcessoJuridicoCommand, RespostaCasoDeUso>
    {
        public AtualizarProcessoJuridicoCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public Task<RespostaCasoDeUso> Handle(AtualizarProcessoJuridicoCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
