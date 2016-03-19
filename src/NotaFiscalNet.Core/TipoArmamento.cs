using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Especifica o Tipo de Armamento.
    /// </summary>
    
    
    public enum TipoArmamento
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,
        /// <summary>
        /// Armamento de Uso Permitido
        /// </summary>
        UsoPermitido = 0,
        /// <summary>
        /// Armamento de Uso Restrito
        /// </summary>
        UsoRestrito = 1
    }
}
