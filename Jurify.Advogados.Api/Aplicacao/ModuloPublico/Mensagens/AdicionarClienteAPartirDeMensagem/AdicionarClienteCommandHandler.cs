using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.Autenticacao;
using Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Jurify.Advogados.Api.Aplicacao.ModuloPublico.Mensagens.AdicionarClienteAPartirDeMensagem
{
    public class AdicionarClienteCommandHandler : BaseHandler, IRequestHandler<AdicionarClienteCommand, RespostaCasoDeUso>
    {
        public AdicionarClienteCommandHandler(JurifyContext context, ServicoUsuarios provedor) : base(context, provedor)
        {
        }

        public async Task<RespostaCasoDeUso> Handle(AdicionarClienteCommand request, CancellationToken cancellationToken)
        {
            var mensagem = await Context
                .MensagensRecebidas
                .FirstOrDefaultAsync(m => m.Codigo == request.CodigoMensagem);   

            if (mensagem == null)
            {
                return RespostaCasoDeUso.ComStatusCode(HttpStatusCode.NotFound);
            }

            var clienteExiste = await Context
                .Clientes
                .AnyAsync(c => c.CPF == mensagem.CpfCliente);

            if (clienteExiste)
            {
                return RespostaCasoDeUso.ComFalha("Um cliente já existe com o mesmo CPF");
            }

            var novoCliente = new Cliente(
                ConstruirNomeCliente(mensagem.NomeCliente),
                new RG(null),
                mensagem.CpfCliente,
                new DataNascimento(null),
                new Email(mensagem.ContatoCliente),
                new List<Endereco>()
            );

            if (novoCliente.Invalid)
            {
                return RespostaCasoDeUso.ComFalha(novoCliente.Notifications);
            }

            await Context.Clientes.AddAsync(novoCliente);
            await Context.SaveChangesAsync();

            return RespostaCasoDeUso.ComSucesso(novoCliente.Codigo);
        }

        private Nome ConstruirNomeCliente(string nomeCompleto)
        {
            var nome = nomeCompleto.Split(" ")[0];
            var sobrenome = string.Join(" ", nomeCompleto.Split(" ").Skip(1));

            return new Nome(nome, sobrenome);
        }
    }
}
