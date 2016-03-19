using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Adições na Declaração de Importação do Produto
    /// </summary>
    public sealed class DeclaracaoImportacaoAdicaoCollection : BaseCollection<DeclaracaoImportacaoAdicao>,
        ISerializavel
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool IsDirty
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.IsDirty)
                        return true;
                }
                return false;
            }
        }

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            foreach (var declaracao in this)
            {
                if (declaracao.IsDirty)
                    ((ISerializavel)declaracao).Serializar(writer, nfe);
            }
        }
    }
}