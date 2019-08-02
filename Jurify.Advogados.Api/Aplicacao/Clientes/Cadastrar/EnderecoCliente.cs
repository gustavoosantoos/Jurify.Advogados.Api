using Jurify.Advogados.Api.Dominio.Enums;

namespace Jurify.Advogados.Api.Aplicacao.Clientes.Cadastrar
{
    public class EnderecoCliente
    {
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Cep { get; set; }
        public string Observacoes { get; set; }
        public ETipoEndereco Tipo { get; set; }
    }
}
