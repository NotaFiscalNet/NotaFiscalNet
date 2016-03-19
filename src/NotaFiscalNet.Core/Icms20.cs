using NotaFiscalNet.Core.Utils;
using System;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o imposto ICMS CST 20 (Com redução de base de cálculo).
    /// </summary>
    public class Icms20 : IcmsTributacaoNormal, IDesoneracaoIcms
    {
        private decimal _aliquota;
        private ModalidadeBaseCalculoIcms _modalidadeBaseCalculo;
        private MotivoDesoneracaoCondicionalICMS? _motivoDesoneracaoIcms;
        private decimal _percentualReducaoBaseCalculo;
        private decimal _valor;
        private decimal _valorBaseCalculo;

        public Icms20()
        {
            CST = SituacaoTributariaICMS.Cst20;
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
        /// [pRedBC] Retorna ou define o Percentual de Redução da Base de Cálculo.
        /// </summary>
        [NFeField(FieldName = "pRedBC", DataType = "TDec_0302")]
        public decimal PercentualReducaoBaseCalculo
        {
            get { return _percentualReducaoBaseCalculo; }
            set
            {
                _percentualReducaoBaseCalculo = ValidationUtil.ValidateTDec_0302(value, "PercentualReducaoBaseCalculo");
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
        [NFeField(FieldName = "vICMS", DataType = "TDec_1302")]
        public decimal Valor
        {
            get { return _valor; }
            set { _valor = ValidationUtil.ValidateTDec_0302(value, "Valor"); }
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
            writer.WriteStartElement("ICMS20");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));
            writer.WriteElementString("modBC", ModalidadeBaseCalculo.GetEnumValue());
            writer.WriteElementString("pRedBC", PercentualReducaoBaseCalculo.ToTDec_0302());
            writer.WriteElementString("vBC", ValorBaseCalculo.ToTDec_1302());
            writer.WriteElementString("pICMS", Aliquota.ToTDec_0302());
            writer.WriteElementString("vICMS", Valor.ToTDec_1302());

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