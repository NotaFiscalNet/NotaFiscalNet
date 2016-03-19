namespace NotaFiscalNet.Core.Schemas
{
    public interface IPacoteLiberacaoNFe
    {
        string VersaoLayout { get; }

        string PathNFe { get; }

        string PathNFeNoSig { get; }

        string PathEnviNFe { get; }

        string PathEnvEventoCancelamento { get; }

        string PathEnvEventoCancelamentoNoSig { get; }

        string PathInutNFe { get; }

        string PathInutNFeNoSig { get; }
    }
}