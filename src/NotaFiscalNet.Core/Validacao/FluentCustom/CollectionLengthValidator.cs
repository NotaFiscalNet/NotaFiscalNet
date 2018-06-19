using FluentValidation.Validators;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace NotaFiscalNet.Core.Validacao.FluentCustom
{
    public class CollectionLengthValidator : PropertyValidator
    {
        private readonly int _max;
        private readonly int _min;

        public CollectionLengthValidator(int min, int max) : this(min, max, () => Mensagens.collectionlength_error)
        {
        }

        public CollectionLengthValidator(int min, int max, Expression<Func<string>> errorMessageResourceSelector)
            : base(errorMessageResourceSelector)
        {
            _max = max;
            _min = min;

            if (max != -1 && max < min)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min.");
            }
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var list = context.PropertyValue as ICollection;
            if (list == null) return true;

            var length = list.Count;

            if (length < _min || (length > _max && _max != -1))
            {
                context.MessageFormatter
                    .AppendArgument("MinLength", _min)
                    .AppendArgument("MaxLength", _max)
                    .AppendArgument("TotalLength", length);

                return false;
            }

            return true;
        }
    }
}