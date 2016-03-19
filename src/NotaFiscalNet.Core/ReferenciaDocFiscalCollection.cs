using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de referências à Documentos Fiscais.
    /// </summary>
    
    
    
    public sealed class ReferenciaDocFiscalCollection : BaseCollection<ReferenciaDocFiscal>, ISerializavel
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


        #region ISerializavel Members

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (ReferenciaDocFiscal referencia in this)
            {
                if (referencia.IsDirty)
                    ((ISerializavel)referencia).Serializar(writer, nfe);
            }
        }

        #endregion
    }
}
