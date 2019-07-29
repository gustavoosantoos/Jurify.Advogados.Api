using Flunt.Validations;
using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.Exceptions;
using System;
using System.Collections.Generic;

namespace Jurify.Advogados.Api.Dominio.ObjetosDeValor
{
    public class DataNascimento : ObjetoDeValor
    {
        public DateTime? Data { get; private set; }

        public DataNascimento(DateTime? data)
        {
            Data = data;

            AddNotifications(new Contract()
                .IfNotNull(Data, c => c.IsTrue(Data < DateTime.Now, "DataNascimento", "A data de nascimento deve estar no passado"))
                .IfNotNull(Data, c => c.IsTrue(Data != default, "DataNascimento", "A data de nascimento é inválida"))
            );
        }

        public int? ObterIdade()
        {
            if (Invalid)
            {
                throw new DomainException(this);
            }

            if (!Data.HasValue)
                return null;

            var hoje = DateTime.Today;
            var data = Data.Value;
            var idade = hoje.Year - data.Year;

            if (data.Date > hoje.AddYears(-idade))
                idade--;

            return idade;
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Data;
        }
    }
}
