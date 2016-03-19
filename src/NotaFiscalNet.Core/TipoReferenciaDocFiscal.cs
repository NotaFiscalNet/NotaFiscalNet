namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Indicam o tipo da referência de nota fiscal utilizada.
    /// </summary>

    public enum TipoReferenciaDocFiscal
    {
        /// <summary>
        /// Indica que a referência é uma NF-e.
        /// </summary>
        NFe = 0,

        /// <summary>
        /// Indica que a referência é uma NF modelo 1/1A.
        /// </summary>
        NF = 1,

        /// <summary>
        /// Indica que a referência é uma NF de produtor.
        /// </summary>
        NFProdutor = 2,

        /// <summary>
        /// Indica que a referência é uma CT-e.
        /// </summary>
        CTe = 3,

        /// <summary>
        /// Indica que a referência é um ECF.
        /// </summary>
        ECF = 4
    }
}