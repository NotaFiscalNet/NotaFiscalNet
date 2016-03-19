namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a Finalizada de Emissão da NF-e.
    /// </summary>

    public enum TipoFinalidade
    {
        /// <summary>
        /// 1 - NF-e Normal.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 2 - NF-e Complementar.
        /// </summary>
        Complementar = 2,

        /// <summary>
        /// 3 - NF-e de Ajuste.
        /// </summary>
        Ajuste = 3,

        /// <summary>
        /// 4 - Devolução/Retorno
        /// </summary>
        Devolucao = 4
    }
}