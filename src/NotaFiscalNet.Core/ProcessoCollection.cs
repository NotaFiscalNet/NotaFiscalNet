using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Processos Referenciados
    /// </summary>
    
    
    public sealed class ProcessoCollection : BaseCollection<Processo>, INFeSerializable
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

        #region INFeSerializable Members

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (Processo processo in this)
            {
                if (processo.IsDirty)
                    ((INFeSerializable)processo).Serialize(writer, nfe);
            }
        }

        #endregion
        
    }
}
