using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocFiscalNF : ISerializavel, IModificavel
    {
        private UfIBGE _UF;
        private int _serie;
        private int _numeroNF;
        private string _codigoModeloDocFiscal = string.Empty;
        private string _cnpj = string.Empty;
        private DateTime _mesAno;

        private bool _isReadOnly = false;

        /// <summary>
        /// [cUF] Retorna ou define o código da UF do emitente da Nota Fiscal (1 ou 1A)
        /// </summary>
        [NFeField(FieldName = "cUF", DataType = "TCodUfIBGE", ID = "B15")]
        [ValidateField(2, Validator = typeof(ReferenciaDocFiscalValidator))]
        public UfIBGE UF
        {
            get { return _UF; }
            set { _UF = value; }
        }

        /// <summary>
        /// [AAMM] Retorna ou define o Mês e Ano de Emissão da Nota Fiscal (1 ou 1A).
        /// </summary>
        /// <remarks>
        /// A informação de dia não será considerada para este campo, podendo ser informado qualquer
        /// dia do mês/ano informado.
        /// </remarks>
        [NFeField(FieldName = "AAMM", DataType = "token", ID = "B16", Pattern = "[0-9]{2}[0]{1}[1-9]{1}|[0-9]{2}[1]{1}[0-2]{1}")]
        [ValidateField(3, Validator = typeof(ReferenciaDocFiscalValidator))]
        public DateTime MesAnoEmissao
        {
            get { return _mesAno; }
            set { _mesAno = value; }
        }

        /// <summary>
        /// [CNPJ] Retorna ou define o CNPJ do emitente da Nota Fiscal (1 ou 1A) referenciada.
        /// </summary>
        [NFeField(FieldName = "CNPJ", DataType = "TCnpj", ID = "B17", Pattern = "[0-9]{14}")]
        [ValidateField(4, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }

        /// <summary>
        /// [mod] Retorna o Código do Modelo do Documento Fiscal Referênciado. Se o Documento Fiscal
        /// referenciado por uma Nota Fiscal Eletrônica, o valor deverá ser '55'. Caso contrário, se
        /// o Documento Fiscal for uma Nota Fiscal modelo 1 ou 1A, deverá ser informado '01'.
        /// </summary>
        [NFeField(FieldName = "mod", DataType = "token", ID = "B18")]
        [ValidateField(5, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string CodigoModeloDocFiscal
        {
            get { return _codigoModeloDocFiscal; }
            set
            {
                if (value == "01")
                    _codigoModeloDocFiscal = value;
                else
                    throw new ArgumentException("O código do modelo de Documento Fiscal informado é inválido.");
            }
        }

        /// <summary>
        /// [serie] Retorna ou define a Série da Nota Fiscal (modelo 1 ou 1A) referenciada. Informar
        /// zero (0) se inexistente. Valor padrão 0.
        /// </summary>
        [NFeField(FieldName = "serie", DataType = "TSerie", ID = "B19", Pattern = @"0|[1-9]{1}[0-9]{0,2}")]
        [ValidateField(6, Validator = typeof(ReferenciaDocFiscalValidator))]
        public int SerieNF
        {
            get { return _serie; }
            set { _serie = value; }
        }

        /// <summary>
        /// [nNF] Retorna ou define o Número da Nota Fical (modelo 1 ou 1A) referenciada.
        /// </summary>
        [NFeField(FieldName = "nNF", DataType = "TNF", ID = "B20", Pattern = @"[1-9]{1}[0-9]{0,8}")]
        [ValidateField(7, Validator = typeof(ReferenciaDocFiscalValidator))]
        public int NumeroNF
        {
            get { return _numeroNF; }
            set { _numeroNF = value; }
        }

        /// <summary>
        /// Retorna o valor indicando se a Nota Fiscal Eletrônica está em modo somente-leitura.
        /// </summary>
        /// <remarks>
        /// A Nota Fiscal Eletrônica estará em modo somente-leitura quando for instanciada a partir
        /// de um arquivo assinado digitalmente.
        /// </remarks>
        public bool IsReadOnly => _isReadOnly;

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => UF != UfIBGE.NaoEspecificado ||
                                  MesAnoEmissao != DateTime.MinValue ||
                                  !string.IsNullOrEmpty(CNPJ) ||
                                  !string.IsNullOrEmpty(CodigoModeloDocFiscal) ||
                                  SerieNF != 0 ||
                                  NumeroNF != 0;

        /// <summary>
        /// Inicializa uma nova instância da classe ReferenciaDocFiscalNFe
        /// </summary>
        public ReferenciaDocFiscalNF()
        {
        }

        public void Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("refNF");
            writer.WriteElementString("cUF", SerializationUtil.GetEnumValue<UfIBGE>(UF));
            writer.WriteElementString("AAMM", MesAnoEmissao.ToString("yyMM"));
            writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(CNPJ));
            writer.WriteElementString("mod", CodigoModeloDocFiscal);
            writer.WriteElementString("serie", SerieNF.ToString());
            writer.WriteElementString("nNF", NumeroNF.ToString());
            writer.WriteEndElement(); // fecha refNF
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }
    }
}