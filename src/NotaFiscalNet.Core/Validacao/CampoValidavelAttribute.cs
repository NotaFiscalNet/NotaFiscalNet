using System;

namespace NotaFiscalNet.Core.Validacao
{
    /// <summary>
    /// DEPRECATED - REMOVER
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CampoValidavelAttribute : Attribute
    {
        public CampoValidavelAttribute(int ordem)
        {
            Ordem = ordem;
        }

        public CampoValidavelAttribute(int ordem, ChaveErroValidacao chaveValidacao)
            : this(ordem)
        {
            ChaveValidacao = chaveValidacao;
        }

        /// <summary>
        /// Retorna a sequência de validação do campo.
        /// </summary>
        public int Ordem { get; private set; }

        /// <summary>
        /// Retorna ou define o valor do campo quando o mesmo não está preenchido.
        /// </summary>
        public object ValorNaoPreenchido { get; set; }

        /// <summary>
        /// Retorna ou define o tipo de regra de validação a ser empregado ao campo.
        /// </summary>
        public Type Validador { get; set; }

        /// <summary>
        /// Retorna ou define a chave de erro para o caso de ocorrer um erro de validação.
        /// </summary>
        public ChaveErroValidacao ChaveValidacao { get; set; }

        /// <summary>
        /// Retorna ou define o valor indicando se o campo deve ou não ser ignorado (não verificado)
        /// o campo.
        /// </summary>
        public bool Opcional { get; set; }

        public int TamanhoMinimo { get; set; }

        public int TamanhoMaximo { get; set; }
    }
}