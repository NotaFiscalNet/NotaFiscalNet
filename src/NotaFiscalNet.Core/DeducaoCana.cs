using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System.Xml;

namespace NotaFiscalNet.Core
{
    public sealed class DeducaoCana : ISerializavel, IModificavel
    {
        private string _descricao;
        private decimal _valor;

        [NFeField(ID = "ZC11", FieldName = "xDed", DataType = "TString")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = ValidationUtil.TruncateString(value, 60); }
        }

        [NFeField(ID = "ZC12", FieldName = "vDed", DataType = "TDec_1302")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal Valor
        {
            get { return _valor; }
            set { _valor = ValidationUtil.ValidateTDec_1302(value, "Valor"); }
        }

        public bool Modificado => !string.IsNullOrEmpty(Descricao) ||
                                  Valor != 0;

        public void Serializar(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("deduc");

            writer.WriteElementString("xDed", SerializationUtil.ToTString(Descricao, 60));
            writer.WriteElementString("vDed", SerializationUtil.ToTDec_1302(Valor));

            writer.WriteEndElement();
        }
    }
}