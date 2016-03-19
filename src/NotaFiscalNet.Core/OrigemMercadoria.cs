namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Especifica a Origem da Mercadoria
    /// </summary>

    public enum OrigemMercadoria
    {
        /// <summary>
        /// Não Especificado (-1).
        /// </summary>
        NaoEspecificado = -1,

        /// <summary>
        /// Nacional (0).
        /// </summary>
        Nacional = 0,

        /// <summary>
        /// Estrangeira com Importação Direta (1).
        /// </summary>
        EstrangeiraDireta = 1,

        /// <summary>
        /// Estrangeira adquirida no Mercado Interno (2).
        /// </summary>
        EstrangeiraIndireta = 2
    }
}