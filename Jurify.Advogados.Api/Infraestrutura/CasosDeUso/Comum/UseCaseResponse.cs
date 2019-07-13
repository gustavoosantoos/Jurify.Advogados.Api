namespace Jurify.Advogados.Api.Infraestrutura.CasosDeUso.Comum
{
    public class RespostaCasoDeUso
    {
        public bool Sucesso { get; private set; }
        public string[] Erros { get; private set; }
        public object Dados { get; private set; }

        public static RespostaCasoDeUso ComFalha(params string[] erros)
        {
            return new RespostaCasoDeUso
            {
                Sucesso = false,
                Erros = erros
            };
        }

        public static RespostaCasoDeUso ComSucesso(object dados = null)
        {
            return new RespostaCasoDeUso
            {
                Sucesso = true,
                Dados = dados
            };
        }
    }
}
