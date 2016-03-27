using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotaFiscalNet.Core.Validacao.FluentCustom
{
    internal class ValidadorPolimorfico<TBaseClass> : NoopPropertyValidator
    {
        private readonly Dictionary<Type, IValidator> _derivedValidators = new Dictionary<Type, IValidator>();

        public ValidadorPolimorfico<TBaseClass> Add<TDerived>(IValidator<TDerived> derivedValidator)
            where TDerived : TBaseClass
        {
            _derivedValidators[typeof(TDerived)] = derivedValidator;
            return this;
        }

        public override IEnumerable<ValidationFailure> Validate(PropertyValidatorContext context)
        {
            // bail out if the property is null
            if (context.PropertyValue == null) return Enumerable.Empty<ValidationFailure>();

            // Make sure property is of correct type
            var collection = context.PropertyValue as IEnumerable<TBaseClass>;
            if (collection == null) return Enumerable.Empty<ValidationFailure>();

            var results = new List<ValidationFailure>();

            var index = 0;
            foreach (var item in collection)
            {
                IValidator derivedValidator;

                // Find the validator to use based on actual type of the element.
                var actualType = item.GetType();
                if (_derivedValidators.TryGetValue(actualType, out derivedValidator))
                {
                    var newContext = context.ParentContext.Clone(instanceToValidate: item);
                    newContext.PropertyChain.Add(context.Rule.PropertyName);
                    newContext.PropertyChain.AddIndexer(index);

                    // Execute child validator.
                    results.AddRange(derivedValidator.Validate(newContext).Errors);
                }

                index++;
            }

            return results;
        }
    }
}