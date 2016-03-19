namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tipo de Processo utilizado para Emissão da NF-e.
    /// </summary>

    public enum TipoProcessoEmissaoNFe
    {
        /// <summary>
        /// 0 - Emissão de NF-e com aplicativo do contribuinte.
        /// </summary>
        AplicativoContribuinte = 0,

        /// <summary>
        /// 1 - Emissão de NF-e avulsa pelo Fisco.
        /// </summary>
        AvulsaFisco = 1,

        /// <summary>
        /// 2 - Emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do
        /// site do Fisco.
        /// </summary>
        AvulsaContribuinteSiteFisco = 2,

        /// <summary>
        /// 3 - Emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.
        /// </summary>
        AplicativoFisco = 3,
    }
}