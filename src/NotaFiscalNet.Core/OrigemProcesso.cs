using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Lista de Origens do Processo Referenciado.
    /// </summary>
    
    
    public enum OrigemProcesso
    {
        /// <summary>
        /// Não Especificado
        /// </summary>
        NaoEspecificado = -1,
        /// <summary>
        /// Origem na SEFAZ
        /// </summary>
        SEFAZ = 0,
        /// <summary>
        /// Origem na Justiça Federal
        /// </summary>
        JusticaFederal = 1,
        /// <summary>
        /// Origem na Justiça Estadual
        /// </summary>
        JusticaEstadual = 2,
        /// <summary>
        /// Origem na Secex/RFB
        /// </summary>
        SecexRFB = 3,
        /// <summary>
        /// Outras Origens
        /// </summary>
        Outros = 9
    }
}
