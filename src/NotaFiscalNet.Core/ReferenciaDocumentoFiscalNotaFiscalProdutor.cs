using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;
using System.Linq;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalNotaFiscalProdutor : ISerializavel, IReferenciaDocumentoFiscal, IPossuiDocumentoIdentificador
    {
        private string _inscricaoEstadual;
        private string _codigoModeloDocumentoFiscal;

        /// <summary>
        /// [cUF] Retorna ou define o código da UF do emitente da Nota Fiscal (1 ou 1A)
        /// </summary>
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public UfIBGE UnidadeFederativa { get; set; } = UfIBGE.NaoEspecificado;

        /// <summary>
        /// [AAMM] Retorna ou define o Mês e Ano de Emissão da Nota Fiscal (1 ou 1A).
        /// </summary>
        /// <remarks>
        /// A informação de dia não será considerada para este campo, podendo ser informado qualquer
        /// dia do mês/ano informado.
        /// </remarks>
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public DateTime MesAnoEmissao { get; set; } = DateTime.MinValue;

        /// <summary>
        /// [CNPJ] Retorna ou define o CNPJ do emitente da Nota Fiscal (1 ou 1A) referenciada.
        /// </summary>
        [ValidateField(3, Validator = typeof(CNPJouCPFValidator))]
        public string CNPJ { get; set; }

        /// <summary>
        /// [CPF] Retorna ou define o CPF do emitente da Nota Fiscal de Produtor (1 ou 1A) referenciada.
        /// </summary>
        [ValidateField(3, Validator = typeof(CNPJouCPFValidator))]
        public string CPF { get; set; }

        /// <summary>
        /// [CPF] Retorna ou define a Inscrição Estadual emitente da Nota Fiscal de Produtor (1 ou
        /// 1A) referenciada.
        /// </summary>
        [ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set
            {
                ValidarInscricaoEstadual(value);
                _inscricaoEstadual = value;
            }
        }

        /// <summary>
        /// [mod] Retorna o Código do Modelo do Documento Fiscal Referênciado. Se o Documento Fiscal
        /// referenciado por uma Nota Fiscal Eletrônica, o valor deverá ser '55'. Caso contrário, se
        /// o Documento Fiscal for uma Nota Fiscal modelo 1 ou 1A, deverá ser informado '01'.
        /// </summary>
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CodigoModeloDocumentoFiscal
        {
            get { return _codigoModeloDocumentoFiscal; }
            set
            {
                ValidarCodigoModeloDocumentoFiscal(value);
                _codigoModeloDocumentoFiscal = value;
            }
        }

        /// <summary>
        /// [serie] Retorna ou define a Série da Nota Fiscal (modelo 1 ou 1A) referenciada. Informar
        /// zero (0) se inexistente. Valor padrão 0.
        /// </summary>
        [ValidateField(6, true)]
        public int SerieNf { get; set; }

        /// <summary>
        /// [nNF] Retorna ou define o Número da Nota Fical (modelo 1 ou 1A) referenciada.
        /// </summary>
        [ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public int NumeroNf { get; set; }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => UnidadeFederativa != UfIBGE.NaoEspecificado ||
                                  MesAnoEmissao != DateTime.MinValue ||
                                  !string.IsNullOrEmpty(CNPJ) ||
                                  !string.IsNullOrEmpty(CPF) ||
                                  !string.IsNullOrEmpty(InscricaoEstadual) ||
                                  !string.IsNullOrEmpty(CodigoModeloDocumentoFiscal) ||
                                  SerieNf != 0 ||
                                  NumeroNf != 0;

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("refNFP");
            writer.WriteElementString("cUF", SerializationUtil.GetEnumValue<UfIBGE>(UnidadeFederativa));
            writer.WriteElementString("AAMM", MesAnoEmissao.ToString("yyMM"));

            if (!string.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(CNPJ));
            else
                writer.WriteElementString("CPF", SerializationUtil.ToCPF(CPF));

            writer.WriteElementString("IE", InscricaoEstadual);
            writer.WriteElementString("mod", CodigoModeloDocumentoFiscal);
            writer.WriteElementString("serie", SerieNf.ToString());
            writer.WriteElementString("nNF", NumeroNf.ToString());
            writer.WriteEndElement();
        }

        private void ValidarInscricaoEstadual(string valor)
        {
            if (!ValidationUtil.ValidateRegex(valor, "^(ISENTO|[0-9]{2,14})$"))
                throw new ArgumentException("O valor informado não é válido. Informar 'ISENTO' ou números (mínimo 2 e no máximo 14 caracteres).");
        }

        private void ValidarCodigoModeloDocumentoFiscal(string valor)
        {
            var valoresValidos = new[] { "01", "04" };
            if (!valoresValidos.Contains(valor))
                throw new ArgumentException("O código do modelo de Documento Fiscal informado é inválido.");
        }
    }
}