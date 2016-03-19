using NotaFiscalNet.Core.Utils;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de Compras (Nota de Empenho, Pedido e Contrato) da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class Compra : ISerializavel
    {
        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("compra"); // Elemento 'compra'

            if (!string.IsNullOrEmpty(NotaEmpenho))
                writer.WriteElementString("xNEmp", SerializationUtil.ToToken(NotaEmpenho, 17));
            if (!string.IsNullOrEmpty(Pedido))
                writer.WriteElementString("xPed", SerializationUtil.ToToken(Pedido, 60));
            if (!string.IsNullOrEmpty(Contrato))
                writer.WriteElementString("xCont", SerializationUtil.ToToken(Contrato, 60));

            writer.WriteEndElement();
        }

        private string _notaEmpenho = string.Empty;
        private string _pedido = string.Empty;
        private string _contrato = string.Empty;

        /// <summary>
        /// [xNEmp] Retorna ou define a Identificação da Nota de Empenho, quando se tratar de compras públicas.
        /// </summary>
        [NFeField(ID = "ZB02", FieldName = "xNEmp", DataType = "token", MinLength = 1, MaxLength = 17)]
        public string NotaEmpenho
        {
            get { return _notaEmpenho; }
            set { _notaEmpenho = ValidationUtil.TruncateString(value, 17); }
        }

        /// <summary>
        /// [xPed] Retorna ou define o Pedido de Compra
        /// </summary>
        [NFeField(ID = "ZB03", FieldName = "xPed", DataType = "token", MinLength = 1, MaxLength = 60)]
        public string Pedido
        {
            get { return _pedido; }
            set { _pedido = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// [xCont] Retorna ou define Contrato de Compra
        /// </summary>
        [NFeField(ID = "ZB04", FieldName = "xCont", DataType = "token", MinLength = 1, MaxLength = 60)]
        public string Contrato
        {
            get { return _contrato; }
            set { _contrato = ValidationUtil.TruncateString(value, 60); }
        }

        /// <summary>
        /// Retorna se a classe foi modificada.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    !string.IsNullOrEmpty(Contrato) ||
                    !string.IsNullOrEmpty(NotaEmpenho) ||
                    !string.IsNullOrEmpty(Pedido);
            }
        }
    }
}