using FluentValidation;
using NotaFiscalNet.Core.Validacao.FluentCustom;

namespace NotaFiscalNet.Core.Validacao.Validadores
{
    internal class ReferenciaDocumentoFiscalNotaFiscalValidador : AbstractValidator<ReferenciaDocumentoFiscalNotaFiscal>
    {
        public ReferenciaDocumentoFiscalNotaFiscalValidador()
        {
            RuleFor(t => t.UnidadeFederativa)
                .IsInEnum()
                .NotEqual(UfIBGE.NaoEspecificado);

            RuleFor(t => t.MesAnoEmissao)
                .NotEmpty();

            RuleFor(t => t.Cnpj)
                .NotEmpty()
                .Length(14)
                .CnpjValido();

            RuleFor(t => t.CodigoModelo)
                .In("01");

            RuleFor(t => t.Serie)
                .InclusiveBetween(0, 999);

            RuleFor(t => t.Numero)
                .InclusiveBetween(0, 999999999);
        }
    }
}