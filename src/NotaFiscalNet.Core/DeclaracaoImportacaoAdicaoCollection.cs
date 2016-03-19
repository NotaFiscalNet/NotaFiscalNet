using NotaFiscalNet.Core.Interfaces;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Coleção de Adições na Declaração de Importação do Produto
    /// </summary>
    public sealed class DeclaracaoImportacaoAdicaoCollection : BaseCollection<DeclaracaoImportacaoAdicao>,
        ISerializavel, IModificavel
    {
        /// <summary>
        /// Retorna se existe alguma instancia da classe modificada na coleção
        /// </summary>
        public bool Modificado
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.Modificado)
                        return true;
                }
                return false;
            }
        }

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            foreach (var declaracao in this)
            {
                if (declaracao.Modificado)
                    ((ISerializavel)declaracao).Serializar(writer, nfe);
            }
        }
    }
}