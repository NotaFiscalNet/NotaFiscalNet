using System;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Atributo auxiliar no processo de conversão de tipos da NF-e.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    internal class NFeFieldAttribute : Attribute
    {
        private string _fieldName = string.Empty;
        private string _id = string.Empty;
        private string _dataType = string.Empty;
        private int _minLength;
        private int _maxLength;
        private string _pattern = string.Empty;
        private bool _opcional = false;
        private XmlNodeType _nodeType = XmlNodeType.Element;

        public NFeFieldAttribute()
        {
        }

        public NFeFieldAttribute(string fieldName, string dataType, string id)
        {
            _fieldName = fieldName;
            _dataType = dataType;
            _id = id;
        }

        /// <summary>
        /// Retorna ou define o Nome do campo contido no leiaute xml da Nota Fiscal Eletrônica.
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        /// <summary>
        /// Retorna ou define o identificador do campo contigo no leiaute xml da Nota Fiscal Eletrônica.
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Retorna ou define o tipo de dado da NF-e.
        /// </summary>
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        /// <summary>
        /// Retorna ou define o tamanho mínimo de caracteres de um campo.
        /// </summary>
        public int MinLength
        {
            get { return _minLength; }
            set
            {
                if (value >= 0)
                    _minLength = value;
                else
                    _minLength = 0;
            }
        }

        /// <summary>
        /// Retorna ou define o tamanho máximo de caracteres de um campo.
        /// </summary>
        public int MaxLength
        {
            get { return _maxLength; }
            set
            {
                if (value >= 0)
                    _maxLength = value;
                else
                    _maxLength = 0;
            }
        }

        /// <summary>
        /// Retorna ou define o padrão (expressão regular) usado para validar o conteúdo do campo.
        /// </summary>
        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        /// <summary>
        /// Retorna ou define o valor indicando se o campo é de preenchimento opcional ou obrigatório.
        /// </summary>
        public bool Opcional
        {
            get { return _opcional; }
            set { _opcional = value; }
        }

        /// <summary>
        /// Retorna ou define o tipo de nó xml do campo.
        /// </summary>
        public XmlNodeType NodeType
        {
            get { return _nodeType; }
            set { _nodeType = value; }
        }
    }
}