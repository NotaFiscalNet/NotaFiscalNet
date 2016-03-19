using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// ICMS cobrado anteriormente por substituição tributária (substituído) ou por antecipação.
    /// </summary>
    public class IcmsSN500 : IcmsSimplesNacional
    {
        public IcmsSN500(OrigemMercadoria origem)
        {
            Origem = origem;
            CSOSN = CSOSN.SN500;
            ValorBaseCalculoSTRetido = null;
            ValorSTRetido = null;
        }

        public IcmsSN500(OrigemMercadoria origem, decimal valorBaseCalculoSTRetido, decimal valorSTRetido)
        {
            Origem = origem;
            CSOSN = CSOSN.SN500;
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

        protected override void SerializeInternal(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("ICMSSN500");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CSOSN", ((int)CSOSN).ToString());

            if (ValorBaseCalculoSTRetido.HasValue || ValorSTRetido.HasValue)
            {
                writer.WriteElementString("vICMS", ValorBaseCalculoSTRetido.Value.ToTDec_1302());
                writer.WriteElementString("motDesICMS", ValorSTRetido.Value.ToTDec_1302());
            }

            writer.WriteEndElement();
        }
    }
}