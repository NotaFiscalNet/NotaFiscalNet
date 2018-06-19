using System.Linq;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Processos Referenciados
    /// </summary>
    public sealed class ProcessoCollection : BaseCollection<Processo>, ISerializavel, IModificavel
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado => this.Any(item => item.Modificado);

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            foreach (var processo in this)
            {
                if (processo.Modificado)
                    ((ISerializavel)processo).Serializar(writer, nfe);
            }
        }
    }
}