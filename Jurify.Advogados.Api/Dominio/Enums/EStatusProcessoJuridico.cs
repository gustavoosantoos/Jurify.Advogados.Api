namespace Jurify.Advogados.Api.Dominio.Enums
{
    public enum EStatusProcessoJuridico
    {
        Aberto,
        AguardandoDistribuicao,
        AguardandoCitacao,
        AguardandoCumprimentoDaCartaPrecatoria,
        AguardandoJulgamentoEmbargoDeTerceiros,
        AguardandoSentenca,
        AguardandoTransitoEmJulgado,
        AguardandoIntimacaoDaSentenca,
        AguardandoJulgamentoDeApelacao,
        AguardandoJulgamentoDeRecursoEspecial,
        CumprimentoDeSentenca,
        AguardandoBaixaDoDistribuidor,
        Finalizado
    }
}
