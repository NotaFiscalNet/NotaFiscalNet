using FluentValidation;
using NotaFiscalNet.Core.Validacao.FluentCustom;

namespace NotaFiscalNet.Core.Validacao.Validadores
{
    internal class ReferenciaDocumentoFiscalNotaFiscalProdutorValidador :
        AbstractValidator<ReferenciaDocumentoFiscalNotaFiscalProdutor>
    {
        public ReferenciaDocumentoFiscalNotaFiscalProdutorValidador()
        {
            RuleFor(t => t.UnidadeFederativa)
                .IsInEnum()
                .NotEqual(UfIBGE.NaoEspecificado);

            RuleFor(t => t.MesAnoEmissao)
                .NotEmpty();

            RuleFor(t => t.CNPJ)
                .NotEmpty()
                .When(t => string.IsNullOrEmpty(t.CPF));

            RuleFor(t => t.CPF)
                .NotEmpty()
                .When(t => string.IsNullOrEmpty(t.CNPJ));

            RuleFor(t => t.InscricaoEstadual)
                .NotEmpty()
                .Matches("^(ISENTO|[0-9]{2,14})$");

            RuleFor(t => t.CodigoModelo)
                .In("01", "04");

            RuleFor(t => t.Serie)
                .InclusiveBetween(0, 999);

            RuleFor(t => t.Numero)
                .InclusiveBetween(0, 999999999);
        }
    }
}