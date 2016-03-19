using NotaFiscalNet.Core.Interfaces;
using System.Linq;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a coleção de Volumes de uma Carga.
    /// </summary>
    public sealed class VolumeCargaCollection : BaseCollection<VolumeCarga>, ISerializavel, IModificavel
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado => this.Any(item => item.Modificado);

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            foreach (var volume in this.Where(volume => volume.Modificado))
            {
                ((ISerializavel)volume).Serializar(writer, nfe);
            }
        }
    }
}