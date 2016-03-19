namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Lista dos Códigos de Situação Tributária (CST) do IPI.
    /// </summary>

    public enum SituacaoTributariaIPI
    {
        /* ATENÇÃO: Os valores do enum devem ser serializados no formato 00 (duas casas, incluindo zeros não significativos) */

        /// <summary>
        /// Não Informado
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// 00 - Entrada com Recuperação de Crédito.
        /// </summary>
        EntradaComRecuperacaoCredito = 0,

        /// <summary>
        /// 01 - Entrada Tributada com Alíquota Zero.
        /// </summary>
        EntradaTributadaAliqZero = 1,

        /// <summary>
        /// 02 - Entrada Isenta.
        /// </summary>
        EntradaIsenta = 2,

        /// <summary>
        /// 03 - Entrada Não-Tributada.
        /// </summary>
        EntradaNaoTributada = 3,

        /// <summary>
        /// 04 - Entrada Imune.
        /// </summary>
        EntradaImune = 4,

        /// <summary>
        /// 05 - Entrada com suspensão
        /// </summary>
        EntradaComSuspensao = 5,

        /// <summary>
        /// 49 - Outras entradas
        /// </summary>
        OutrasEntradas = 49,

        /// <summary>
        /// 50 - Saída Tributada
        /// </summary>
        SaidaTributada = 50,

        /// <summary>
        /// 51 - Saída tributada com alíquota zero.
        /// </summary>
        SaidaTributadaAliqZero = 51,

        /// <summary>
        /// 52 - Saída Isenta.
        /// </summary>
        SaidaIsenta = 52,

        /// <summary>
        /// 53 - Saída Não-Tributada.
        /// </summary>
        SaidaNaoTributada = 53,

        /// <summary>
        /// 54 - Saída Imune.
        /// </summary>
        SaidaImune = 54,

        /// <summary>
        /// 55 - Saída com suspensão.
        /// </summary>
        SaidaComSuspensao = 55,

        /// <summary>
        /// 99 - Outras Saídas
        /// </summary>
        OutrasSaidas = 99
    }
}