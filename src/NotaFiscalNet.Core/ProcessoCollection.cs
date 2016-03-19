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
        public bool Modificado
        {
            get
            {
                foreach (Processo item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            foreach (Processo processo in this)
            {
                if (processo.Modificado)
                    ((ISerializavel)processo).Serializar(writer, nfe);
            }
        }
    }
}