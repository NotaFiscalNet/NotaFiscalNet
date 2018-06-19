using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de Compras (Nota de Empenho, Pedido e Contrato) da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class Compra : ISerializavel, IModificavel
    {
        /// <summary>
        /// [xNEmp] Retorna ou define a Identificação da Nota de Empenho, quando se tratar de compras públicas.
        /// Tamanho máximo de 22 caracteres
        /// </summary>
        public string NotaEmpenho { get; set; }

        /// <summary>
        /// [xPed] Retorna ou define o Pedido de Compra
        /// Tamanho máximo de 60 caracteres
        /// </summary>
        public string Pedido { get; set; }

        /// <summary>
        /// [xCont] Retorna ou define Contrato de Compra
        /// Tamanho máximo de 60 caracteres
        /// </summary>
        public string Contrato { get; set; }

        /// <summary>
        /// Retorna se a classe foi modificada.
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(Contrato) ||
                                  !string.IsNullOrEmpty(NotaEmpenho) ||
                                  !string.IsNullOrEmpty(Pedido);

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("compra");

            if (!string.IsNullOrEmpty(NotaEmpenho))
                writer.WriteElementString("xNEmp", SerializationUtil.ToToken(NotaEmpenho, 22));
            if (!string.IsNullOrEmpty(Pedido))
                writer.WriteElementString("xPed", SerializationUtil.ToToken(Pedido, 60));
            if (!string.IsNullOrEmpty(Contrato))
                writer.WriteElementString("xCont", SerializationUtil.ToToken(Contrato, 60));

            writer.WriteEndElement();
        }
    }
}