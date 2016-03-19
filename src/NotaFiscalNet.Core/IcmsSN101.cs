using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=101.
    /// </summary>
    public class IcmsSN101 : IcmsSimplesNacional
    {
        private decimal _aliquotaCredito;
        private decimal _valorCredito;

        public IcmsSN101()
        {
            CSOSN = CSOSN.SN101;
        }

        /// <summary>
        /// [pCredSN] Retorna ou define a Alíquota aplicável de cálculo de crédito (Simples Nacional).
        /// </summary>
        [NFeField(FieldName = "pCredSN", DataType = "TDec_0302")]
        public decimal AliquotaCredito
        {
            get { return _aliquotaCredito; }
            set { _aliquotaCredito = ValidationUtil.ValidateTDec_0302(value, "AliquotaCredito"); }
        }

        /// <summary>
        /// [vCredICMSSN] Retorna ou define ao Valor do crédito do ICMS que pode ser aproveitado nos
        /// termos do art. 23 da LC 123 (Simples Nacional).
        /// </summary>
        [NFeField(FieldName = "vCredICMSSN", DataType = "TDec_1302")]
        public decimal ValorCredito
        {
            get { return _valorCredito; }
            set { _valorCredito = ValidationUtil.ValidateTDec_0302(value, "ValorCredito"); }
        }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN101");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CSOSN", ((int)CSOSN).ToString());
            writer.WriteElementString("pCredSN", AliquotaCredito.ToTDec_0302());
            writer.WriteElementString("vCredICMSSN", ValorCredito.ToTDec_1302());

            writer.WriteEndElement();
        }
    }
}