using FluentValidation;
using NotaFiscalNet.Core.Validacao.FluentCustom;

namespace NotaFiscalNet.Core.Validacao.Validadores
{
    internal class ReferenciaDocumentoFiscalNfeValidador : AbstractValidator<ReferenciaDocumentoFiscalNfe>
    {
        public ReferenciaDocumentoFiscalNfeValidador()
        {
            RuleFor(t => t.ChaveAcessoNFe)
                .NotEmpty()
                .Length(44)
                .ChaveDeAcessoValida();
        }
    }
}