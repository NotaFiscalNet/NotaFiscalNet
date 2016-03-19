using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core
{
    public class ImpostoDevolvido : ISerializavel
    {
        /// <summary>
        /// [pDevol] Retorna ou define o Percentual da mercadoria devolvida.
        /// </summary>
        [NFeField(FieldName = "pDevol", DataType = "TDec_0302Max100", ID = "U51")]
        public decimal PercentualMercadoriaDevolvida { get; set; }

        /// <summary>
        /// [IPI] Retorna ou define as informações do IPI devolvido.
        /// </summary>
        [NFeField(FieldName = "IPI", ID = "U60")]
        public IpiDevolvido IPI { get; private set; }

        public ImpostoDevolvido()
        {
            IPI = new IpiDevolvido();
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("impostoDevol");

            writer.WriteElementString("pDevol", PercentualMercadoriaDevolvida.ToTDec_0302());
            ((ISerializavel)IPI).Serializar(writer, nfe);

            writer.WriteEndElement();
        }
    }
}