namespace NotaFiscalNet.Core
{
    public interface IDesoneracaoIcms
    {
        decimal? ValorIcmsDesoneracao { get; set; }
        MotivoDesoneracaoCondicionalICMS? MotivoDesoneracaoIcms { get; set; }
    }
}