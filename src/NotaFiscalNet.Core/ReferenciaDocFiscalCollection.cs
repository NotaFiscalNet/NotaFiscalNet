using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma lista de referências à Documentos Fiscais.
    /// </summary>
    public sealed class ReferenciaDocFiscalCollection : BaseCollection<ReferenciaDocFiscal>, ISerializavel, IModificavel
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
        {
            get
            {
                foreach (ReferenciaDocFiscal item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (ReferenciaDocFiscal referencia in this)
            {
                if (referencia.Modificado)
                    ((ISerializavel)referencia).Serializar(writer, nfe);
            }
        }
    }
}