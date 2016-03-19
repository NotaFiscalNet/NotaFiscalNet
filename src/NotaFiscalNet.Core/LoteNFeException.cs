using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa um erro referente a um Lote de Notas Fiscais Eletrônicas.
    /// </summary>

    public class LoteNFeException : ApplicationException
    {
        internal LoteNFeException(string message)
            : base(message)
        {
        }

        internal LoteNFeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}