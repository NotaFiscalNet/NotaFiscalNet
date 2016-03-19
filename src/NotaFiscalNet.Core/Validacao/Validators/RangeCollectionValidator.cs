using System;
using System.Collections;

namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class RangeCollectionValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            object value = field.GetValue();
            int count = 0;
            if (value.GetType().GetInterface("System.Collections.IEnumerable") != null)
            {
                IEnumerable enumerable = (IEnumerable)value;
                foreach (object item in enumerable)
                    count++;
            }
            else
            {
                throw new NotSupportedException("Só aceita IEnumerable.");
            }

            if (field.Attribute.MinLength > 0 && count < field.Attribute.MinLength)
                context.Add(ErroValidacao.Create(field.Attribute.ErrorKey, field.Attribute.MinLength));

            if (field.Attribute.MaxLength > 0 && count > field.Attribute.MaxLength)
                context.Add(ErroValidacao.Create(field.Attribute.ErrorKey, field.Attribute.MaxLength));
        }
    }
}