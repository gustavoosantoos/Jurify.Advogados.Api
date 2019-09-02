using Jurify.Advogados.Api.Dominio.Base;
using System.Collections.Generic;
using System.Linq;

namespace Jurify.Advogados.Api.Dominio.Enums
{
    public class EEstadoBrasileiro : ObjetoDeValor
    {
        public static readonly EEstadoBrasileiro NAO_INFORMADO = new EEstadoBrasileiro(0, "Não informado", string.Empty);
        public static readonly EEstadoBrasileiro ACRE = new EEstadoBrasileiro(1, "Acre", "AC");
        public static readonly EEstadoBrasileiro ALAGOAS = new EEstadoBrasileiro(2, "Alagoas", "AL");
        public static readonly EEstadoBrasileiro AMAPA = new EEstadoBrasileiro(3, "Amapá", "AP");
        public static readonly EEstadoBrasileiro AMAZONAS = new EEstadoBrasileiro(4, "Amazonas", "AM");
        public static readonly EEstadoBrasileiro BAHIA = new EEstadoBrasileiro(5, "Bahia", "BA");
        public static readonly EEstadoBrasileiro CEARA = new EEstadoBrasileiro(6, "Ceará", "CE");
        public static readonly EEstadoBrasileiro DISTRITO_FEDERAL = new EEstadoBrasileiro(7, "Distrito Federal", "DF");
        public static readonly EEstadoBrasileiro ESPIRITO_SANTO = new EEstadoBrasileiro(8, "Espírito Santo", "ES");
        public static readonly EEstadoBrasileiro GOIAS = new EEstadoBrasileiro(9, "Goiás", "GO");
        public static readonly EEstadoBrasileiro MARANHAO = new EEstadoBrasileiro(10, "Maranhão", "MA");
        public static readonly EEstadoBrasileiro MATO_GROSSO = new EEstadoBrasileiro(11, "Mato Grosso", "MT");
        public static readonly EEstadoBrasileiro MATO_GROSSO_DO_SUL = new EEstadoBrasileiro(12, "Mato Grosso do Sul", "MS");
        public static readonly EEstadoBrasileiro MINAS_GERAIS = new EEstadoBrasileiro(13, "Minas Gerais", "MG");
        public static readonly EEstadoBrasileiro PARA = new EEstadoBrasileiro(14, "Pará", "PA");
        public static readonly EEstadoBrasileiro PARAIBA = new EEstadoBrasileiro(15, "Paraíba", "PB");
        public static readonly EEstadoBrasileiro PARANA = new EEstadoBrasileiro(16, "Paraná", "PR");
        public static readonly EEstadoBrasileiro PERNAMBUCO = new EEstadoBrasileiro(17, "Pernambuco", "PE");
        public static readonly EEstadoBrasileiro PIAUI = new EEstadoBrasileiro(18, "Piauí", "PI");
        public static readonly EEstadoBrasileiro RIO_DE_JANEIRO = new EEstadoBrasileiro(19, "Rio de Janeiro", "RJ");
        public static readonly EEstadoBrasileiro RIO_GRANDE_DO_NORTE = new EEstadoBrasileiro(20, "Rio Grande do Norte", "RN");
        public static readonly EEstadoBrasileiro RIO_GRANDE_DO_SUL = new EEstadoBrasileiro(21, "Rio Grande do Sul", "RS");
        public static readonly EEstadoBrasileiro RONDONIA = new EEstadoBrasileiro(22, "Rondônia", "RO");
        public static readonly EEstadoBrasileiro RORAIMA = new EEstadoBrasileiro(23, "Roraima", "RR");
        public static readonly EEstadoBrasileiro SANTA_CATARINA = new EEstadoBrasileiro(24, "Santa Catarina", "SC");
        public static readonly EEstadoBrasileiro SAO_PAULO = new EEstadoBrasileiro(25, "São Paulo", "SP");
        public static readonly EEstadoBrasileiro SERGIPE = new EEstadoBrasileiro(26, "Sergipe", "SE");
        public static readonly EEstadoBrasileiro TOCANTINS = new EEstadoBrasileiro(27, "Tocantins", "TO");

        private EEstadoBrasileiro(int codigo, string nome, string uf)
        {
            Codigo = codigo;
            Nome = nome;
            UF = uf;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public string UF { get; private set; }

        public static IEnumerable<EEstadoBrasileiro> ObterTodos()
        {
            return new List<EEstadoBrasileiro>
            {
                NAO_INFORMADO,
                ACRE,
                ALAGOAS,
                AMAPA,
                AMAZONAS,
                BAHIA,
                CEARA,
                DISTRITO_FEDERAL,
                ESPIRITO_SANTO,
                GOIAS,
                MARANHAO,
                MATO_GROSSO,
                MATO_GROSSO_DO_SUL,
                MINAS_GERAIS,
                PARA,
                PARAIBA,
                PARANA,
                PERNAMBUCO,
                PIAUI,
                RIO_DE_JANEIRO,
                RIO_GRANDE_DO_NORTE,
                RIO_GRANDE_DO_SUL,
                RONDONIA,
                RORAIMA,
                SANTA_CATARINA,
                SAO_PAULO,
                SERGIPE,
                TOCANTINS
            };
        }

        public static EEstadoBrasileiro ObterPorUF(string uf)
        {
            return ObterTodos().FirstOrDefault(e => e.UF == uf) ?? NAO_INFORMADO;
        }

        public static EEstadoBrasileiro ObterPorCodigo(int codigo)
        {
            return ObterTodos().FirstOrDefault(e => e.Codigo == codigo) ?? NAO_INFORMADO;
        }

        protected override IEnumerable<object> ObterComponentesIgualdade()
        {
            yield return Codigo;
            yield return Nome;
            yield return UF;
        }
    }
}
