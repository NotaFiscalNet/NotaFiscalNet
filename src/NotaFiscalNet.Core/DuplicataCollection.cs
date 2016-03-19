using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Duplicatas de Cobrança da Nota Fiscal Eletrônica
    /// </summary>
    public sealed class DuplicataCollection : BaseCollection<Duplicata>, INFeSerializable
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

        void INFeSerializable.Serialize(XmlWriter writer, NFe nfe)
        {
            foreach (var duplicata in this)
            {
                if (duplicata.IsDirty)
                    ((INFeSerializable)duplicata).Serialize(writer, nfe);
            }
        }
    }
}