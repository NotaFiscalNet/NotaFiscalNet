using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocFiscalNFProdutor : ISerializavel, IModificavel
    {
        private UfIBGE _uf;
        private DateTime _mesAno;
        private string _cnpj = string.Empty;
        private string _cpf = string.Empty;
        private string _inscricaoEstadual = string.Empty;
        private string _codigoModeloDocFiscal = string.Empty;
        private int _serie;
        private int _numeroNF;

        private bool _isReadOnly = false;

        /// <summary>
        /// [cUF] Retorna ou define o código da UF do emitente da Nota Fiscal (1 ou 1A)
        /// </summary>
        [NFeField(FieldName = "cUF", DataType = "TCodUfIBGE", ID = "B20b")]
        [ValidateField(2, Validator = typeof(ReferenciaDocFiscalValidator))]
        public UfIBGE UF
        {
            get { return _uf; }
            set { _uf = value; }
        }

        /// <summary>
        /// [AAMM] Retorna ou define o Mês e Ano de Emissão da Nota Fiscal (1 ou 1A).
        /// </summary>
        /// <remarks>
        /// A informação de dia não será considerada para este campo, podendo ser informado qualquer
        /// dia do mês/ano informado.
        /// </remarks>
        [NFeField(FieldName = "AAMM", DataType = "token", ID = "B20c", Pattern = "[0-9]{2}[0]{1}[1-9]{1}|[0-9]{2}[1]{1}[0-2]{1}")]
        [ValidateField(3, Validator = typeof(ReferenciaDocFiscalValidator))]
        public DateTime MesAnoEmissao
        {
            get { return _mesAno; }
            set { _mesAno = value; }
        }

        /// <summary>
        /// [CNPJ] Retorna ou define o CNPJ do emitente da Nota Fiscal (1 ou 1A) referenciada.
        /// </summary>
        [NFeField(FieldName = "CNPJ", DataType = "TCnpj", ID = "B20d", Pattern = "[0-9]{14}")]
        [ValidateField(4, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }

        /// <summary>
        /// [CPF] Retorna ou define o CPF do emitente da Nota Fiscal de Produtor (1 ou 1A) referenciada.
        /// </summary>
        [NFeField(FieldName = "CPF", DataType = "TCpf", ID = "B20e", Pattern = "[0-9]{11}")]
        [ValidateField(4, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string CPF
        {
            get { return _cpf; }
            set { _cpf = value; }
        }

        /// <summary>
        /// [CPF] Retorna ou define a Inscrição Estadual emitente da Nota Fiscal de Produtor (1 ou
        /// 1A) referenciada.
        /// </summary>
        [NFeField(FieldName = "IE", DataType = "TIeDest", ID = "B20f", Pattern = "ISENTO|[0-9]{2,14}")]
        [ValidateField(4, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set
            {
                if (!ValidationUtil.ValidateRegex(value, "ISENTO|[0-9]{2,14}"))
                    throw new ArgumentException("O valor informado não é válido. Informar 'ISENTO' ou números (mínimo 2 e no máximo 14 caracteres).");
                _inscricaoEstadual = value;
            }
        }

        /// <summary>
        /// [mod] Retorna o Código do Modelo do Documento Fiscal Referênciado. Se o Documento Fiscal
        /// referenciado por uma Nota Fiscal Eletrônica, o valor deverá ser '55'. Caso contrário, se
        /// o Documento Fiscal for uma Nota Fiscal modelo 1 ou 1A, deverá ser informado '01'.
        /// </summary>
        [NFeField(FieldName = "mod", DataType = "token", ID = "B20g")]
        [ValidateField(5, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string CodigoModeloDocFiscal
        {
            get { return _codigoModeloDocFiscal; }
            set
            {
                if (value == "01" || value == "04")
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
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado
        {
            get
            {
                return
                    UF != UfIBGE.NaoEspecificado ||
                    MesAnoEmissao != DateTime.MinValue ||
                    !string.IsNullOrEmpty(CNPJ) ||
                    !string.IsNullOrEmpty(CPF) ||
                    !string.IsNullOrEmpty(InscricaoEstadual) ||
                    !string.IsNullOrEmpty(CodigoModeloDocFiscal) ||
                    SerieNF != 0 ||
                    NumeroNF != 0;
            }
        }

        public void Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("refNFP");
            writer.WriteElementString("cUF", SerializationUtil.GetEnumValue<UfIBGE>(UF));
            writer.WriteElementString("AAMM", MesAnoEmissao.ToString("yyMM"));

            if (!String.IsNullOrEmpty(CNPJ))
                writer.WriteElementString("CNPJ", SerializationUtil.ToCNPJ(CNPJ));
            else
                writer.WriteElementString("CPF", SerializationUtil.ToCPF(CPF));

            writer.WriteElementString("IE", CodigoModeloDocFiscal);
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