﻿using System;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.ListarClientes
{
    public class ClientePreview
    {
        public Guid Codigo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
    }
}
