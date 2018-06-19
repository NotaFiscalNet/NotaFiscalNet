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
	    private int _minLength;
        private int _maxLength;

	    public NFeFieldAttribute()
        {
        }

        public NFeFieldAttribute(string fieldName, string dataType, string id)
        {
            FieldName = fieldName;
            DataType = dataType;
            ID = id;
        }

        /// <summary>
        /// Retorna ou define o Nome do campo contido no leiaute xml da Nota Fiscal Eletrônica.
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

	    /// <summary>
        /// Retorna ou define o identificador do campo contigo no leiaute xml da Nota Fiscal Eletrônica.
        /// </summary>
        public string ID { get; set; } = string.Empty;

	    /// <summary>
        /// Retorna ou define o tipo de dado da NF-e.
        /// </summary>
        public string DataType { get; set; } = string.Empty;

	    /// <summary>
        /// Retorna ou define o tamanho mínimo de caracteres de um campo.
        /// </summary>
        public int MinLength
        {
            get => _minLength;
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
            get => _maxLength;
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
        public string Pattern { get; set; } = string.Empty;

	    /// <summary>
        /// Retorna ou define o valor indicando se o campo é de preenchimento opcional ou obrigatório.
        /// </summary>
        public bool Opcional { get; set; } = false;

	    /// <summary>
        /// Retorna ou define o tipo de nó xml do campo.
        /// </summary>
        public XmlNodeType NodeType { get; set; } = XmlNodeType.Element;
    }
}