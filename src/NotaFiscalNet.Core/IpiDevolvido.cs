using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core
{
    
    
    
    public class IpiDevolvido : INFeSerializable
    {
        private decimal _valorIpiDevolvido;

        /// <summary>
        /// [vIPIDevol] Retorna ou define o valor do IPI devolvido.
        /// </summary>
        /// 
        [NFeField(FieldName = "vIPIDevol", DataType = "TDec_1302", ID = "U61")]
        public decimal ValorIpiDevolvido
        {
            get { return _valorIpiDevolvido; }
            set
            {
                _valorIpiDevolvido = ValidationUtil.ValidateTDec_0302Max100(value, "ValorIpiDevolvido"); ;
            }
        }

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("IPI");
            writer.WriteStartElement("vIPIDevol", ValorIpiDevolvido.ToTDec_1302());
            writer.WriteEndElement();
        }
    }
}