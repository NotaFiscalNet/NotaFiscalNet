using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o imposto ICMS CST 60 (ICMS cobrado anteriormente por substituição tributária).
    /// </summary>
    public class Icms60 : IcmsTributacaoNormal
    {
        public Icms60(OrigemMercadoria origem)
        {
            Origem = origem;
            CST = SituacaoTributariaICMS.Cst60;
            ValorBaseCalculoSTRetido = null;
            ValorSTRetido = null;
        }

        public Icms60(OrigemMercadoria origem, decimal valorBaseCalculoSTRetido, decimal valorSTRetido)
        {
            Origem = origem;
            CST = SituacaoTributariaICMS.Cst60;
            ValorBaseCalculoSTRetido = ValidationUtil.ValidateTDec_1302(valorBaseCalculoSTRetido,
                "valorBaseCalculoSTRetido");
            ValorSTRetido = ValidationUtil.ValidateTDec_1302(valorSTRetido, "valorSTRetido");
        }

        /// <summary>
        /// [vBCSTRet] Retorna ou define o Valor da Base de Cálculo do ICMS ST retido anteriormente.
        /// </summary>
        [NFeField(FieldName = "vBCSTRet", DataType = "TDec_1302")]
        public decimal? ValorBaseCalculoSTRetido { get; }

        /// <summary>
        /// [vICMSSTRet] Retorna ou define o Valor do ICMS ST retido anteriormente.
        /// </summary>
        [NFeField(FieldName = "vICMSSTRet", DataType = "TDec_1302")]
        public decimal? ValorSTRetido { get; }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS60");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));

            if (ValorBaseCalculoSTRetido.HasValue || ValorSTRetido.HasValue)
            {
                writer.WriteElementString("vICMS", ValorBaseCalculoSTRetido.Value.ToTDec_1302());
                writer.WriteElementString("motDesICMS", ValorSTRetido.Value.ToTDec_1302());
            }

            writer.WriteEndElement();
        }
    }
}