using System;

namespace Jurify.Advogados.Api.Infraestrutura.Autenticacao
{
    public class ProvedorUsuarioAtual
    {
        public UsuarioAtual Usuario { get; private set; }
        public EscritorioAtual Escritorio { get; private set; }

        public void AtualizarUsuario(UsuarioAtual usuario, EscritorioAtual escritorio)
        {
            if (Usuario != null || usuario == null || Escritorio != null || escritorio == null)
                throw new ArgumentException("Não é possível atualizar o usuário atual");

            Usuario = usuario;
            Escritorio = escritorio;
        }
    }
}
