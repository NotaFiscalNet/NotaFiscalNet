using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Processos Referenciados
    /// </summary>
    
    
    public sealed class ProcessoCollection : BaseCollection<Processo>, ISerializavel
    {

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (Processo item in this)
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
            foreach (Processo processo in this)
            {
                if (processo.IsDirty)
                    ((ISerializavel)processo).Serializar(writer, nfe);
            }
        }

        #endregion
        
    }
}
