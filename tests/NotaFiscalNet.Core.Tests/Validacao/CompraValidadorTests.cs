using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using NotaFiscalNet.Core.Validacao.Validadores;
using Xunit;

namespace NotaFiscalNet.Core.Tests.Validacao
{
    public class CompraValidadorTests
    {
        private readonly CompraValidador _validador = new CompraValidador();
        
        [Fact]
        public void NaoDeveMostrarErroSeNaoInformarContrato()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.Contrato, null as string);
        }

        [Fact]
        public void NaoDeveMostrarErroSeNaoInformarNotaEmpenho()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.NotaEmpenho, null as string);
        }

        [Fact]
        public void NaoDeveMostrarErroSeNaoInformarPedido()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.Pedido, null as string);
        }

        [Fact]
        public void DeveMostrarErroSeInformarPedidoComMaisDe60Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.Pedido, new string('1', 61))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("LengthValidator", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["MinLength"]);
            Assert.Equal(60, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DevePermitirInformarPedidoAte60Caracteres()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.Pedido, new string('1', 60));
        }

        [Fact]
        public void DeveMostrarErroSeInformarContratoComMaisDe60Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.Contrato, new string('1', 61))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("LengthValidator", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["MinLength"]);
            Assert.Equal(60, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DevePermitirInformarContratoAte60Caracteres()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.Contrato, new string('1', 60));
        }

        [Fact]
        public void DeveMostrarErroSeInformarNotaEmpenhoComMaisDe60Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.NotaEmpenho, new string('1', 23))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("LengthValidator", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["MinLength"]);
            Assert.Equal(22, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DevePermitirInformarNotaEmpenhoAte22Caracteres()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.NotaEmpenho, new string('1', 22));
        }
    }
}
