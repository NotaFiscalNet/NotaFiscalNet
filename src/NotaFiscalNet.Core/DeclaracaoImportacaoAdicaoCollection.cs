using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Adições na Declaração de Importação do Produto
    /// </summary>
    public sealed class DeclaracaoImportacaoAdicaoCollection : BaseCollection<DeclaracaoImportacaoAdicao>,
        INFeSerializable
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

        void INFeSerializable.Serialize(XmlWriter writer, NFe nfe)
        {
            foreach (var declaracao in this)
            {
                if (declaracao.IsDirty)
                    ((INFeSerializable)declaracao).Serialize(writer, nfe);
            }
        }
    }
}