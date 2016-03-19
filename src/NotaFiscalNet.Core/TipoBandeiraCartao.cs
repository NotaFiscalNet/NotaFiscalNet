using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    
    
    public enum TipoBandeiraCartao
    {
        /// <summary>
        /// 01 - Visa.
        /// </summary>
        Visa = 1,
        /// <summary>
        /// 02 - Mastercard.
        /// </summary>
        Mastercard = 2,
        /// <summary>
        /// 03 - American Express.
        /// </summary>
        AmericanExpress = 3,
        /// <summary>
        /// 04 - Sorocred.
        /// </summary>
        Sorocred = 4,
        /// <summary>
        /// 99 - Outros.
        /// </summary>
        Outros = 99
    }
}