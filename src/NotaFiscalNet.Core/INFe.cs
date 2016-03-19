namespace NotaFiscalNet.Core
{
    public interface INFe
    {
        int DigitoVerificadorChaveAcesso { get; }
        IdentificacaoDocumentoFiscal Identificacao { get; }
    }
}