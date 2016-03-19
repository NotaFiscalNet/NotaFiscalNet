using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o fornecimento diário de cana.
    /// </summary>
    public class FornecimentoDiarioCana : ISerializavel, IModificavel
    {
        private int _dia;
        private decimal _quantidade;

        /// <summary>
        /// Retorna ou define o dia (número) em foi feito o fornecimento.
        /// </summary>
        [NFeField(ID = "ZC05", FieldName = "dia", DataType = "xs:string", Pattern = "[1-9]|[1][0-9]|[2][0-9]|[3][0-1]?")
        ]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public int Dia
        {
            get { return _dia; }
            set
            {
                ValidationUtil.ValidateRange(value, 1, 31, "Dia");
                _dia = value;
            }
        }

        /// <summary>
        /// Retorna ou define a quantidade (em quilogramas - peso líquido).
        /// </summary>
        public decimal Quantidade
        {
            get { return _quantidade; }
            set
            {
                ValidationUtil.ValidateTDec_1110(value, "Quantidade");
                _quantidade = value;
            }
        }

        public bool Modificado
        {
            get { return Dia != 0 && Quantidade > 0; }
        }

        public void Serializar(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("forDia"); // <forDia>
            writer.WriteAttributeString("dia", Dia.ToString());

            writer.WriteElementString("qtde", SerializationUtil.ToTDec_1110(Quantidade));

            writer.WriteEndElement(); // </forDia>
        }
    }
}