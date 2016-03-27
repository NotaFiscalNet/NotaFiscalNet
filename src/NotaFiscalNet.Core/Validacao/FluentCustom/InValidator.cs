using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotaFiscalNet.Core.Validacao.FluentCustom
{
    public class InValidator<T> : PropertyValidator
    {
        private readonly IEnumerable<T> _enumerable;

        public InValidator(IEnumerable<T> enumerable)
            : base("'{PropertyName}' não está especificado na lista de valores aceitos.")
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable), "enumerable should not be null.");

            _enumerable = enumerable;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var item = (T)context.PropertyValue;
            var success = _enumerable.Contains(item);

            if (!success)
            {
                context.MessageFormatter.AppendArgument("ComparisonValue", string.Join(",", _enumerable));
                return false;
            }

            return true;
        }
    }
}