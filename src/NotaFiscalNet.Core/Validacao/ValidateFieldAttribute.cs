using System;

namespace NotaFiscalNet.Core.Validacao
{
    

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    internal sealed class ValidateFieldAttribute : Attribute
    {
        public ValidateFieldAttribute(int sequence)
        {
            //if ( sequence <= 0 )
            //    throw new ArgumentOutOfRangeException("sequence");
            Sequence = sequence;
        }

        public ValidateFieldAttribute(int sequence, ChaveErroValidacao errorKey)
            : this(sequence)
        {
            ErrorKey = errorKey;
        }

        public ValidateFieldAttribute(int sequence, bool ignore)
            : this(sequence)
        {
            Ignore = true;
        }

        /// <summary>
        /// Retorna a sequência de validação do campo.
        /// </summary>
        public int Sequence { get; private set; }

        /// <summary>
        /// Retorna ou define o valor padrão do campo.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Retorna ou define o tipo de regra de validação a ser empregado ao campo.
        /// </summary>
        public Type Validator { get; set; }

        /// <summary>
        /// Retorna ou define a chave de erro para o caso de ocorrer um erro de validação.
        /// </summary>
        public ChaveErroValidacao ErrorKey { get; set; }

        /// <summary>
        /// Retorna ou define o valor indicando se o campo deve ou não ser ignorado (não verificado) o campo.
        /// </summary>
        public bool Ignore { get; set; }

        public int MinLength { get; set; }

        public int MaxLength { get; set; }
    }
}
