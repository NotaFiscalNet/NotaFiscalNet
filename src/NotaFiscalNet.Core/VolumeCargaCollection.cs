using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a coleção de Volumes de uma Carga.
    /// </summary>
    
    
    
    public sealed class VolumeCargaCollection : BaseCollection<VolumeCarga>,  INFeSerializable
    {

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (VolumeCarga item in this)
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
            foreach (VolumeCarga volume in this)
            {
                if (volume.IsDirty)
                    ((INFeSerializable)volume).Serialize(writer, nfe);
            }
        }

        #endregion

       
    }
}
