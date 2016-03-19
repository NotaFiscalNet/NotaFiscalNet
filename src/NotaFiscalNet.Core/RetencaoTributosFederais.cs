using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa a Retenção de Tributos Federais da Nota Fiscal Eletrônica
    /// </summary>
    public sealed class RetencaoTributosFederais : ISerializavel, IModificavel
    {
        private decimal _valorRetidoPIS;
        private decimal _valorRetidoCOFINS;
        private decimal _valorRetidoCSLL;
        private decimal _baseCalculo;
        private decimal _valorRetidoIRRF;
        private decimal _baseCalculoRetencaoPrevidenciaSocial;
        private decimal _valorRetencaoPrevidenciaSocial;

        public RetencaoTributosFederais()
        {
        }

        /// <summary>
        /// [vRetPIS] Retorna ou define o Valor Total Retido de PIS. Opcional.
        /// </summary>
        [NFeField(ID = "W24", FieldName = "vRetPIS", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorRetidoPIS
        {
            get { return _valorRetidoPIS; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorRetidoPIS");
                _valorRetidoPIS = value;
            }
        }

        /// <summary>
        /// [vRetCOFINS] Retorna ou define o Valor Total Retido de COFINS. Opcional.
        /// </summary>
        [NFeField(ID = "W25", FieldName = "vRetCOFINS", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorRetidoCOFINS
        {
            get { return _valorRetidoCOFINS; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorRetidoCOFINS");
                _valorRetidoCOFINS = value;
            }
        }

        /// <summary>
        /// [vRetCSLL] Retorna ou define o Valor Total Retido de CSLL. Opcional.
        /// </summary>
        [NFeField(ID = "W26", FieldName = "vRetCSLL", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorRetidoCSLL
        {
            get { return _valorRetidoCSLL; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorRetidoCSLL");
                _valorRetidoCSLL = value;
            }
        }

        /// <summary>
        /// [vBCIRRF] Retorna ou define a Base de Cálculo do IRRF. Opcional.
        /// </summary>
        [NFeField(ID = "W27", FieldName = "vBCIRRF", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal BaseCalculo
        {
            get { return _baseCalculo; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "BaseCalculo");
                _baseCalculo = value;
            }
        }

        /// <summary>
        /// [vIRRF] Retorna ou define o Valor Retido do IRRF. Opcional.
        /// </summary>
        [NFeField(ID = "W28", FieldName = "vIRRF", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorRetidoIRRF
        {
            get { return _valorRetidoIRRF; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorRetidoIRRF");
                _valorRetidoIRRF = value;
            }
        }

        /// <summary>
        /// [vBCRetPrev] Retorna ou define a Base de Cálculo da Retenção da Previdência Social. Opcional.
        /// </summary>
        [NFeField(ID = "W29", FieldName = "vBCRetPrev", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal BaseCalculoRetencaoPrevidenciaSocial
        {
            get { return _baseCalculoRetencaoPrevidenciaSocial; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "BaseCalculoRetencaoPrevidenciaSocial");
                _baseCalculoRetencaoPrevidenciaSocial = value;
            }
        }

        /// <summary>
        /// [vRetPrev] Retorna ou define o Valor da Retenção da Previdência Social. Opcional.
        /// </summary>
        [NFeField(ID = "W30", FieldName = "vRetPrev", DataType = "TDec_1302Opc", Pattern = @"0\.[0-9]{1}[1-9]{1}|0\.[1-9]{1}[0-9]{1}|[1-9]{1}[0-9]{0,12}(\.[0-9]{2})?", Opcional = true)]
        public decimal ValorRetencaoPrevidenciaSocial
        {
            get { return _valorRetencaoPrevidenciaSocial; }
            set
            {
                ValidationUtil.ValidateTDec_1302(value, "ValorRetencaoPrevidenciaSocial");
                _valorRetencaoPrevidenciaSocial = value;
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => ValorRetidoPIS != 0m ||
                                  ValorRetidoCOFINS > 0m ||
                                  ValorRetidoCSLL > 0m ||
                                  BaseCalculo > 0m ||
                                  ValorRetidoIRRF > 0m ||
                                  BaseCalculoRetencaoPrevidenciaSocial > 0m ||
                                  ValorRetencaoPrevidenciaSocial > 0m;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("retTrib");

            if (ValorRetidoPIS > 0)
                writer.WriteElementString("vRetPIS", SerializationUtil.ToTDec_1302(ValorRetidoPIS));

            if (ValorRetidoCOFINS > 0)
                writer.WriteElementString("vRetCOFINS", SerializationUtil.ToTDec_1302(ValorRetidoCOFINS));

            if (ValorRetidoCSLL > 0)
                writer.WriteElementString("vRetCSLL", SerializationUtil.ToTDec_1302(ValorRetidoCSLL));

            if (BaseCalculo > 0)
                writer.WriteElementString("vBCIRRF", SerializationUtil.ToTDec_1302(BaseCalculo));

            if (ValorRetidoIRRF > 0)
                writer.WriteElementString("vIRRF", SerializationUtil.ToTDec_1302(ValorRetidoIRRF));

            if (BaseCalculoRetencaoPrevidenciaSocial > 0)
                writer.WriteElementString("vBCRetPrev", SerializationUtil.ToTDec_1302(BaseCalculoRetencaoPrevidenciaSocial));

            if (ValorRetencaoPrevidenciaSocial > 0)
                writer.WriteElementString("vRetPrev", SerializationUtil.ToTDec_1302(ValorRetencaoPrevidenciaSocial));

            writer.WriteEndElement(); // fim do elemento 'retTrib'
        }
    }
}