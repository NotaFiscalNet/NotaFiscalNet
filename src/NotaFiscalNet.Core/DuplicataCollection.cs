using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Duplicatas de Cobrança da Nota Fiscal Eletrônica
    /// </summary>
    public sealed class DuplicataCollection : BaseCollection<Duplicata>, ISerializavel
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.IsDirty)
                        return true;
                }
                return false;
            }
        }

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            foreach (var duplicata in this)
            {
                if (duplicata.IsDirty)
                    ((ISerializavel)duplicata).Serializar(writer, nfe);
            }
        }
    }
}