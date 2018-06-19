using System.Linq;
using NotaFiscalNet.Core.Interfaces;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Duplicatas de Cobrança da Nota Fiscal Eletrônica
    /// </summary>
    public sealed class DuplicataCollection : BaseCollection<Duplicata>, ISerializavel, IModificavel
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado => this.Any(item => item.Modificado);
        
	    public void Serializar(XmlWriter writer, INFe nfe)
        {
            foreach (var duplicata in this)
            {
                if (duplicata.Modificado)
                    duplicata.Serializar(writer, nfe);
            }
        }
    }
}