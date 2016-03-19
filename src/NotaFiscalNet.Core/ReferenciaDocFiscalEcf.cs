using System;
using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;

namespace NotaFiscalNet.Core
{
    
    
    
    public sealed class ReferenciaDocFiscalEcf : ISerializavel
    {
        #region Fields

        private string _codigoModeloDocFiscal = string.Empty;
        private int _numeroECF;
        private int _numeroCOO;

        private bool _isReadOnly = false;

        #endregion Fields

        #region Properties

        /// <summary>
        /// [mod] Retorna o C�digo do Modelo do Documento Fiscal Refer�nciado. Se o Documento Fiscal referenciado por uma Nota Fiscal Eletr�nica, o valor dever� ser '55'. 
        /// Caso contr�rio, se o Documento Fiscal for uma Nota Fiscal modelo 1 ou 1A, dever� ser informado '01'.
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
                        throw new ArgumentException("O c�digo do modelo de Documento Fiscal informado � inv�lido.");
                }
            }
        }

        /// <summary>
        /// [nECF] Retorna ou define o n�mero de ordem seq�encial do ECF que emitiu o Cupom Fiscal vinculado � NF-e.
        /// </summary>
        [NFeField(FieldName = "nECF", ID = "B20l", Pattern = @"[0-9]{1,3}")]
        [ValidateField(6, Validator = typeof(ReferenciaDocFiscalValidator))]
        public int NumeroECF
        {
            get { return _numeroECF; }
            set { _numeroECF = value; }
        }

        /// <summary>
        /// [nECF] Retorna ou define o N�mero do Contador de Ordem de Opera��o - COO vinculado � NF-e.
        /// </summary>
        [NFeField(FieldName = "nCOO", ID = "B20m", Pattern = @"[0-9]{1,6}")]
        [ValidateField(6, Validator = typeof(ReferenciaDocFiscalValidator))]
        public int NumeroCOO
        {
            get { return _numeroCOO; }
            set { _numeroCOO = value; }
        }

        /// <summary>
        /// Retorna o valor indicando se a Nota Fiscal Eletr�nica est� em modo somente-leitura.
        /// </summary>
        /// <remarks>
        /// A Nota Fiscal Eletr�nica estar� em modo somente-leitura quando for instanciada a partir de um arquivo assinado digitalmente.
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

        #region ISerializavel Members

        public void Serializar(System.Xml.XmlWriter writer, NFe nfe)
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