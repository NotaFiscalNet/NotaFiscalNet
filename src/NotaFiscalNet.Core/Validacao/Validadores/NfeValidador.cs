using FluentValidation;

namespace NotaFiscalNet.Core.Validacao.Validadores
{
    public class NfeValidador : AbstractValidator<NFe>
    {
        public NfeValidador()
        {
            RuleFor(n => n.Identificacao)
                .SetValidator(new IdentificacaoDocumentoFiscalValidador());

            RuleFor(t => t.Compras)
                .SetValidator(new CompraValidador());
        }
    }
}