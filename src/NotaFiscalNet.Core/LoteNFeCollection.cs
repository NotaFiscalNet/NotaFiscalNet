using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de Lotes de Notas Fiscais Eletrônicas.
    /// </summary>
    
    
    
    public sealed class LoteNFeCollection : BaseCollection<LoteNFe>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="LoteNFeCollection"/>.
        /// </summary>
        public LoteNFeCollection()
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="LoteNFeCollection"/> com base em uma
        /// lista de Lotes já existente.
        /// </summary>
        /// <param name="collection">Lista de lotes pré-existente.</param>
        public LoteNFeCollection(IEnumerable<LoteNFe> collection)
            : base(collection)
        {
        }
        
    }
}
