using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Grupo de informação do ICMSST devido para a UF de destino, nas operações interestaduais de
    /// produtos que tiveram retenção antecipada de ICMS por ST na UF do remetente. Repasse via
    /// Substituto Tributário.
    /// </summary>
    public class IcmsST : IcmsTributacaoNormal
    {
        private decimal _valorBaseCalculoSTRetido;
        private decimal _valorBaseCalculoSTUFDestino;
        private decimal _valorSTRetido;
        private decimal _valorSTUFDestino;

        public IcmsST()
        {
            CST = SituacaoTributariaICMS.Cst41;
        }

        /// <summary>
        /// [vBCSTRet] Retorna ou define o Valor da Base de Cálculo do ICMS ST retido na UF remetente.
        /// </summary>
        [NFeField(FieldName = "vBCSTRet", DataType = "TDec_1302")]
        public decimal ValorBaseCalculoSTRetido
        {
            get { return _valorBaseCalculoSTRetido; }
            set { _valorBaseCalculoSTRetido = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculoSTRetido"); }
        }

        /// <summary>
        /// [vICMSSTRet] Retorna ou define o Valor do ICMS ST retido na UF remetente.
        /// </summary>
        [NFeField(FieldName = "vICMSSTRet", DataType = "TDec_1302")]
        public decimal ValorSTRetido
        {
            get { return _valorSTRetido; }
            set { _valorSTRetido = ValidationUtil.ValidateTDec_1302(value, "ValorSTRetido"); }
        }

        /// <summary>
        /// [vBCSTDest] Retorna ou define o Valor da Base de Cálculo do ICMS ST da UF de destino.
        /// </summary>
        [NFeField(FieldName = "vBCSTDest", DataType = "TDec_1302")]
        public decimal ValorBaseCalculoSTUFDestino
        {
            get { return _valorBaseCalculoSTUFDestino; }
            set
            {
                _valorBaseCalculoSTUFDestino = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculoSTUFDestino");
            }
        }

        /// <summary>
        /// [vICMSSTDet] Retorna ou define o Valor do ICMS ST da UF destino.
        /// </summary>
        [NFeField(FieldName = "vICMSSTDet", DataType = "TDec_1302")]
        public decimal ValorSTUFDestino
        {
            get { return _valorSTUFDestino; }
            set { _valorSTUFDestino = ValidationUtil.ValidateTDec_1302(value, "ValorSTUFDestino"); }
        }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSST");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));
            writer.WriteElementString("vBCSTRet", ValorBaseCalculoSTRetido.ToTDec_1302());
            writer.WriteElementString("vICMSSTRet", ValorSTRetido.ToTDec_1302());
            writer.WriteElementString("vBCSTDest", ValorBaseCalculoSTUFDestino.ToTDec_1302());
            writer.WriteElementString("vICMSSTDest", ValorSTUFDestino.ToTDec_1302());

            writer.WriteEndElement();
        }
    }
}