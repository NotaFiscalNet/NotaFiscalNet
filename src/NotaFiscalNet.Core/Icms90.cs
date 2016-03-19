using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o imposto ICMS CST 90 (Outras).
    /// </summary>
    public class Icms90 : IcmsTributacaoNormal, IDesoneracaoIcms
    {
        private decimal _aliquota;
        private decimal _aliquotaST;
        private ModalidadeBaseCalculoIcms _modalidadeBaseCalculo = ModalidadeBaseCalculoIcms.NaoEspecificado;

        private ModalidadeBaseCalculoIcmsST _modalidadeBaseCalculoST = ModalidadeBaseCalculoIcmsST.NaoEspecificado;
        private MotivoDesoneracaoCondicionalICMS? _motivoDesoneracaoIcms;
        private decimal? _percentualMargemValorAdicionadoST;
        private decimal? _percentualReducaoBaseCalculo;
        private decimal? _percentualReducaoBaseCalculoST;
        private decimal _valor;
        private decimal _valorBaseCalculo;
        private decimal _valorBaseCalculoST;
        private decimal _valorST;

        public Icms90()
        {
            CST = SituacaoTributariaICMS.Cst90;
        }

        /// <summary>
        /// [modBC] Retorna ou define a Modalidade da Base de Cálculo do ICMS.
        /// </summary>
        [NFeField(ID = "N13", FieldName = "modBC")]
        public ModalidadeBaseCalculoIcms ModalidadeBaseCalculo
        {
            get { return _modalidadeBaseCalculo; }
            set { _modalidadeBaseCalculo = ValidationUtil.ValidateEnum(value, "ModalidadeBaseCalculo"); }
        }

        /// <summary>
        /// [vRedBC] Retorna ou define o Percentual de Redução da Base de Cálculo.
        /// </summary>
        [NFeField(FieldName = "vRedBC", DataType = "TDec_0302")]
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
        [NFeField(ID = "N15", FieldName = "vBC", DataType = "TDec_1302")]
        public decimal ValorBaseCalculo
        {
            get { return _valorBaseCalculo; }
            set { _valorBaseCalculo = ValidationUtil.ValidateTDec_1302(value, "ValorBaseCalculo"); }
        }

        /// <summary>
        /// [pICMS] Retorna ou define a Alíquota do ICMS.
        /// </summary>
        [NFeField(ID = "N16", FieldName = "pICMS", DataType = "TDec_0302")]
        public decimal Aliquota
        {
            get { return _aliquota; }
            set { _aliquota = ValidationUtil.ValidateTDec_0302(value, "Aliquota"); }
        }

        /// <summary>
        /// [vICMS] Retorna o Valor do ICMS.
        /// </summary>
        [NFeField(ID = "N17", FieldName = "vICMS", DataType = "TDec_0302")]
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
        /// [vICMSDeson] Retorna ou define o Valor do ICMS de desoneração.
        /// </summary>
        [NFeField(FieldName = "vICMSDeson", DataType = "TDec_1302", Opcional = true)]
        public decimal? ValorIcmsDesoneracao { get; set; }

        /// <summary>
        /// [motDesICMS] Retorna ou define o Motivo da desoneração do ICMS.
        /// </summary>
        /// <remarks>Valores aceitos: 3-Uso na agropecuária, 9-Outros e 12-Fomento agropecuário.</remarks>
        [NFeField(FieldName = "motDesICMS", DataType = "xs:string")]
        public MotivoDesoneracaoCondicionalICMS? MotivoDesoneracaoIcms
        {
            get { return _motivoDesoneracaoIcms; }
            set
            {
                if (!value.HasValue)
                {
                    _motivoDesoneracaoIcms = null;
                    return;
                }

                switch (value.Value)
                {
                    case MotivoDesoneracaoCondicionalICMS.ProdutorAgropecuario:
                    case MotivoDesoneracaoCondicionalICMS.Outros:
                    case MotivoDesoneracaoCondicionalICMS.FomentoAgropecuario:
                        _motivoDesoneracaoIcms = value;
                        break;

                    default:
                        throw new InvalidOperationException(
                            "O valor informado não é válido. Veja a documentação para valores aceitos neste cenário.");
                }
            }
        }

        protected override void SerializeInternal(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("ICMS90");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));

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

            if (ValorIcmsDesoneracao.HasValue && MotivoDesoneracaoIcms.HasValue)
            {
                writer.WriteElementString("vICMSDeson", ValorIcmsDesoneracao.Value.ToTDec_1302());
                writer.WriteElementString("motDesICMS", MotivoDesoneracaoIcms.Value.GetEnumValue());
            }
            else if (ValorIcmsDesoneracao.HasValue || MotivoDesoneracaoIcms.HasValue)
                throw new ApplicationException(
                    "Ambos os campos 'ValorIcmsDesoneracao' e 'MotivoDesoneracaoIcms' devem ser preenchidos ou vazios.");

            writer.WriteEndElement();
        }
    }
}