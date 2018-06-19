using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o imposto ICMS de Partilha entre a UF de origem e UF de destino ou a UF definida
    /// na legislação. Operação interestadual para consumidor final com partilha do ICMS devido na
    /// operação entre a UF de origem e a UF do destinatário ou ou a UF definida na legislação. (Ex.
    /// UF da concessionária de entrega do veículos).
    /// </summary>
    public class IcmsPartilha : IcmsTributacaoNormal
    {
        private decimal _aliquota;
        private decimal _aliquotaST;
        private SituacaoTributariaICMS _cst;
        private ModalidadeBaseCalculoIcms _modalidadeBaseCalculo;
        private ModalidadeBaseCalculoIcmsST _modalidadeBaseCalculoST;
        private decimal _percentualBaseCalculoOperacaoPropria;
        private decimal? _percentualMargemValorAdicionadoST;
        private decimal? _percentualReducaoBaseCalculo;
        private decimal? _percentualReducaoBaseCalculoST;
        private SiglaUF _ufSTDevido;
        private decimal _valor;
        private decimal _valorBaseCalculo;
        private decimal _valorBaseCalculoST;
        private decimal _valorST;

        public IcmsPartilha(OrigemMercadoria origem, SituacaoTributariaICMS cst)
        {
            Origem = origem;
            CST = cst;
        }

        /// <summary>
        /// Retorna ou define o Código de Situação Tributária do ICMS.
        /// </summary>
        [NFeField(FieldName = "CST")]
        public override SituacaoTributariaICMS CST
        {
            get => _cst;
	        protected set
            {
                switch (value)
                {
                    case SituacaoTributariaICMS.Cst10:
                    case SituacaoTributariaICMS.Cst90:
                        _cst = value;
                        break;

                    default:
                        throw new ApplicationException(
                            "O CST informado não é válido para o tipo de ICMS (Partilha) atual.");
                }
            }
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
        [NFeField(FieldName = "vBC", DataType = "TDec_1302")]
        public decimal ValorBaseCalculo
        {
            get => _valorBaseCalculo;
	        set => _valorBaseCalculo = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculo");
        }

        /// <summary>
        /// [vRedBC] Retorna ou define o Percentual de Redução da Base de Cálculo.
        /// </summary>
        [NFeField(FieldName = "pRedBC", DataType = "TDec_0302")]
        public decimal? PercentualReducaoBaseCalculo
        {
            get => _percentualReducaoBaseCalculo;
	        set
            {
                if (value.HasValue)
                    _percentualReducaoBaseCalculo = ValidationUtil.ValidateTDec_0302(value.Value,
                        "PercentualReducaoBaseCalculo");
                else
                    _percentualReducaoBaseCalculo = null;
            }
        }

        /// <summary>
        /// [pICMS] Retorna ou define a Alíquota do ICMS.
        /// </summary>
        [NFeField(FieldName = "pICMS", DataType = "TDec_0302")]
        public decimal Aliquota
        {
            get => _aliquota;
	        set => _aliquota = ValidationUtil.ValidateTDec_0302(value, "Aliquota");
        }

        /// <summary>
        /// [vICMS] Retorna ou define o Valor do ICMS.
        /// </summary>
        [NFeField(FieldName = "vICMS", DataType = "TDec_1302")]
        public decimal Valor
        {
            get => _valor;
	        set => _valor = ValidationUtil.ValidateTDec_1302(value, "Valor");
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
	        set => _valorST = ValidationUtil.ValidateTDec_1302(value, "ValorST");
        }

        /// <summary>
        /// [vBCOp] Retorna ou define o Percentual para determinação do valor da Base de Cálculo da
        /// operação própria.
        /// </summary>
        [NFeField(FieldName = "vBCOp", DataType = "TDec_0302Opc")]
        public decimal PercentualBaseCalculoOperacaoPropria
        {
            get => _percentualBaseCalculoOperacaoPropria;
	        set => _percentualBaseCalculoOperacaoPropria = ValidationUtil.ValidateTDec_0302(value,
		        "PercentualBaseCalculoOperacaoPropria");
        }

        /// <summary>
        /// [vBCOp] Retorna ou define a Sigla da UF qual é devido o ICMS ST da operação.
        /// </summary>
        [NFeField(FieldName = "vBCOp", DataType = "TDec_0302Opc")]
        public SiglaUF UFST
        {
            get => _ufSTDevido;
	        set => _ufSTDevido = ValidationUtil.ValidateEnumOptional(value, SiglaUF.NaoEspecificado, "UFST");
        }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSPart");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));
            writer.WriteElementString("modBC", ModalidadeBaseCalculo.GetEnumValue());
            writer.WriteElementString("vBC", ValorBaseCalculo.ToTDec_1302());
            if (PercentualReducaoBaseCalculo.HasValue)
                writer.WriteElementString("pRedBC", PercentualReducaoBaseCalculo.Value.ToTDec_0302());

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

            writer.WriteElementString("pBCOp", PercentualBaseCalculoOperacaoPropria.ToTDec_0302());
            writer.WriteElementString("UFST", UFST.ToString());

            writer.WriteEndElement();
        }
    }
}