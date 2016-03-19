using System;
using System.Collections;
using System.Linq;

namespace NotaFiscalNet.Core.Validacao.Validators
{
    internal class RangeCollectionValidator : BaseValidator
    {
        public override void Validate(ValidationContext context, FieldMember field)
        {
            var value = field.GetValue();
            if (value.GetType().GetInterface("System.Collections.IEnumerable") == null)
            {
                throw new NotSupportedException("Só aceita IEnumerable.");
            }

            var enumerable = (IEnumerable) value;
            var quantidadeElementos = enumerable.Cast<object>().Count();
            
            if (field.Attribute.MinLength > 0 && quantidadeElementos < field.Attribute.MinLength)
                context.Add(ErroValidacao.Create(field.Attribute.ErrorKey, field.Attribute.MinLength));

            if (field.Attribute.MaxLength > 0 && quantidadeElementos > field.Attribute.MaxLength)
                context.Add(ErroValidacao.Create(field.Attribute.ErrorKey, field.Attribute.MaxLength));
        }
    }
}