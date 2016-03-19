using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o imposto ICMS CST 40 (Tributação pelo ICMS 40-Isenta, 41-Não tributada e 50-Suspensão).
    /// </summary>
    public class Icms40 : IcmsTributacaoNormal, IDesoneracaoIcms
    {
        private SituacaoTributariaICMS _cst;
        private MotivoDesoneracaoCondicionalICMS? _motivoDesoneracao;

        public Icms40(OrigemMercadoria origem, SituacaoTributariaICMS cst)
        {
            Origem = origem;
            CST = cst;
            ValorIcmsDesoneracao = null;
            MotivoDesoneracaoIcms = null;
        }

        public Icms40(OrigemMercadoria origem, SituacaoTributariaICMS cst, decimal valorIcmsDesoneracao,
            MotivoDesoneracaoCondicionalICMS motivoDesoneracao)
        {
            ValidationUtil.ValidateTDec_1302(valorIcmsDesoneracao, "valor");
            ValidationUtil.ValidateEnum(motivoDesoneracao, "motivoDesoneracao");
            if (motivoDesoneracao == MotivoDesoneracaoCondicionalICMS.NaoEspecificado)
                throw new ErroValidacaoNFeException(ErroValidacao.Create(ChaveErroValidacao.GenericArgumentException,
                    "motivoDesoneracao"));
            Origem = origem;
            CST = cst;
            ValorIcmsDesoneracao = valorIcmsDesoneracao;
            MotivoDesoneracaoIcms = motivoDesoneracao;
        }

        /// <summary>
        /// Retorna ou define o Código de Situação Tributária do ICMS.
        /// </summary>
        /// <remarks>
        /// Valores aceitos: 40 - Isenta 41 - Não tributada 50 - Suspensão 51 - Diferimento
        /// </remarks>
        [NFeField(FieldName = "CST")]
        public override SituacaoTributariaICMS CST
        {
            get { return _cst; }
            protected set
            {
                switch (value)
                {
                    case SituacaoTributariaICMS.Cst40:
                    case SituacaoTributariaICMS.Cst41:
                    case SituacaoTributariaICMS.Cst50:
                    case SituacaoTributariaICMS.Cst51:
                        _cst = value;
                        break;

                    default:
                        throw new ApplicationException("O CST informado não é válido para o tipo de ICMS (40) atual.");
                }
            }
        }

        /// <summary>
        /// [vICMS] Retorna ou define o Valor do ICMS Desoneração.
        /// </summary>
        /// <remarks>
        /// O valor do ICMS será informado apenas nas operações com veículos beneficiados com a
        /// desoneração condicional do ICMS.
        /// </remarks>
        [NFeField(FieldName = "vICMSDeson", DataType = "TDec_1302")]
        public decimal? ValorIcmsDesoneracao { get; set; }

        /// <summary>
        /// [motDesICMS] Retorna ou define o motivo da desoneração.
        /// </summary>
        /// <remarks>
        /// Opções aceitas: 1 – Táxi; 3 – Produtor Agropecuário; 4 – Frotista/Locadora; 5 –
        /// Diplomático/Consular; 6 – Utilitários e Motocicletas da Amazônia Ocidental e Áreas de
        /// Livre Comércio (Resolução 714/88 e 790/94 – CONTRAN e suas alterações); 7 – SUFRAMA; 8 -
        /// Venda a órgão Público; 9 – Outros 10- Deficiente Condutor 11- Deficiente não condutor
        /// </remarks>
        [NFeField(FieldName = "motDesICMS", DataType = "TDec_1302")]
        public MotivoDesoneracaoCondicionalICMS? MotivoDesoneracaoIcms
        {
            get { return _motivoDesoneracao; }
            set
            {
                if (!value.HasValue)
                    _motivoDesoneracao = null;

                switch (value)
                {
                    case MotivoDesoneracaoCondicionalICMS.Taxi:
                    case MotivoDesoneracaoCondicionalICMS.ProdutorAgropecuario:
                    case MotivoDesoneracaoCondicionalICMS.FrotistaLocadora:
                    case MotivoDesoneracaoCondicionalICMS.DiplomaticoConsular:
                    case MotivoDesoneracaoCondicionalICMS.AmazoniaOcidentalAreaLivreComercio:
                    case MotivoDesoneracaoCondicionalICMS.SUFRAMA:
                    case MotivoDesoneracaoCondicionalICMS.VendaOrgaoPublico:
                    case MotivoDesoneracaoCondicionalICMS.Outros:
                    case MotivoDesoneracaoCondicionalICMS.DeficienteCondutor:
                    case MotivoDesoneracaoCondicionalICMS.DeficienteNaoCondutor:
                        _motivoDesoneracao = value;
                        break;

                    default:
                        throw new InvalidOperationException(
                            "O valor informado não é válido. Veja a documentação para valores aceitos neste cenário.");
                }
            }
        }

        protected override void SerializeInternal(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("ICMS40");

            writer.WriteElementString("orig", Origem.GetEnumValue());
            writer.WriteElementString("CST", ((int)CST).ToString("00"));

            if (MotivoDesoneracaoIcms.HasValue)
            {
                writer.WriteElementString("vICMSDeson", (ValorIcmsDesoneracao ?? 0m).ToTDec_1302());
                writer.WriteElementString("motDesICMS", MotivoDesoneracaoIcms.Value.GetEnumValue());
            }

            writer.WriteEndElement();
        }
    }
}