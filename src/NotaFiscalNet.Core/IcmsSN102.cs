using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tributação do ICMS pelo SIMPLES NACIONAL e CSOSN=102, 103, 300 ou 400.
    /// </summary>
    public class IcmsSN102 : IcmsSimplesNacional
    {
        private CSOSN _csosn = CSOSN.SN102;

        public IcmsSN102(OrigemMercadoria origem, CSOSN csosn)
        {
            Origem = origem;
            CSOSN = csosn;
        }

        /// <summary>
        /// [CSOSN] Retorna ou define o Código de Situação da Operação no Simples Nacional. O valores
        /// permitidos são: 102, 103, 300 e 400.
        /// </summary>
        [NFeField(FieldName = "CSOSN")]
        public override CSOSN CSOSN
        {
            get { return _csosn; }
            protected set
            {
                switch (value)
                {
                    case CSOSN.SN102:
                    case CSOSN.SN103:
                    case CSOSN.SN300:
                    case CSOSN.SN400:
                        _csosn = value;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("CSOSN", "O valor informado não é válido no contexto.");
                }
            }
        }

        protected override void SerializeInternal(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("ICMSSN102");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CSOSN", ((int)CSOSN).ToString());

            writer.WriteEndElement();
        }
    }
}