using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Tributação pelo Simples Nacional com permissão de crédito e com cobrança do ICMS por
    /// substituição tributária.
    /// </summary>
    public class IcmsSN201 : IcmsSimplesNacional
    {
        private decimal _aliquotaCredito;
        private decimal _aliquotaST;

        private ModalidadeBaseCalculoIcmsST _modalidadeBaseCalculoST;
        private decimal? _percentualMargemValorAdicionadoST;
        private decimal? _percentualReducaoBaseCalculoST;
        private decimal _valorBaseCalculoST;
        private decimal _valorCredito;
        private decimal _valorST;

        public IcmsSN201()
        {
            CSOSN = CSOSN.SN201;
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

        protected override void SerializeInternal(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("ICMSSN201");

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
            writer.WriteElementString("pCredSN", AliquotaCredito.ToTDec_0302());
            writer.WriteElementString("vCredICMSSN", ValorCredito.ToTDec_1302());

            writer.WriteEndElement();
        }
    }
}