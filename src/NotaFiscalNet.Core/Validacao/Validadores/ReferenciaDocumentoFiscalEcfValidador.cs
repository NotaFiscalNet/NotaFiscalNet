using FluentValidation;
using NotaFiscalNet.Core.Validacao.FluentCustom;

namespace NotaFiscalNet.Core.Validacao.Validadores
{
    internal class ReferenciaDocumentoFiscalEcfValidador : AbstractValidator<ReferenciaDocumentoFiscalEcf>
    {
        public ReferenciaDocumentoFiscalEcfValidador()
        {
            RuleFor(t => t.CodigoModelo)
                .NotEmpty()
                .In("2B", "2C", "2D");

            RuleFor(t => t.NumeroEcf)
                .InclusiveBetween(0, 999);

            RuleFor(t => t.NumeroContadorOrdemOperacao)
                .InclusiveBetween(0, 999999);
        }
    }
}