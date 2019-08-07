﻿using System;
using System.Collections.Generic;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;

namespace Jurify.Advogados.Api.Aplicacao.ProcessosJuridicos.Obter.Models
{
    public class Evento
    {
        public Guid Codigo { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<Anexo> Anexos { get; set; }
    }
}
