using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de strings.
    /// </summary>
    
    
    
    public sealed class StringCollection : BaseCollection<string>
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (string item in this)
                {
                    if (!string.IsNullOrEmpty(item))
                        return true;
                }
                return false;
            }
        }
    }
}
