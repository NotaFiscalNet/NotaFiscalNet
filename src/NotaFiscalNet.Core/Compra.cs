using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de Compras (Nota de Empenho, Pedido e Contrato) da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class Compra : ISerializavel, IModificavel
    {
        private string _contrato;
        private string _notaEmpenho;
        private string _pedido;

        /// <summary>
        /// [xNEmp] Retorna ou define a Identificação da Nota de Empenho, quando se tratar de compras públicas.
        /// </summary>
        [ValidateField(1, true)]
        public string NotaEmpenho
        {
            get { return _notaEmpenho; }
            set { _notaEmpenho = ValidationUtil.TruncateString(value, 22); }
        }

        /// <summary>
        /// [xPed] Retorna ou define o Pedido de Compra
        /// </summary>
        [ValidateField(2, true)]
        public string Pedido
        {
            get { return _pedido; }
            set { _pedido = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// [xCont] Retorna ou define Contrato de Compra
        /// </summary>
        [ValidateField(3, true)]
        public string Contrato
        {
            get { return _contrato; }
            set { _contrato = ValidationUtil.TruncateString(value, 60); }
        }

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
                writer.WriteElementString("xNEmp", SerializationUtil.ToToken(NotaEmpenho, 17));
            if (!string.IsNullOrEmpty(Pedido))
                writer.WriteElementString("xPed", SerializationUtil.ToToken(Pedido, 60));
            if (!string.IsNullOrEmpty(Contrato))
                writer.WriteElementString("xCont", SerializationUtil.ToToken(Contrato, 60));

            writer.WriteEndElement();
        }
    }
}