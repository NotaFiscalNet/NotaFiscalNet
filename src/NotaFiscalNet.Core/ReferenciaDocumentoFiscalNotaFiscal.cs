using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Linq;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalNotaFiscal : ISerializavel, IReferenciaDocumentoFiscal
    {
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
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CNPJ { get; set; }

        /// <summary>
        /// [mod] Retorna o Código do Modelo do Documento Fiscal Referênciado. Utilizar 01 para NF
        /// modelo 1/1A
        /// </summary>
        [ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido)]
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
        /// [serie] Retorna ou define a Série do Documento Fiscal. Informar zero (0) se inexistente.
        /// Valor padrão 0.
        /// </summary>
        [ValidateField(6, true)]
        public int SerieNf { get; set; }

        /// <summary>
        /// [nNF] Retorna ou define o Número do Documento Fiscal
        /// </summary>
        [ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public int NumeroNf { get; set; }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => UnidadeFederativa != UfIBGE.NaoEspecificado ||
                                  MesAnoEmissao != DateTime.MinValue ||
                                  !string.IsNullOrEmpty(CNPJ) ||
                                  !string.IsNullOrEmpty(CodigoModeloDocumentoFiscal) ||
                                  SerieNf != 0 ||
                                  NumeroNf != 0;

        public void Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("refNF");
            writer.WriteElementString("cUF", SerializationUtil.GetEnumValue<UfIBGE>(UnidadeFederativa));
            writer.WriteElementString("AAMM", MesAnoEmissao.ToString("yyMM"));
            writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(CNPJ));
            writer.WriteElementString("mod", CodigoModeloDocumentoFiscal);
            writer.WriteElementString("serie", SerieNf.ToString());
            writer.WriteElementString("nNF", NumeroNf.ToString());
            writer.WriteEndElement(); // fecha refNF
        }
        
        private void ValidarCodigoModeloDocumentoFiscal(string valor)
        {
            var valoresValidos = new[] { "01" };
            if (!valoresValidos.Contains(valor))
                throw new ArgumentException("O código do modelo de Documento Fiscal informado é inválido.");
        }
    }
}