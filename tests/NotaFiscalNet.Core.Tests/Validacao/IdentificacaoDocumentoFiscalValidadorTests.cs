using System;
using System.Linq;
using FluentValidation.TestHelper;
using NotaFiscalNet.Core.Validacao.Validadores;
using Xunit;

namespace NotaFiscalNet.Core.Tests.Validacao
{
    public class IdentificacaoDocumentoFiscalValidadorTests
    {
        private readonly IdentificacaoDocumentoFiscalValidador _validador = new IdentificacaoDocumentoFiscalValidador();

        [Fact]
        public void DeveMostrarErroSeNaoInformarUnidadeFederativaEmitente()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.UnidadeFederativaEmitente, UfIBGE.NaoEspecificado)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("notequal_error", erro.ErrorCode);
        }

        [Fact]
        public void NaoDeveMostrarErroSeInformarUnidadeFederativaEmitente()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.UnidadeFederativaEmitente, UfIBGE.SP);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(9999999)]
        [InlineData(100000000)]
        public void DeveMostrarErroSeNaoInformarCodigoNumericoDentroRange(int codigoNumerico)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.CodigoNumerico, codigoNumerico)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("inclusivebetween_error", erro.ErrorCode);
            Assert.Equal(10000000, erro.FormattedMessagePlaceholderValues["From"]);
            Assert.Equal(99999999, erro.FormattedMessagePlaceholderValues["To"]);
        }

        [Theory]
        [InlineData(10000000)]
        [InlineData(99999999)]
        public void NaoDeveMostrarErroSeInformarCodigoNumericoDentroRange(int codigoNumerico)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.CodigoNumerico, codigoNumerico);
        }

        [Fact]
        public void DeveMostrarErroSeNaoInformarNaturezaOperacao()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.NaturezaOperacao, default(string))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("notempty_error", erro.ErrorCode);
        }

        [Fact]
        public void DeveMostrarErroSeInformarNaturezaOperacaoComMaisDe60Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.NaturezaOperacao, new string('1', 61))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("length_error", erro.ErrorCode);
            Assert.Equal(1, erro.FormattedMessagePlaceholderValues["MinLength"]);
            Assert.Equal(60, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DevePermitirInformarNaturezaOperacaoAte60Caracteres()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.NaturezaOperacao, new string('1', 60));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public void DeveMostrarErroSeNaoInformarSerieDentroRange(int serie)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.Serie, serie)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("inclusivebetween_error", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["From"]);
            Assert.Equal(999, erro.FormattedMessagePlaceholderValues["To"]);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public void NaoDeveMostrarErroSeInformarSerieDentroRange(int numero)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.Serie, numero);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1000000000)]
        public void DeveMostrarErroSeNaoInformarNumeroDentroRange(int numero)
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.Numero, numero)
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("inclusivebetween_error", erro.ErrorCode);
            Assert.Equal(1, erro.FormattedMessagePlaceholderValues["From"]);
            Assert.Equal(999999999, erro.FormattedMessagePlaceholderValues["To"]);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(999999999)]
        public void NaoDeveMostrarErroSeInformarNumeroDentroRange(int numero)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.Numero, numero);
        }

        [Fact]
        public void DeveMostrarErroSeNaoInformarDataEmissao()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.DataEmissao, default(DateTime))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("notempty_error", erro.ErrorCode);
        }

        [Fact]
        public void DeveMostrarErroSeInformarDataEntradaSaidaMinima()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.DataEntradaSaida, default(DateTime))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("notequal_error", erro.ErrorCode);
        }

        [Fact]
        public void NaoDeveMostrarErroSeNaoInformarDataEntradaSaida()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.DataEntradaSaida, default(DateTime?));
        }

        [Theory]
        [InlineData(999999)]
        [InlineData(10000000)]
        public void DeveMostrarErroSeNaoInformarCodigoMunicipioFatoGeradorDentroRange(int codigoMunicipioFatoGerador)
        {
            var erro =
                _validador.ShouldHaveValidationErrorFor(t => t.CodigoMunicipioFatoGerador, codigoMunicipioFatoGerador)
                    .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("inclusivebetween_error", erro.ErrorCode);
            Assert.Equal(1000000, erro.FormattedMessagePlaceholderValues["From"]);
            Assert.Equal(9999999, erro.FormattedMessagePlaceholderValues["To"]);
        }

        [Theory]
        [InlineData(1000000)]
        [InlineData(9999999)]
        public void NaoDeveMostrarErroSeInformarCodigoMunicipioFatoGeradorDentroRange(int codigoMunicipioFatoGerador)
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.CodigoMunicipioFatoGerador, codigoMunicipioFatoGerador);
        }


        [Fact]
        public void DeveMostrarErroSeNaoInformarVersaoAplicativoEmissao()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.VersaoAplicativoEmissao, default(string))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("notempty_error", erro.ErrorCode);
        }

        [Fact]
        public void DeveMostrarErroSeInformarVersaoAplicativoEmissaoComMaisDe20Caracteres()
        {
            var erro = _validador.ShouldHaveValidationErrorFor(t => t.VersaoAplicativoEmissao, new string('1', 21))
                .FirstOrDefault();

            Assert.NotNull(erro);
            Assert.Equal("length_error", erro.ErrorCode);
            Assert.Equal(1, erro.FormattedMessagePlaceholderValues["MinLength"]);
            Assert.Equal(20, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void DevePermitirInformarVersaoAplicativoEmissaoAte20Caracteres()
        {
            _validador.ShouldNotHaveValidationErrorFor(t => t.VersaoAplicativoEmissao, new string('1', 20));
        }

        [Theory]
        [InlineData(TipoEmissaoNFe.Contingencia)]
        [InlineData(TipoEmissaoNFe.ContingenciaOffLineNfce)]
        [InlineData(TipoEmissaoNFe.DeclaracaoPrevia)]
        [InlineData(TipoEmissaoNFe.FormularioSeguranca)]
        [InlineData(TipoEmissaoNFe.SefazVirtualAmbienteNacional)]
        [InlineData(TipoEmissaoNFe.SefazVirtualRioGrandeDoSul)]
        [InlineData(TipoEmissaoNFe.SistemaContigenciaAmbienteNacional)]
        public void DeveMostrarErroSeNaoInformarDataEntradaSaidaContingenciaEmTipoEmissaoNfeDiferenteNormal(
            TipoEmissaoNFe tipoEmissao)
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = tipoEmissao,
                DataHoraEntradaContingencia = default(DateTime?)
            });

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.DataHoraEntradaContingencia));

            Assert.NotNull(erro);
            Assert.Equal("notempty_error", erro.ErrorCode);
        }

        [Theory]
        [InlineData(TipoEmissaoNFe.Contingencia)]
        [InlineData(TipoEmissaoNFe.ContingenciaOffLineNfce)]
        [InlineData(TipoEmissaoNFe.DeclaracaoPrevia)]
        [InlineData(TipoEmissaoNFe.FormularioSeguranca)]
        [InlineData(TipoEmissaoNFe.SefazVirtualAmbienteNacional)]
        [InlineData(TipoEmissaoNFe.SefazVirtualRioGrandeDoSul)]
        [InlineData(TipoEmissaoNFe.SistemaContigenciaAmbienteNacional)]
        public void DeveMostrarErroSeInformarDataEntradaSaidaMinimaContingenciaEmTipoEmissaoNfeDiferenteNormal(
            TipoEmissaoNFe tipoEmissao)
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = tipoEmissao,
                DataHoraEntradaContingencia = default(DateTime)
            });

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.DataHoraEntradaContingencia));

            Assert.NotNull(erro);
            Assert.Equal("notequal_error", erro.ErrorCode);
        }

        [Theory]
        [InlineData(TipoEmissaoNFe.Contingencia)]
        [InlineData(TipoEmissaoNFe.ContingenciaOffLineNfce)]
        [InlineData(TipoEmissaoNFe.DeclaracaoPrevia)]
        [InlineData(TipoEmissaoNFe.FormularioSeguranca)]
        [InlineData(TipoEmissaoNFe.SefazVirtualAmbienteNacional)]
        [InlineData(TipoEmissaoNFe.SefazVirtualRioGrandeDoSul)]
        [InlineData(TipoEmissaoNFe.SistemaContigenciaAmbienteNacional)]
        public void NaoDeveMostrarErroSeInformarDataEntradaSaidaContingenciaEmTipoEmissaoNfeDiferenteNormal(
            TipoEmissaoNFe tipoEmissao)
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = tipoEmissao,
                DataHoraEntradaContingencia = DateTime.Today
            });

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.DataHoraEntradaContingencia));

            Assert.Null(erro);
        }

        [Fact]
        public void NaoDeveMostrarErroSeNaoInformarDataHoraEntradaContingenciaEmTipoEmissaoNFeNormal()
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = TipoEmissaoNFe.Normal,
                DataHoraEntradaContingencia = default(DateTime?)
            });

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.DataHoraEntradaContingencia));

            Assert.Null(erro);
        }

        [Fact]
        public void DeveMostrarErroSeInformarDataHoraEntradaContingenciaMinimaEmTipoEmissaoNFeNormal()
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = TipoEmissaoNFe.Normal,
                DataHoraEntradaContingencia = DateTime.MinValue
            });

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.DataHoraEntradaContingencia));

            Assert.NotNull(erro);
            Assert.Equal("null_error", erro.ErrorCode);
        }

        [Fact]
        public void DeveMostrarErroSeInformarDataHoraEntradaContingenciaEmTipoEmissaoNFeNormal()
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = TipoEmissaoNFe.Normal,
                DataHoraEntradaContingencia = DateTime.Now
            });

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.DataHoraEntradaContingencia));

            Assert.NotNull(erro);
            Assert.Equal("null_error", erro.ErrorCode);
        }


        [Theory]
        [InlineData(TipoEmissaoNFe.Contingencia)]
        [InlineData(TipoEmissaoNFe.ContingenciaOffLineNfce)]
        [InlineData(TipoEmissaoNFe.DeclaracaoPrevia)]
        [InlineData(TipoEmissaoNFe.FormularioSeguranca)]
        [InlineData(TipoEmissaoNFe.SefazVirtualAmbienteNacional)]
        [InlineData(TipoEmissaoNFe.SefazVirtualRioGrandeDoSul)]
        [InlineData(TipoEmissaoNFe.SistemaContigenciaAmbienteNacional)]
        public void DeveMostrarErroSeNaoInformarJustificativaEntradaContingenciaEmTipoEmissaoNfeDiferenteNormal(
            TipoEmissaoNFe tipoEmissao)
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = tipoEmissao,
                JustificativaEntradaContingencia = default(string)
            });

            var erro = results.Errors
                .FirstOrDefault(
                    t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.JustificativaEntradaContingencia));

            Assert.NotNull(erro);
            Assert.Equal("notempty_error", erro.ErrorCode);
        }

        [Theory]
        [InlineData(TipoEmissaoNFe.Contingencia)]
        [InlineData(TipoEmissaoNFe.ContingenciaOffLineNfce)]
        [InlineData(TipoEmissaoNFe.DeclaracaoPrevia)]
        [InlineData(TipoEmissaoNFe.FormularioSeguranca)]
        [InlineData(TipoEmissaoNFe.SefazVirtualAmbienteNacional)]
        [InlineData(TipoEmissaoNFe.SefazVirtualRioGrandeDoSul)]
        [InlineData(TipoEmissaoNFe.SistemaContigenciaAmbienteNacional)]
        public void
            DeveMostrarErroSeInformarJustificativaEntradaContingenciaComMenosDe15CaracteresEmTipoEmissaoNfeDiferenteNormal
            (TipoEmissaoNFe tipoEmissao)
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = tipoEmissao,
                JustificativaEntradaContingencia = new string('1', 14)
            });

            var erro = results.Errors
                .FirstOrDefault(
                    t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.JustificativaEntradaContingencia));

            Assert.NotNull(erro);
            Assert.Equal("length_error", erro.ErrorCode);
            Assert.Equal(15, erro.FormattedMessagePlaceholderValues["MinLength"]);
            Assert.Equal(256, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Theory]
        [InlineData(TipoEmissaoNFe.Contingencia)]
        [InlineData(TipoEmissaoNFe.ContingenciaOffLineNfce)]
        [InlineData(TipoEmissaoNFe.DeclaracaoPrevia)]
        [InlineData(TipoEmissaoNFe.FormularioSeguranca)]
        [InlineData(TipoEmissaoNFe.SefazVirtualAmbienteNacional)]
        [InlineData(TipoEmissaoNFe.SefazVirtualRioGrandeDoSul)]
        [InlineData(TipoEmissaoNFe.SistemaContigenciaAmbienteNacional)]
        public void
            DeveMostrarErroSeInformarJustificativaEntradaContingenciaComMaisDe256CaracteresEmTipoEmissaoNfeDiferenteNormal
            (TipoEmissaoNFe tipoEmissao)
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = tipoEmissao,
                JustificativaEntradaContingencia = new string('1', 257)
            });

            var erro = results.Errors
                .FirstOrDefault(
                    t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.JustificativaEntradaContingencia));

            Assert.NotNull(erro);
            Assert.Equal("length_error", erro.ErrorCode);
            Assert.Equal(15, erro.FormattedMessagePlaceholderValues["MinLength"]);
            Assert.Equal(256, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }

        [Fact]
        public void NaoDeveMostrarErroSeNaoInformarJustificativaEntradaContingenciaEmTipoEmissaoNfeNormal()
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = TipoEmissaoNFe.Normal,
                JustificativaEntradaContingencia = default(string)
            });

            var erro = results.Errors
                .FirstOrDefault(
                    t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.JustificativaEntradaContingencia));

            Assert.Null(erro);
        }

        [Fact]
        public void DeveMostrarErroSeInformarJustificativaEntradaContingenciaEmTipoEmissaoNfeNormal()
        {
            var results = _validador.Validate(new IdentificacaoDocumentoFiscal
            {
                TipoEmissao = TipoEmissaoNFe.Normal,
                JustificativaEntradaContingencia = new string('1', 1)
            });

            var erro = results.Errors
                .FirstOrDefault(
                    t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.JustificativaEntradaContingencia));

            Assert.NotNull(erro);
            Assert.Equal("null_error", erro.ErrorCode);
        }

        [Fact]
        public void NaoDeveMostrarErroSeNaoInformarReferenciaDocumentoFiscal()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();

            var results = _validador.Validate(identificacao);

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.ReferenciasDocumentoFiscais));

            Assert.Null(erro);
        }

        [Fact]
        public void NaoDeveMostrarErroSeInformar500ReferenciaDocumentoFiscal()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var listaCom500Referencias = Enumerable.Range(1, 500)
                .Select(i => new ReferenciaDocumentoFiscalNfe())
                .ToList();

            identificacao.ReferenciasDocumentoFiscais.AddRange(listaCom500Referencias);

            var results = _validador.Validate(identificacao);

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.ReferenciasDocumentoFiscais));

            Assert.Null(erro);
        }

        [Fact]
        public void DeveMostrarErroSeInformar501ReferenciaDocumentoFiscal()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var listaCom501Referencias = Enumerable.Range(1, 501)
                .Select(i => new ReferenciaDocumentoFiscalNfe())
                .ToList();

            identificacao.ReferenciasDocumentoFiscais.AddRange(listaCom501Referencias);

            var results = _validador.Validate(identificacao);

            var erro = results.Errors
                .FirstOrDefault(t => t.PropertyName == nameof(IdentificacaoDocumentoFiscal.ReferenciasDocumentoFiscais));

            Assert.NotNull(erro);
            Assert.Equal("collectionlength_error", erro.ErrorCode);
            Assert.Equal(0, erro.FormattedMessagePlaceholderValues["MinLength"]);
            Assert.Equal(500, erro.FormattedMessagePlaceholderValues["MaxLength"]);
        }
    }
}