using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tipo de Cálculo do COFINS
    /// </summary>
    
    
    public enum TipoCalculoCOFINS
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
