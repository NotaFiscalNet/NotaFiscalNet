using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Identificador de Local de Destino da Opeeração.
    /// </summary>
    
    
    public enum TipoIdentificadorLocalDestinoOperacao
    {
        /// <summary>
        /// 1 - Operação Interna.
        /// </summary>
        Interna = 1,
        
        /// <summary>
        /// 2 - Operação Interestadual.
        /// </summary>
        Interestadual = 2,

        /// <summary>
        /// 3 - Operação com Exterior
        /// </summary>
        Exterior = 3
    }
}
