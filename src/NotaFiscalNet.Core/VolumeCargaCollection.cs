using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a coleção de Volumes de uma Carga.
    /// </summary>
    public sealed class VolumeCargaCollection : BaseCollection<VolumeCarga>,  ISerializavel, IModificavel
    {

        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
        {
            get
            {
                foreach (VolumeCarga item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        #region ISerializavel Members

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (VolumeCarga volume in this)
            {
                if (volume.Modificado)
                    ((ISerializavel)volume).Serializar(writer, nfe);
            }
        }

        #endregion

       
    }
}
