namespace NotaFiscalNet.Core
{
    
    /// <summary>
    /// Representa o formato de impressão da DANFE.
    /// </summary>
    public enum TipoFormatoImpressaoDanfe
    {
        /// <summary>
        /// 0 - Sem geração de DANFE.
        /// </summary>
        SemImpressao = 0,

        /// <summary>
        /// 1 - DANFE Normal (Impressão no formato Retrato).
        /// </summary>
        Retrato = 1,

        /// <summary>
        /// 2 - DANFE Normal (Impressão no formato Paisagem).
        /// </summary>
        Paisagem = 2,

        /// <summary>
        /// 3 - DANFE Simplificado.
        /// </summary>
        Simplificado = 3,

        /// <summary>
        /// 4 - DANFE NFC-e.
        /// </summary>
        Nfce = 4,

        /// <summary>
        /// 5 - DANFE NFC-e em mensagem eletronica.
        /// </summary>
        NfceMensagemEletronica = 5
    }
}