using NotaFiscalNet.Core.Interfaces;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa os Dados de Cobrança da Nota Fiscal Eletrônica
    /// </summary>
    public sealed class CobrancaNFe : ISerializavel, IModificavel
    {
        public void Serializar(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("cobr"); // Elemento 'cobr'

            if (Fatura.Modificado)
                ((ISerializavel)Fatura).Serializar(writer, nfe);

            if (Duplicatas.Modificado)
                ((ISerializavel)Duplicatas).Serializar(writer, nfe);

            writer.WriteEndElement(); // Elemento 'cobr'
        }

        /// <summary>
        /// Retorna os Dados da Fatura
        /// </summary>
        [NFeField(ID = "Y02", FieldName = "fat")]
        public Fatura Fatura { get; } = new Fatura();

        /// <summary>
        /// Retorna as Duplicatas de Cobrança
        /// </summary>
        [NFeField(ID = "Y07", FieldName = "dup")]
        public DuplicataCollection Duplicatas { get; } = new DuplicataCollection();

        /// <summary>
        /// Retorna se a Classe foi modificada.
        /// </summary>
        public bool Modificado => Fatura.Modificado ||
                                  Duplicatas.Modificado;

        /// <summary>
        /// Retorna o valor indicando se a Nota Fiscal Eletrônica está em modo somente-leitura.
        /// </summary>
        /// <remarks>
        /// A Nota Fiscal Eletrônica estará em modo somente-leitura quando for instanciada a partir
        /// de um arquivo assinado digitalmente.
        /// </remarks>
        public bool IsReadOnly { get; } = false;
    }
}