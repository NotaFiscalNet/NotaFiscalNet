using System;

namespace NotaFiscalNet.Core.Validacao
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
    internal class DefaultValidatorAttribute : Attribute
    {
        public DefaultValidatorAttribute(Type validatorType)
        {
            ValidatorType = validatorType;
        }

        public Type ValidatorType { get; private set; }
    }
}