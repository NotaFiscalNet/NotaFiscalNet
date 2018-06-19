using System.Linq;
using FluentValidation.TestHelper;
using NotaFiscalNet.Core.Validacao.Validadores;
using Xunit;

namespace NotaFiscalNet.Core.Tests.Validacao
{
    public class ReferenciaDocumentoFiscalNfeValidadorTests
    {
        private readonly ReferenciaDocumentoFiscalNfeValidador _validador = new ReferenciaDocumentoFiscalNfeValidador();

        [Fact]
        public void DeveMostrarErroSeNaoInformarChaveAcessoNfe()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.ChaveAcessoNFe, null as string)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("NotEmptyValidator", erro.ErrorCode);
        }

        [Fact]
        public void DeveMostrarErroSeInformarChaveAcessoNfeComMaisDe44Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.ChaveAcessoNFe, new string('1', 45))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("ExactLengthValidator", erro.ErrorCode);
            Assert.Equal(44, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DeveMostrarErroSeInformarChaveAcessoNfeComMenosDe44Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.ChaveAcessoNFe, new string('1', 43))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("ExactLengthValidator", erro.ErrorCode);
            Assert.Equal(44, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DevePermitirInformarChaveAcessoNfeCom44Caracteres()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.ChaveAcessoNFe, new string('1', 44));
        }
    }
}