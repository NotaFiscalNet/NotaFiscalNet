using System.Dynamic;
using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de strings.
    /// </summary>
    public sealed class StringCollection : BaseCollection<string>, IModificavel
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
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
