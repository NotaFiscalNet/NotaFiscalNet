using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o imposto ICMS CST 30 (Isenta ou não tributada e com cobrança do ICMS por
    /// substituição tributária).
    /// </summary>
    public class Icms30 : IcmsTributacaoNormal, IDesoneracaoIcms
    {
        private decimal _aliquotaST;
        private ModalidadeBaseCalculoIcmsST _modalidadeBaseCalculoST;
        private MotivoDesoneracaoCondicionalICMS? _motivoDesoneracaoIcms;
        private decimal? _percentualMargemValorAdicionadoST;
        private decimal? _percentualReducaoBaseCalculoST;
        private decimal _valorBaseCalculoST;
        private decimal _valorST;

        public Icms30()
        {
            CST = SituacaoTributariaICMS.Cst30;
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
        /// <remarks>Valores aceitos: 6-Utilitários Motocicleta AÁrea Livre, 7-SUFRAMA e 9-Outros.</remarks>
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
                    case MotivoDesoneracaoCondicionalICMS.AmazoniaOcidentalAreaLivreComercio:
                    case MotivoDesoneracaoCondicionalICMS.SUFRAMA:
                    case MotivoDesoneracaoCondicionalICMS.Outros:
                        _motivoDesoneracaoIcms = value;
                        break;

                    default:
                        throw new InvalidOperationException(
                            "O valor informado não é válido. Veja a documentação para valores aceitos neste cenário.");
                }
            }
        }

        protected override void SerializeInternal(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ICMS30");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));
            writer.WriteElementString("modBCST", ModalidadeBaseCalculoST.GetEnumValue());

            if (PercentualMargemValorAdicionadoST.HasValue)
                writer.WriteElementString("pMVAST", PercentualMargemValorAdicionadoST.Value.ToTDec_0302());

            if (PercentualReducaoBaseCalculoST.HasValue)
                writer.WriteElementString("pRedBCST", PercentualReducaoBaseCalculoST.Value.ToTDec_0302());

            writer.WriteElementString("vBCST", ValorBaseCalculoST.ToTDec_1302());
            writer.WriteElementString("pICMSST", AliquotaST.ToTDec_0302());
            writer.WriteElementString("vICMSST", ValorST.ToTDec_1302());

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