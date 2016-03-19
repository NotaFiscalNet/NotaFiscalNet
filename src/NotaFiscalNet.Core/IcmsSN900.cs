using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    public class IcmsSN900 : IcmsSimplesNacional
    {
        private decimal _aliquota;

        private decimal _aliquotaCredito;
        private decimal _aliquotaST;
        private ModalidadeBaseCalculoIcms _modalidadeBaseCalculo = ModalidadeBaseCalculoIcms.NaoEspecificado;

        private ModalidadeBaseCalculoIcmsST _modalidadeBaseCalculoST = ModalidadeBaseCalculoIcmsST.NaoEspecificado;
        private decimal? _percentualMargemValorAdicionadoST;
        private decimal? _percentualReducaoBaseCalculo;
        private decimal? _percentualReducaoBaseCalculoST;
        private decimal _valor;
        private decimal _valorBaseCalculo;
        private decimal _valorBaseCalculoST;
        private decimal _valorCredito;
        private decimal _valorST;

        public IcmsSN900()
        {
            CSOSN = CSOSN.SN900;
        }

        /// <summary>
        /// [modBC] Retorna ou define a Modalidade da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(FieldName = "modBC")]
        public ModalidadeBaseCalculoIcms ModalidadeBaseCalculo
        {
            get { return _modalidadeBaseCalculo; }
            set { _modalidadeBaseCalculo = ValidationUtil.ValidateEnum(value, "ModalidadeBaseCalculo"); }
        }

        /// <summary>
        /// [vRedBC] Retorna ou define o Percentual de Redução da Base de Cálculo.
        /// </summary>
        [NFeField(FieldName = "pRedBC", DataType = "TDec_0302")]
        public decimal? PercentualReducaoBaseCalculo
        {
            get { return _percentualReducaoBaseCalculo; }
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
        /// [vBC] Retorna ou define o Valor da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(FieldName = "vBC", DataType = "TDec_1302")]
        public decimal ValorBaseCalculo
        {
            get { return _valorBaseCalculo; }
            set { _valorBaseCalculo = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculo"); }
        }

        /// <summary>
        /// [pICMS] Retorna ou define a Alíquota do ICMS.
        /// </summary>
        [NFeField(FieldName = "pICMS", DataType = "TDec_0302")]
        public decimal Aliquota
        {
            get { return _aliquota; }
            set { _aliquota = ValidationUtil.ValidateTDec_0302(value, "Aliquota"); }
        }

        /// <summary>
        /// [vICMS] Retorna ou define o Valor do ICMS.
        /// </summary>
        [NFeField(FieldName = "vICMS", DataType = "TDec_0302")]
        public decimal Valor
        {
            get { return _valor; }
            set { _valor = ValidationUtil.ValidateTDec_0302(value, "Valor"); }
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

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMSSN900");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CSOSN", ((int)CSOSN).ToString());

            if (ModalidadeBaseCalculo != ModalidadeBaseCalculoIcms.NaoEspecificado)
            {
                writer.WriteElementString("modBC", ModalidadeBaseCalculo.GetEnumValue());
                writer.WriteElementString("vBC", ValorBaseCalculo.ToTDec_1302());

                if (PercentualReducaoBaseCalculo.HasValue)
                    writer.WriteElementString("pRedBC", PercentualReducaoBaseCalculo.Value.ToTDec_0302());

                writer.WriteElementString("pICMS", Aliquota.ToTDec_0302());
                writer.WriteElementString("vICMS", Valor.ToTDec_1302());
            }

            if (ModalidadeBaseCalculoST != ModalidadeBaseCalculoIcmsST.NaoEspecificado)
            {
                writer.WriteElementString("modBCST", ModalidadeBaseCalculoST.GetEnumValue());

                if (PercentualMargemValorAdicionadoST.HasValue)
                    writer.WriteElementString("pMVAST", PercentualMargemValorAdicionadoST.Value.ToTDec_0302());

                if (PercentualReducaoBaseCalculoST.HasValue)
                    writer.WriteElementString("pRedBCST", PercentualReducaoBaseCalculoST.Value.ToTDec_0302());

                writer.WriteElementString("vBCST", ValorBaseCalculoST.ToTDec_1302());
                writer.WriteElementString("pICMSST", AliquotaST.ToTDec_0302());
                writer.WriteElementString("vICMSST", ValorST.ToTDec_1302());
            }

            if (AliquotaCredito > 0m && ValorCredito > 0m)
            {
                writer.WriteElementString("pCredSN", AliquotaCredito.ToTDec_0302());
                writer.WriteElementString("vCredICMSSN", ValorCredito.ToTDec_1302());
            }

            writer.WriteEndElement();
        }
    }
}