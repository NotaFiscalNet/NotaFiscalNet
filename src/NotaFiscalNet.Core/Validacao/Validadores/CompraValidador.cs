using FluentValidation;

namespace NotaFiscalNet.Core.Validacao.Validadores
{
    internal class CompraValidador : AbstractValidator<Compra>
    {
        public CompraValidador()
        {
            RuleFor(n => n.NotaEmpenho)
                .Length(0, 22);

            RuleFor(n => n.Contrato)
                .Length(0, 60);

            RuleFor(n => n.Pedido)
                .Length(0, 60);
        }
    }
}