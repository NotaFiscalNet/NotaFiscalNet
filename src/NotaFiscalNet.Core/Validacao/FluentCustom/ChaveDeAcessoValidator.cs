using FluentValidation.Validators;

namespace NotaFiscalNet.Core.Validacao.FluentCustom
{
    public class ChaveDeAcessoValidator : PropertyValidator
    {
        public ChaveDeAcessoValidator()
            : base("{PropertyName} é inválida!")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            // TODO: Adicionar validações dos valores da chave de acesso para os campos UfIbge, Cnpj,
            // Modalidade, TipoEmissao e Digito Verificador

            return true;
        }
    }
}