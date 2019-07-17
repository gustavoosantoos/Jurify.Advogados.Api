using Jurify.Advogados.Api.Dominio.Entidades;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using Jurify.Advogados.Api.Infraestrutura.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Jurify.Advogados.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly JurifyContext _context;

        public ClientesController(JurifyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_context.Clientes.ToList());
        }

        [HttpPost]
        public ActionResult Post()
        {
            var cliente = new Cliente(new InformacoesPessoaisCliente(new Nome("Anna", "Kida"), Convert.ToDateTime("1998-12-07")));
            cliente.AdicionarEndereco(new Endereco(
                "Rua Padre Victor Dewor",
                "222",
                "Curitiba",
                "PR",
                "Brasil",
                "80280100",
                "",
                "",
                Dominio.Enums.TipoEndereco.Residencial
            ));

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Ok(cliente);
        }
    }
}