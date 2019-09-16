using Jurify.Advogados.Api.Dominio.Base;
using Jurify.Advogados.Api.Dominio.ObjetosDeValor;
using System;

namespace Jurify.Advogados.Api.Dominio.Entidades
{
    public class CompromissoAgenda : Entidade
    {
        public Guid? CodigoAdvogado { get; private set; }
        public Guid? CodigoCliente { get; private set; }

        public DescricaoCurta Titulo { get; private set; }
        public Descricao Descricao { get; private set; }
        public HorarioCompromisso Horario { get; private set; }

        public Cliente Cliente { get; private set; }

        protected CompromissoAgenda() { }

        public CompromissoAgenda(
            DescricaoCurta titulo,
            Descricao descricao,
            HorarioCompromisso horario,
            Guid? codigoCliente,
            Guid? codigoAdvogado)
        {
            Titulo = titulo;
            Descricao = descricao;
            Horario = horario;
            CodigoCliente = codigoCliente;
            CodigoAdvogado = codigoAdvogado;

            Validar();
        }

        public void AtualizarTitulo(string novoTitulo)
        {
            Titulo = new DescricaoCurta(novoTitulo);
            AddNotifications(Titulo);
        }

        public void AtualizarDescricao(string novaDescricao)
        {
            Descricao = new Descricao(novaDescricao);
            AddNotifications(Descricao);
        }

        public void AtualizarHorario(DateTime inicio, DateTime? final)
        {
            Horario = new HorarioCompromisso(inicio, final);
            AddNotifications(Horario);
        }

        public bool PodeNotificarCliente()
        {
            return Cliente != null && Cliente.Email != null;
        }

        protected override void Validar()
        {
            AddNotifications(Titulo, Descricao, Horario);
        }
    }
}
