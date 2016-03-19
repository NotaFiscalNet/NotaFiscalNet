using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Indicador de Tipo de Cálculo do IPI.<br/>
    /// </summary>
    
    
    public enum TipoCalculoIPI
    {
        /// <summary>
        /// Caso o cálculo do IPI seja por Alíquota.
        /// </summary>
        Percentual,
        /// <summary>
        /// Caso o cálculo do IPI seja Valor por Unidade.
        /// </summary>
        Valor
    }
}
