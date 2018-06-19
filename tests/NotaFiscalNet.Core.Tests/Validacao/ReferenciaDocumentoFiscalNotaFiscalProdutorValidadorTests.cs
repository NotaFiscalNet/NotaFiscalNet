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
    public class ReferenciaDocumentoFiscalNotaFiscalProdutorValidadorTests
    {
        private readonly ReferenciaDocumentoFiscalNotaFiscalProdutorValidador _validador = new ReferenciaDocumentoFiscalNotaFiscalProdutorValidador();

        [Fact]
        public void DeveMostrarErroSeNaoInformarUnidadeFederativa()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.UnidadeFederativa, UfIBGE.NaoEspecificado)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("NotEqualValidator", erro.ErrorCode);
        }

        [Fact]
        public void NaoDeveMostrarErroSeInformarUnidadeFederativa()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.UnidadeFederativa, UfIBGE.SP);
        }

        [Fact]
        public void DeveMostrarErroSeNaoInformarMesAnoEmissao()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.MesAnoEmissao, DateTime.MinValue)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("NotEmptyValidator", erro.ErrorCode);
        }

        [Fact]
        public void NaoDeveMostrarErroSeInformarMesAnoEmissao()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.MesAnoEmissao, DateTime.Today);
        }

        //[Fact]
        //public void DeveMostrarErroSeNaoInformarCnpj()
        //{
        //    var erro = _validador.ShouldHaveValidationErrorFor(t => t.Cnpj, null as string)
        //        .FirstOrDefault();

        //    Assert.NotNull(erro);
        //    Assert.Equal("NotEmptyValidator", erro.ErrorCode);
        //}

        //[Fact]
        //public void DeveMostrarErroSeInformarCnpjComMaisDe14Caracteres()
        //{
        //    var erro = _validador.ShouldHaveValidationErrorFor(t => t.Cnpj, new string('1', 15))
        //        .FirstOrDefault();

        //    Assert.NotNull(erro);
        //    Assert.Equal("exact_length_error", erro.ErrorCode);
        //    Assert.Equal(14, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        //}

        //[Fact]
        //public void DeveMostrarErroSeInformarCnpjComMenosDe14Caracteres()
        //{
        //    var erro = _validador.ShouldHaveValidationErrorFor(t => t.Cnpj, new string('1', 13))
        //        .FirstOrDefault();

        //    Assert.NotNull(erro);
        //    Assert.Equal("exact_length_error", erro.ErrorCode);
        //    Assert.Equal(14, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        //}

        //[Fact]
        //public void DevePermitirInformarCnpjCom14Caracteres()
        //{
        //    _validador.ShouldNotHaveValidationErrorFor(t => t.Cnpj, new string('1', 14));
        //}

        [Fact]
        public void DevePermitirInscricaoEstadualIsento()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.InscricaoEstadual, "ISENTO");
        }

        [Theory]
        [InlineData("20")]
        [InlineData("202")]
        [InlineData("2332")]
        [InlineData("12345678901234")]
        public void DevePermitirInscricaoEstatualNumericaDe2A14Caracteres(string inscricaoEstadual)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.InscricaoEstadual, inscricaoEstadual);
        }

        [Fact]
        public void DeveMostrarErroSeNaoInformarInscricaoEstadual()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.InscricaoEstadual, null as string)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("NotEmptyValidator", erro.ErrorCode);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("-1")]
        public void DeveMostrarErroSeInformarInscricaoEstadualInvalida(string inscricaoEstadual)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.InscricaoEstadual, inscricaoEstadual)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("RegularExpressionValidator", erro.ErrorCode);
        }

        [Fact]
        public void DeveMostrarErroSeNaoInformarCodigoModeloDocumentoFiscal()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.CodigoModelo, null as string)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("01,04", erro.FormattedMessagePlaceholderValues["ComparisonValue"]);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("03")]
        public void DeveMostrarErroSeInformarCodigoModeloDocumentoFiscalInvalido(string codigoModeloDocumentoFiscal)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.CodigoModelo, codigoModeloDocumentoFiscal)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("01,04", erro.FormattedMessagePlaceholderValues["ComparisonValue"]);
        }

        [Theory]
        [InlineData("01")]
        [InlineData("04")]
        public void NaoDeveMostrarErroSeInformarCodigoModeloDocumentoFiscalValido(string codigoModeloDocumentoFiscal)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.CodigoModelo, codigoModeloDocumentoFiscal);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void DeveMostrarErroSeNaoInformarSerieDocumentoFiscalForaRange(int serieDocumentoFiscal)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.Serie, serieDocumentoFiscal)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("InclusiveBetweenValidator", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["From"]);
            Assert.Equal(999, erro.FormattedMessagePlaceholderValues["To"]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public void NaoDeveMostrarErroSeNaoInformarSerieDocumentoFiscalDentroRange(int serieDocumentoFiscal)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.Serie, serieDocumentoFiscal);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000000000)]
        public void DeveMostrarErroSeNaoInformarNumeroDocumentoFiscalForaRange(int numeroDocumentoFiscal)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.Numero, numeroDocumentoFiscal)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("InclusiveBetweenValidator", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["From"]);
            Assert.Equal(999999999, erro.FormattedMessagePlaceholderValues["To"]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999999999)]
        public void NaoDeveMostrarErroSeNaoInformarNumeroDocumentoFiscalDentroRange(int numeroDocumentoFiscal)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.Numero, numeroDocumentoFiscal);
        }

    }
}