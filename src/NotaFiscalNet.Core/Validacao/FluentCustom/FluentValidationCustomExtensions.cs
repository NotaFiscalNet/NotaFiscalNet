using FluentValidation;
using System.Collections;

namespace NotaFiscalNet.Core.Validacao.FluentCustom
{
    public static class FluentValidationCustomExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> In<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
            params TProperty[] valores)
        {
            return ruleBuilder.SetValidator(new InValidator<TProperty>(valores));
        }

        public static IRuleBuilderOptions<T, ICollection> CollectionLength<T>(
            this IRuleBuilder<T, ICollection> ruleBuilder, int min, int max)
        {
            return ruleBuilder.SetValidator(new CollectionLengthValidator(min, max));
        }

        public static IRuleBuilderOptions<T, ICollection> CollectionLength<T>(
            this IRuleBuilder<T, ICollection> ruleBuilder, int exactLength)
        {
            return ruleBuilder.SetValidator(new CollectionExactLengthValidator(exactLength));
        }

        public static IRuleBuilderOptions<T, TProperty> ChaveDeAcessoValida<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new ChaveDeAcessoValidator());
        }

        public static IRuleBuilderOptions<T, string> CnpjValido<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CnpjValidator());
        }
    }
}