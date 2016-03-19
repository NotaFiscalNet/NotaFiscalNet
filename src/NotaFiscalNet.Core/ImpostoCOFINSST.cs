using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Imposto Contribuição para o Financiamento da Seguridade Social ST
    /// </summary>

    public sealed class ImpostoCOFINSST : INFeSerializable
    {
        private TipoCalculoCOFINS _tipo = TipoCalculoCOFINS.PercentualValor;
        private decimal _baseCalculo;
        private decimal _aliquota;
        private decimal _valor;

        private readonly ImpostoProduto _imposto;

        internal ImpostoCOFINSST(ImpostoProduto imposto)
        {
            _imposto = imposto;
        }

        /// <summary>
        /// Retorna a referência para o objeto ImpostoProduto no qual o Imposto se refere.
        /// </summary>
        internal ImpostoProduto Imposto { get { return _imposto; } }

        /// <summary>
        /// Retorna ou define o Tipo da Alíquota e da Base de Cálculo.
        /// </summary>
        public TipoCalculoCOFINS TipoCalculo
        {
            get { return _tipo; }
            set
            {
                ValidationUtil.ValidateEnum(value, "TipoCalculo");
                _tipo = value;
            }
        }

        /// <summary>
        /// [vBC,qBCProd] Retorna ou define a Base de Cálculo do COFINS ST.
        /// </summary>
        [NFeField(ID = "T02", FieldName = "vBC", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [NFeField(ID = "T04", FieldName = "qBCProd", DataType = "TDec_1204Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,2}(\.[0-9]{2})?")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                switch (TipoCalculo)
                {
                    case TipoCalculoCOFINS.PercentualValor:
                        ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                        break;

                    case TipoCalculoCOFINS.ValorQuantidade:
                        ValidationUtil.ValidateTDec_1204(value, "BaseCalculo");
                        break;
                }
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [pCOFINS,vAliqProd] Retorna ou define a Alíquota do COFINS ST.
        /// </summary>
        [NFeField(ID = "T03", FieldName = "pCOFINS", DataType = "TDec_0302Opc", Pattern = @"0\.[1-9]{1}[0-9]{3}|0\.[0-9]{3}[1-9]{1}|0\.[0-9]{2}[1-9]{1}[0-9]{1}|0\.[0-9]{1}[1-9]{1}[0-9]{2}|[1-9]{1}[0-9]{0,11}(\.[0-9]{4})?")]
        [NFeField(ID = "T05", FieldName = "vAliqProd", DataType = "TDec_1104Opc", Pattern = @"0\.[1-9]{1}[0-9]{3}|0\.[0-9]{3}[1-9]{1}|0\.[0-9]{2}[1-9]{1}[0-9]{1}|0\.[0-9]{1}[1-9]{1}[0-9]{2}|[1-9]{1}[0-9]{0,10}(\.[0-9]{4})?")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal Aliquota
        {
            get { return _aliquota; }
            set
            {
                switch (TipoCalculo)
                {
                    case TipoCalculoCOFINS.PercentualValor:
                        ValidationUtil.ValidateTDec_0302(value, "Aliquota");
                        break;

                    case TipoCalculoCOFINS.ValorQuantidade:
                        ValidationUtil.ValidateTDec_1104(value, "Aliquota");
                        break;
                }
                _aliquota = value;
            }
        }

        /// <summary>
        /// [vCOFINS] Retorna ou define o Valor do COFINS ST.
        /// </summary>
        [NFeField(ID = "T06", FieldName = "vCOFINS", DataType = "TDec_1302", Pattern = @"0|0\.[0-9]{2}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?")]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public decimal Valor
        {
            get { return _valor; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "Valor");
                _valor = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    Aliquota != 0m ||
                    BaseCalculo != 0m ||
                    Valor != 0m;
            }
        }

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("COFINSST"); // Elemento 'COFINSST'
            switch (TipoCalculo)
            {
                case TipoCalculoCOFINS.PercentualValor:
                    writer.WriteElementString("vBC", SerializationUtil.ToTDec_1302(BaseCalculo));
                    writer.WriteElementString("pCOFINS", SerializationUtil.ToTDec_0302(Aliquota));
                    break;

                case TipoCalculoCOFINS.ValorQuantidade:
                    writer.WriteElementString("qBCProd", SerializationUtil.ToTDec_1204(BaseCalculo));
                    writer.WriteElementString("vAliqProd", SerializationUtil.ToTDec_1104(Aliquota));
                    break;
            }
            writer.WriteElementString("vCOFINS", SerializationUtil.ToTDec_1302(Valor));
            writer.WriteEndElement(); // Elemento 'COFINSST'
        }
    }
}