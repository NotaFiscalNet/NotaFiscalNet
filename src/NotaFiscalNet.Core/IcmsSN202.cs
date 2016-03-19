using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tributação pelo Simples Nacional sem permissão de crédito e com cobrança do ICMS por
    /// substituição tributária.
    /// </summary>
    public class IcmsSN202 : IcmsSimplesNacional
    {
        private decimal _aliquotaST;
        private CSOSN _csosn;
        private ModalidadeBaseCalculoIcmsST _modalidadeBaseCalculoST;
        private decimal? _percentualMargemValorAdicionadoST;
        private decimal? _percentualReducaoBaseCalculoST;
        private decimal _valorBaseCalculoST;
        private decimal _valorST;

        public IcmsSN202(OrigemMercadoria origem, CSOSN csosn)
        {
            Origem = origem;
            CSOSN = csosn;
        }

        /// <summary>
        /// [CSOSN] Retorna ou define o Código de Situação da Operação no Simples Nacional. O valores
        /// permitidos são: 202 e 203.
        /// </summary>
        [NFeField(FieldName = "CSOSN")]
        public override CSOSN CSOSN
        {
            get { return _csosn; }
            protected set
            {
                switch (value)
                {
                    case CSOSN.SN202:
                    case CSOSN.SN203:
                        _csosn = value;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("CSOSN", "O valor informado não é válido no contexto.");
                }
            }
        }

        /// <summary>
        /// [modBCST] Retorna ou define a Modalidade de Base de Cálculo do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "modBCST")]
        public ModalidadeBaseCalculoIcmsST ModalidadeBaseCalculoST
        {
            get { return _modalidadeBaseCalculoST; }
            set
            {
                _modalidadeBaseCalculoST = ValidationUtil.ValidateEnum(value,
                    "ModalidadeBaseCalculoST");
            }
        }

        /// <summary>
        /// [pMVAST] Retorna ou define o Percentual da Margem de Valor Adicionado do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "pMVAST", DataType = "TDec_0302Opc")]
        public decimal? PercentualMargemValorAdicionadoST
        {
            get { return _percentualMargemValorAdicionadoST; }
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
            get { return _percentualReducaoBaseCalculoST; }
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
            get { return _valorBaseCalculoST; }
            set { _valorBaseCalculoST = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculoST"); }
        }

        /// <summary>
        /// [pICMSST] Retorna ou define a Alíquota do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "pICMSST", DataType = "TDec_0302")]
        public decimal AliquotaST
        {
            get { return _aliquotaST; }
            set { _aliquotaST = ValidationUtil.ValidateTDec_0302(value, "AliquotaST"); }
        }

        /// <summary>
        /// [vICMSST] Retorna ou define o Valor do ICMS ST.
        /// </summary>
        [NFeField(FieldName = "vICMSST", DataType = "TDec_1302")]
        public decimal ValorST
        {
            get { return _valorST; }
            set { _valorST = ValidationUtil.ValidateTDec_1302(value, "ValorST"); }
        }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN202");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CSOSN", ((int)CSOSN).ToString());

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