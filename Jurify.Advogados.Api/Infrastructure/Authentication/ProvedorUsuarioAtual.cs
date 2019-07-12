using System;

namespace Jurify.Advogados.Api.Infrastructure.Authentication
{
    public class ProvedorUsuarioAtual
    {
        public UsuarioAtual Usuario { get; private set; }

        public void AtualizarUsuario(UsuarioAtual usuario)
        {
            if (Usuario != null || usuario == null)
                throw new ArgumentException("Não é possível atualizar o usuário atual");

            Usuario = usuario;
        }
    }
}
