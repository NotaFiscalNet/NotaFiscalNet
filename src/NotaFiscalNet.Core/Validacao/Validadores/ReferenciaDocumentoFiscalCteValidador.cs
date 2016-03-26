using FluentValidation;

namespace NotaFiscalNet.Core.Validacao.Validadores
{
    internal class ReferenciaDocumentoFiscalCteValidador : AbstractValidator<ReferenciaDocumentoFiscalCte>
    {
        public ReferenciaDocumentoFiscalCteValidador()
        {
            RuleFor(t => t.ReferenciaCte)
                .NotEmpty()
                .Length(44);
        }
    }
}