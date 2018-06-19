using System.Linq;
using FluentValidation.TestHelper;
using NotaFiscalNet.Core.Validacao.Validadores;
using Xunit;

namespace NotaFiscalNet.Core.Tests.Validacao
{
    public class ReferenciaDocumentoFiscalEcfValidadorTests
    {
        private readonly ReferenciaDocumentoFiscalEcfValidador _validador = new ReferenciaDocumentoFiscalEcfValidador();

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void DeveImpedirModeloDocumentoFiscalInexistente(string codigoModeloFiscal)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.CodigoModelo, codigoModeloFiscal)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("2B,2C,2D", erro.FormattedMessagePlaceholderValues["ComparisonValue"]);
        }

        [Theory]
        [InlineData("2B")]
        [InlineData("2C")]
        [InlineData("2D")]
        public void DevePermitirTodosModeloDocumentoFiscalExistente(string codigoModeloFiscal)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.CodigoModelo, codigoModeloFiscal);
        }

        [Fact]
        public void DeveMostrarErroSeNaoInformarCodigoModeloDocumentoFiscal()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.CodigoModelo, null as string)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("NotEmptyValidator", erro.ErrorCode);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void DeveMostrarErroSeNaoInformarNumeroEcfForaRange(int numeroEcf)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.NumeroEcf, numeroEcf)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("InclusiveBetweenValidator", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["From"]);
            Assert.Equal(999, erro.FormattedMessagePlaceholderValues["To"]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public void NaoDeveMostrarErroSeNaoInformarNumeroEcfDentroRange(int numeroEcf)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.NumeroEcf, numeroEcf);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000000)]
        public void DeveMostrarErroSeNaoInformarNumeroContadorOrdemOperacaoForaRange(int numeroContadorOrdemOperacao)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.NumeroContadorOrdemOperacao, numeroContadorOrdemOperacao)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("InclusiveBetweenValidator", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["From"]);
            Assert.Equal(999999, erro.FormattedMessagePlaceholderValues["To"]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999999)]
        public void NaoDeveMostrarErroSeNaoInformarNumeroContadorOrdemOperacaoDentroRange(int numeroContadorOrdemOperacao)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.NumeroContadorOrdemOperacao, numeroContadorOrdemOperacao);
        }

    }
}