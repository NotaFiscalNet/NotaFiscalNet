using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de referências à Documentos Fiscais.
    /// </summary>
    
    
    
    public sealed class ReferenciaDocFiscalCollection : BaseCollection<ReferenciaDocFiscal>, INFeSerializable
    {

     /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (ReferenciaDocFiscal item in this)
                {
                    if (item.IsDirty)
                        return true;
                }
                return false;
            }
        }


        #region INFeSerializable Members

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (ReferenciaDocFiscal referencia in this)
            {
                if (referencia.IsDirty)
                    ((INFeSerializable)referencia).Serialize(writer, nfe);
            }
        }

        #endregion
    }
}
