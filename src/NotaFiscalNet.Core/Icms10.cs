using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o imposto ICMS CST 10 (Tributada e com cobrança do ICMS por substituição tributária).
    /// </summary>
    public class Icms10 : IcmsTributacaoNormal
    {
        private decimal _aliquota;
        private decimal _aliquotaST;
        private ModalidadeBaseCalculoIcms _modalidadeBaseCalculo;
        private ModalidadeBaseCalculoIcmsST _modalidadeBaseCalculoST;
        private decimal? _percentualMargemValorAdicionadoST;
        private decimal? _percentualReducaoBaseCalculoST;
        private decimal _valor;
        private decimal _valorBaseCalculo;
        private decimal _valorBaseCalculoST;
        private decimal _valorST;

        public Icms10()
        {
            CST = SituacaoTributariaICMS.Cst10;
        }

        /// <summary>
        /// [modBC] Retorna ou define a Modalidade da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(ID = "N13", FieldName = "modBC")]
        public ModalidadeBaseCalculoIcms ModalidadeBaseCalculo
        {
            get => _modalidadeBaseCalculo;
	        set => _modalidadeBaseCalculo = ValidationUtil.ValidateEnum(value, "ModalidadeBaseCalculo");
        }

        /// <summary>
        /// [vBC] Retorna ou define o Valor da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(ID = "N15", FieldName = "vBC", DataType = "TDec_1302")]
        public decimal ValorBaseCalculo
        {
            get => _valorBaseCalculo;
	        set => _valorBaseCalculo = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculo");
        }

        /// <summary>
        /// [pICMS] Retorna ou define a Alíquota do ICMS.
        /// </summary>
        [NFeField(ID = "N16", FieldName = "pICMS", DataType = "TDec_0302")]
        public decimal Aliquota
        {
            get => _aliquota;
	        set => _aliquota = ValidationUtil.ValidateTDec_0302(value, "Aliquota");
        }

        /// <summary>
        /// [vICMS] Retorna ou define o Valor do ICMS.
        /// </summary>
        [NFeField(ID = "N17", FieldName = "vICMS", DataType = "TDec_0302")]
        public decimal Valor
        {
            get => _valor;
	        set => _valor = ValidationUtil.ValidateTDec_0302(value, "Valor");
        }

        /// <summary>
        /// [modBCST] Retorna ou define a Modalidade de Base de Cálculo do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "modBCST")]
        public ModalidadeBaseCalculoIcmsST ModalidadeBaseCalculoST
        {
            get => _modalidadeBaseCalculoST;
	        set => _modalidadeBaseCalculoST = ValidationUtil.ValidateEnum(value,
		        "ModalidadeBaseCalculoST");
        }

        /// <summary>
        /// [pMVAST] Retorna ou define o Percentual da Margem de Valor Adicionado do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "pMVAST", DataType = "TDec_0302Opc")]
        public decimal? PercentualMargemValorAdicionadoST
        {
            get => _percentualMargemValorAdicionadoST;
	        set
            {
                if (value.HasValue)
                    _percentualMargemValorAdicionadoST = ValidationUtil.ValidateTDec_0302(value.Value,
                        "PercentualMargemValorAdicionadoST");
                else
                    _percentualMargemValorAdicionadoST = null;
            }
        }

        /// <summary>
        /// [pRedBCST] Retorna ou define o Percentual de Redução da Base de Cálculo do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "pRedBCST", DataType = "TDec_0302Opc")]
        public decimal? PercentualReducaoBaseCalculoST
        {
            get => _percentualReducaoBaseCalculoST;
	        set
            {
                if (value.HasValue)
                    _percentualReducaoBaseCalculoST = ValidationUtil.ValidateTDec_0302(value.Value,
                        "PercentualReducaoBaseCalculoST");
                else
                    _percentualReducaoBaseCalculoST = null;
            }
        }

        /// <summary>
        /// [vBCST] Retorna ou define o Valor da Base de Cálculo do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "vBCST", DataType = "TDec_1302")]
        public decimal ValorBaseCalculoST
        {
            get => _valorBaseCalculoST;
	        set => _valorBaseCalculoST = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculoST");
        }

        /// <summary>
        /// [pICMSST] Retorna ou define a Alíquota do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "pICMSST", DataType = "TDec_0302")]
        public decimal AliquotaST
        {
            get => _aliquotaST;
	        set => _aliquotaST = ValidationUtil.ValidateTDec_0302(value, "AliquotaST");
        }

        /// <summary>
        /// [vICMSST] Retorna ou define o Valor do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "vICMSST", DataType = "TDec_1302")]
        public decimal ValorST
        {
            get => _valorST;
	        set => _valorST = ValidationUtil.ValidateTDec_0302(value, "ValorST");
        }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS10");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));
            writer.WriteElementString("modBC", ModalidadeBaseCalculo.GetEnumValue());
            writer.WriteElementString("vBC", ValorBaseCalculo.ToTDec_1302());
            writer.WriteElementString("pICMS", Aliquota.ToTDec_0302());
            writer.WriteElementString("vICMS", Valor.ToTDec_1302());

            writer.WriteElementString("modBCST", ModalidadeBaseCalculoST.GetEnumValue());

            if (PercentualMargemValorAdicionadoST.HasValue)
                writer.WriteElementString("pMVAST", PercentualMargemValorAdicionadoST.Value.ToTDec_0302());

            if (PercentualReducaoBaseCalculoST.HasValue)
                writer.WriteElementString("pRedBCST", PercentualReducaoBaseCalculoST.Value.ToTDec_0302());

            writer.WriteElementString("vBCST", ValorBaseCalculoST.ToTDec_1302());
            writer.WriteElementString("pICMSST", AliquotaST.ToTDec_0302());
            writer.WriteElementString("vICMSST", ValorST.ToTDec_1302());

            writer.WriteEndElement();
        }
    }
}