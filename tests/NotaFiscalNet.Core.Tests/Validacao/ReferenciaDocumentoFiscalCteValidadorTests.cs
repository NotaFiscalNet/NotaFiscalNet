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
    public class ReferenciaDocumentoFiscalCteValidadorTests
    {
        private readonly ReferenciaDocumentoFiscalCteValidador _validador = new ReferenciaDocumentoFiscalCteValidador();

        [Fact]
        public void DeveMostrarErroSeNaoInformarReferenciaCte()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.ReferenciaCte, null as string)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("notempty_error", erro.ErrorCode);
        }

        [Fact]
        public void DeveMostrarErroSeInformarReferenciaCteComMaisDe44Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.ReferenciaCte, new string('1', 45))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("exact_length_error", erro.ErrorCode);
            Assert.Equal(44, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DeveMostrarErroSeInformarReferenciaCteComMenosDe44Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.ReferenciaCte, new string('1', 43))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("exact_length_error", erro.ErrorCode);
            Assert.Equal(44, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DevePermitirInformarReferenciaCteCom44Caracteres()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.ReferenciaCte, new string('1', 44));
        }
    }
}