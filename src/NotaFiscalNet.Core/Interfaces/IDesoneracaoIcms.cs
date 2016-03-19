namespace NotaFiscalNet.Core.Interfaces
{
    public interface IDesoneracaoIcms
    {
        decimal? ValorIcmsDesoneracao { get; set; }
        MotivoDesoneracaoCondicionalICMS? MotivoDesoneracaoIcms { get; set; }
    }
}