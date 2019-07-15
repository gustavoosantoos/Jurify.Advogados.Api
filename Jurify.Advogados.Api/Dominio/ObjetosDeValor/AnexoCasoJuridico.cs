using System.Collections.Generic;
using Jurify.Advogados.Api.Domain.Base;

namespace Jurify.Advogados.Api.Domain.ObjetosDeValor
{
    public class AnexoCasoJuridico : ObjetoDeValor
    {
        public string FileName { get; private set; }
        public string FileUrl { get; private set; }

        public AnexoCasoJuridico(string fileName, string fileUrl)
        {
            FileName = fileName;
            FileUrl = fileUrl;
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return FileName;
            yield return FileUrl;
        }
    }
}
