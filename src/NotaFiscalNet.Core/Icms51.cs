using NotaFiscalNet.Core.Utils;
using System.Xml;

namespace NotaFiscalNet.Core
{
    public class Icms51 : IcmsTributacaoNormal
    {
        private decimal? _aliquota;
        private decimal? _aliquotaDiferimento;
        private ModalidadeBaseCalculoIcms? _modalidadeBaseCalculo;
        private decimal? _percentualReducaoBaseCalculo;
        private decimal? _valor;
        private decimal? _valorBaseCalculo;
        private decimal? _valorIcmsDiferido;
        private decimal? _valorIcmsOperacao;

        public Icms51()
        {
            CST = SituacaoTributariaICMS.Cst51;
        }

        /// <summary>
        /// [modBC] Retorna ou define a Modalidade da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(FieldName = "modBC")]
        public ModalidadeBaseCalculoIcms? ModalidadeBaseCalculo
        {
            get => _modalidadeBaseCalculo;
	        set => _modalidadeBaseCalculo = ValidationUtil.ValidateEnum(value, "ModalidadeBaseCalculo");
        }

        /// <summary>
        /// [pRedBC] Retorna ou define o Percentual de Redução de Base de Cálculo.
        /// </summary>
        [NFeField(FieldName = "pRedBC", DataType = "TDec_0302")]
        public decimal? PercentualReducaoBaseCalculo
        {
            get => _percentualReducaoBaseCalculo;
	        set => _percentualReducaoBaseCalculo = ValidationUtil.ValidateTDec_0302Opc(value,
		        "PercentualReducaoBaseCalculo");
        }

        /// <summary>
        /// [vBC] Retorna ou define o Valor da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(FieldName = "vBC", DataType = "TDec_1302")]
        public decimal? ValorBaseCalculo
        {
            get => _valorBaseCalculo;
	        set => _valorBaseCalculo = ValidationUtil.ValidateTDec_1302Opc(value, "ValorBaseCalculo");
        }

        /// <summary>
        /// [pICMS] Retorna ou define a Alíquota do ICMS.
        /// </summary>
        [NFeField(FieldName = "pICMS", DataType = "TDec_0302")]
        public decimal? Aliquota
        {
            get => _aliquota;
	        set => _aliquota = ValidationUtil.ValidateTDec_0302Opc(value, "Aliquota");
        }

        /// <summary>
        /// [vICMSOp] Retorna ou define o valor do ICMS da Operação.
        /// </summary>
        [NFeField(FieldName = "vICMSOp", DataType = "TDec_1302Opc")]
        public decimal? ValorIcmsOperacao
        {
            get => _valorIcmsOperacao;
	        set => _valorIcmsOperacao = ValidationUtil.ValidateTDec_1302Opc(value, "ValorIcmsOperacao");
        }

        /// <summary>
        /// [pDif] Retorna ou define o percentual do diferimento.
        /// </summary>
        [NFeField(FieldName = "pDif", DataType = "TDec_0302a04Opc")]
        public decimal? AliquotaDiferimento
        {
            get => _aliquotaDiferimento;
	        set => _aliquotaDiferimento = ValidationUtil.ValidateTDec_0302a04Opc(value, "AliquotaDiferimento");
        }

        /// <summary>
        /// [vICMSDif] Retorna ou define o valor do ICMS diferido.
        /// </summary>
        [NFeField(FieldName = "vICMSDif", DataType = "TDec_1302Opc")]
        public decimal? ValorIcmsDiferido
        {
            get => _valorIcmsDiferido;
	        set => _valorIcmsDiferido = ValidationUtil.ValidateTDec_1302Opc(value, "ValorIcmsDiferido");
        }

        /// <summary>
        /// [vICMS] Retorna ou define o Valor do ICMS.
        /// </summary>
        [NFeField(FieldName = "vICMS", DataType = "TDec_0302")]
        public decimal? Valor
        {
            get => _valor;
	        set => _valor = ValidationUtil.ValidateTDec_1302Opc(value, "Valor");
        }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS51");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));
            if (ModalidadeBaseCalculo.HasValue)
                writer.WriteElementString("modBC", ModalidadeBaseCalculo.GetEnumValue());

            if (PercentualReducaoBaseCalculo.HasValue)
                writer.WriteElementString("pRedBC", PercentualReducaoBaseCalculo.Value.ToTDec_0302());

            if (ValorBaseCalculo.HasValue)
                writer.WriteElementString("vBC", ValorBaseCalculo.Value.ToTDec_1302());

            if (Aliquota.HasValue)
                writer.WriteElementString("pICMS", Aliquota.Value.ToTDec_0302());

            if (ValorIcmsOperacao.HasValue)
                writer.WriteElementString("vICMSOp", ValorIcmsOperacao.Value.ToTDec_1302());

            if (AliquotaDiferimento.HasValue)
                writer.WriteElementString("pDif", AliquotaDiferimento.Value.ToTDec_0302a04());

            if (ValorIcmsDiferido.HasValue)
                writer.WriteElementString("vICMSDif", ValorIcmsDiferido.Value.ToTDec_1302());

            if (Valor.HasValue)
                writer.WriteElementString("vICMS", Valor.Value.ToTDec_1302());

            writer.WriteEndElement();
        }
    }
}