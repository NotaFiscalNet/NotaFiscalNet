using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Indicador de Forma de Pagamento.
    /// </summary>
    
    
    public enum IndicadorFormaPagamento
    {
        /// <summary>
        /// 0 - Pagamento à vista.
        /// </summary>
        AVista = 0,
        /// <summary>
        /// 1 - Pagamento à prazo.
        /// </summary>
        APrazo = 1,
        /// <summary>
        /// 2- Outras formas de pagamento.
        /// </summary>
        Outro = 2
    }
}
