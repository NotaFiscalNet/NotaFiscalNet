namespace NotaFiscalNet.Core.Interfaces
{
    public interface IPossuiDocumentoIdentificador
    {
        string CNPJ { get; set; }
        string CPF { get; set; }
    }
}