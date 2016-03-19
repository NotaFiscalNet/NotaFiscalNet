using System;
using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;

namespace NotaFiscalNet.Core
{
    
    
    
    public sealed class ReferenciaDocFiscalEcf : INFeSerializable
    {
        #region Fields

        private string _codigoModeloDocFiscal = string.Empty;
        private int _numeroECF;
        private int _numeroCOO;

        private bool _isReadOnly = false;

        #endregion Fields

        #region Properties

        /// <summary>
        /// [mod] Retorna o Código do Modelo do Documento Fiscal Referênciado. Se o Documento Fiscal referenciado por uma Nota Fiscal Eletrônica, o valor deverá ser '55'. 
        /// Caso contrário, se o Documento Fiscal for uma Nota Fiscal modelo 1 ou 1A, deverá ser informado '01'.
        /// </summary>
        [NFeField(FieldName = "mod", DataType = "token", ID = "B20k")]
        [ValidateField(5, Validator = typeof(ReferenciaDocFiscalValidator))]
        public string CodigoModeloDocFiscal
        {
            get { return _codigoModeloDocFiscal; }
            set
            {
                switch (value)
                {
                    case "2B":
                    case "2C":
                    case "2D":
                        _codigoModeloDocFiscal = value;
                        break;
                    default:
                        throw new ArgumentException("O código do modelo de Documento Fiscal informado é inválido.");
                }
            }
        }

        /// <summary>
        /// [nECF] Retorna ou define o número de ordem seqüencial do ECF que emitiu o Cupom Fiscal vinculado à NF-e.
        /// </summary>
        [NFeField(FieldName = "nECF", ID = "B20l", Pattern = @"[0-9]{1,3}")]
        [ValidateField(6, Validator = typeof(ReferenciaDocFiscalValidator))]
        public int NumeroECF
        {
            get { return _numeroECF; }
            set { _numeroECF = value; }
        }

        /// <summary>
        /// [nECF] Retorna ou define o Número do Contador de Ordem de Operação - COO vinculado à NF-e.
        /// </summary>
        [NFeField(FieldName = "nCOO", ID = "B20m", Pattern = @"[0-9]{1,6}")]
        [ValidateField(6, Validator = typeof(ReferenciaDocFiscalValidator))]
        public int NumeroCOO
        {
            get { return _numeroCOO; }
            set { _numeroCOO = value; }
        }

        /// <summary>
        /// Retorna o valor indicando se a Nota Fiscal Eletrônica está em modo somente-leitura.
        /// </summary>
        /// <remarks>
        /// A Nota Fiscal Eletrônica estará em modo somente-leitura quando for instanciada a partir de um arquivo assinado digitalmente.
        /// </remarks>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    !string.IsNullOrEmpty(CodigoModeloDocFiscal) ||
                    NumeroECF != 0 ||
                    NumeroCOO != 0;
            }
        }

        #endregion Properties

        #region Constructor

        #endregion Constructor

        #region INFeSerializable Members

        public void Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("refECF");
            writer.WriteElementString("mod", CodigoModeloDocFiscal);
            writer.WriteElementString("nECF", NumeroECF.ToString());
            writer.WriteElementString("nCOO", NumeroCOO.ToString());
            writer.WriteEndElement(); // fecha refNF
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}