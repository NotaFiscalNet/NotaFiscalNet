using FluentValidation.Validators;

namespace NotaFiscalNet.Core.Validacao.FluentCustom
{
    public class CnpjValidator : PropertyValidator
    {
        public CnpjValidator()
            : base("'{PropertyName}' é um CNPJ inválido!")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            // TODO: Validar CNPJ com Digito Verificador

            return true;
        }
    }
}