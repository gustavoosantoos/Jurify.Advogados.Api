using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class Cliente : Entidade
    {
        private readonly ICollection<Endereco> _enderecos;

        public Nome Nome { get; private set; }
        public DateTime? DataNascimento { get; private set; }

        public IReadOnlyCollection<Endereco> Enderecos => _enderecos.ToArray();

        protected Cliente()
        {
            _enderecos = new List<Endereco>();
        }

        public Cliente(Nome nome, DateTime? dataNascimento, IEnumerable<Endereco> enderecos)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            _enderecos = enderecos.ToList();
        }
    }
}
