using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o imposto ICMS CST 00 (Tributação Integral).
    /// </summary>
    public class Icms00 : IcmsTributacaoNormal
    {
        private decimal _aliquota;
        private ModalidadeBaseCalculoIcms _modalidadeBaseCalculo;
        private decimal _valor;
        private decimal _valorBaseCalculo;

        public Icms00()
        {
            CST = SituacaoTributariaICMS.Cst00;
        }

        /// <summary>
        /// [modBC] Retorna ou define a Modalidade da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(FieldName = "modBC")]
        public ModalidadeBaseCalculoIcms ModalidadeBaseCalculo
        {
            get => _modalidadeBaseCalculo;
	        set => _modalidadeBaseCalculo = ValidationUtil.ValidateEnum(value, "ModalidadeBaseCalculo");
        }

        /// <summary>
        /// [vBC] Retorna ou define o Valor da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(FieldName = "vBC", DataType = "TDec_1302")]
        public decimal ValorBaseCalculo
        {
            get => _valorBaseCalculo;
	        set => _valorBaseCalculo = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculo");
        }

        /// <summary>
        /// [pICMS] Retorna ou define a Alíquota do ICMS (não centesimal).
        /// </summary>
        [NFeField(FieldName = "pICMS", DataType = "TDec_0302")]
        public decimal Aliquota
        {
            get => _aliquota;
	        set => _aliquota = ValidationUtil.ValidateTDec_0302(value, "Aliquota");
        }

        /// <summary>
        /// [vICMS] Retorna o Valor (calculado) do ICMS.
        /// </summary>
        [NFeField(FieldName = "vICMS", DataType = "TDec_1302")]
        public decimal Valor
        {
            get => _valor;
	        set => _valor = ValidationUtil.ValidateTDec_0302(value, "Valor");
        }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS00");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));
            writer.WriteElementString("modBC", ModalidadeBaseCalculo.GetEnumValue());
            writer.WriteElementString("vBC", ValorBaseCalculo.ToTDec_1302());
            writer.WriteElementString("pICMS", Aliquota.ToTDec_0302());
            writer.WriteElementString("vICMS", Valor.ToTDec_1302());

            writer.WriteEndElement();
        }
    }
}