namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Indica a modalidade de frete do Nota Fiscal Eletrônica.
    /// </summary>

    public enum TipoModalidadeFrete
    {
        /// <summary>
        /// 0 - Por conta do Emitente.
        /// </summary>
        Emitente = 0,

        /// <summary>
        /// 1 - Por conta do Destinatário.
        /// </summary>
        Destinatario = 1,

        /// <summary>
        /// 2 - Por conta do Terceiros.
        /// </summary>
        Terceiros = 2,

        /// <summary>
        /// 9 - Sem Frete.
        /// </summary>
        SemFrete = 9
    }
}