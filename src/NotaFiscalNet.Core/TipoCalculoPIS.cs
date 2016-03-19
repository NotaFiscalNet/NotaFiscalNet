namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tipo de Cálculo do PIS
    /// </summary>

    public enum TipoCalculoPIS
    {
        /// <summary>
        /// Alíquota em Percentual, Base de Cálculo em Valor.
        /// </summary>
        PercentualValor = 0,

        /// <summary>
        /// Alíquota em Valor, Base de Cálculo em Quantidade.
        /// </summary>
        ValorQuantidade = 1
    }
}