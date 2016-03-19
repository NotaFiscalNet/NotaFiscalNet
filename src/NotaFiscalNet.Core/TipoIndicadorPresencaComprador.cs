namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Indicador de presença do comprador no estabelecimento comercial no momento da operação.
    /// </summary>

    public enum TipoIndicadorPresencaComprador
    {
        /// <summary>
        /// 0 - Não se aplica (ex.: Nota Fiscal complementar ou de ajuste).
        /// </summary>
        NaoSeAplica = 0,

        /// <summary>
        /// 1 - Operação presencial.
        /// </summary>
        Presencial = 1,

        /// <summary>
        /// 2 - Operação não presencial, internet.
        /// </summary>
        Internet = 2,

        /// <summary>
        /// 3 - Operação não presencial, teleatendimento.
        /// </summary>
        TeleAtendimento = 3,

        /// <summary>
        /// 4 - NFC-e em operação com entrega em domicílio.
        /// </summary>
        NFCeEntregaEmDomicilio = 4,

        /// <summary>
        /// 9 - Não presencial, outros.
        /// </summary>
        Outros = 9
    }
}