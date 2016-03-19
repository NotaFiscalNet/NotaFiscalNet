namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tipo de emissão da Nota Fiscal Eletrônica.
    /// </summary>

    public enum TipoEmissaoNFe
    {
        /// <summary>
        /// 1 - Emissão normal com transmissão on-line da NF-e para a SEFAZ de origem.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 2 - Emissão em contingência FS-IA, com impressão do DANFE em formulário de segurança e
        /// posterior transmissão da NF-e para a SEFAZ de origem.
        /// </summary>
        Contingencia = 2,

        /// <summary>
        /// 3 - Emissão em contingência no SCAN (Sistema de Contingência do Ambiente Nacional).
        /// </summary>
        SCAN = 3,

        /// <summary>
        /// 4 - Emissão em contingência com DEPC (Declaração Prévia de Emissão em Contingência).
        /// </summary>
        DEPC = 4,

        /// <summary>
        /// 5 - Emissão em contingência no FSDA.
        /// </summary>
        FSDA = 5,

        /// <summary>
        /// 6 - Contingência SVC-AN (SEFAZ Virtual de Contingência do AN).
        /// </summary>
        SVC_AN = 6,

        /// <summary>
        /// 7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS).
        /// </summary>
        SVC_RS = 7,

        /// <summary>
        /// 9 - Contingência off-line da NFC-e (as demais opções de contingência são válidas também
        /// para a NFC-e).
        /// Nota: As opções de contingência 3, 4, 6 e 7 (SCAn, DPEC e SVC) não estão disponíveis no
        ///       momento atual.
        /// </summary>
        ContingenciaOffLineNFCe = 9
    }
}