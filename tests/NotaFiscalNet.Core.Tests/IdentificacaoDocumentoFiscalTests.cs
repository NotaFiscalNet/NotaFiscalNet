using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Tests.Dados;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class IdentificacaoDocumentoFiscalTests
    {
        [Fact]
        public void DeveCriarIdenficacaoDocumentoComValoresPadroes()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            Assert.InRange(identificacao.CodigoNumerico, 10000000, 99999999);
            Assert.Equal(IndicadorFormaPagamento.AVista, identificacao.FormaPagamento);
            Assert.Equal(TipoModalidadeDocumentoFiscal.Nfe, identificacao.ModalidadeDocumentoFiscal);
            Assert.Equal(TipoNotaFiscal.Saida, identificacao.TipoNotaFiscal);
            Assert.Equal(TipoFormatoImpressaoDanfe.Retrato, identificacao.TipoImpressao);
            Assert.Equal(TipoEmissaoNFe.Normal, identificacao.TipoEmissao);
            Assert.Equal(TipoAmbiente.Producao, identificacao.Ambiente);
            Assert.Equal(TipoFinalidade.Normal, identificacao.Finalidade);
            Assert.Equal(TipoProcessoEmissaoNFe.AplicativoContribuinte, identificacao.TipoProcessoEmissao);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(1000000)]
        [InlineData(100000000)]
        public void DeveImpedirCodigoNumeroForaDaFaixa(int codigoNumeroInvalido)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.CodigoNumerico = codigoNumeroInvalido);

            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Contains("Informe um valor maior ou igual a", exception.Message);
            Assert.Contains("e menor ou igual a", exception.Message);
        }

        [Theory]
        [InlineData(10000000)]
        [InlineData(88888888)]
        [InlineData(99999999)]
        public void DevePermitirCodigoNumeroDentroDaFaixa(int codigoNumero)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.CodigoNumerico = codigoNumero);

            Assert.Null(exception);
        }

        [Fact]
        public void DeveImpedirNaturezaOperacaoVazia()
        {
            var naturezaOperacaoInvalida = string.Empty;
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.NaturezaOperacao = naturezaOperacaoInvalida);

            Assert.NotNull(exception);
            Assert.IsType<ErroValidacaoNFeException>(exception);
            Assert.Contains("Informe um valor maior ou igual a", exception.Message);
            Assert.Contains("e menor ou igual a", exception.Message);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(99999)]
        public void DeveImpedirSerieInvalida(int serieInvalida)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.Serie = serieInvalida);

            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Contains("Informe um valor maior ou igual a", exception.Message);
            Assert.Contains("e menor ou igual a", exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999)]
        public void DevePermitirSerieValida(int serieValida)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.Serie = serieValida);

            Assert.Null(exception);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1000000000)]
        public void DeveImpedirNumeroDocumentoFiscalInvalido(int numeroDocumentoFiscalInvalido)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.NumeroDocumentoFiscal = numeroDocumentoFiscalInvalido);

            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Contains("Informe um valor maior ou igual a", exception.Message);
            Assert.Contains("e menor ou igual a", exception.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999999999)]
        public void DevePermitirNumeroDocumentoFiscalValido(int numeroDocumentoFiscalValido)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.NumeroDocumentoFiscal = numeroDocumentoFiscalValido);

            Assert.Null(exception);
        }

        [Fact]
        public void DeveImpedirDataEmissaoMinima()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.DataEmissao = DateTime.MinValue);

            Assert.NotNull(exception);
            Assert.IsType<ErroValidacaoNFeException>(exception);
            Assert.Contains("O valor informado não é valido", exception.Message);
        }

        [Fact]
        public void DeveImpedirDataEntradaSaidaMinima()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.DataEntradaSaida = DateTime.MinValue);

            Assert.NotNull(exception);
            Assert.IsType<ErroValidacaoNFeException>(exception);
            Assert.Contains("O valor informado não é valido", exception.Message);
        }

        [Fact]
        public void DevePermitirDataEntradaSaidaNula()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.DataEntradaSaida = null);

            Assert.Null(exception);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100000)]
        [InlineData(10000000)]
        public void DeveImpedirCodigoMunicipioFatoGeradorInvalido(int codigoMunicipioFatoGeradorInvalido)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.CodigoMunicipioFatoGerador = codigoMunicipioFatoGeradorInvalido);

            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Contains("Informe um valor maior ou igual a", exception.Message);
            Assert.Contains("e menor ou igual a", exception.Message);
        }

        [Theory]
        [InlineData(1000000)]
        [InlineData(9999999)]
        public void DevePermitirCodigoMunicipioFatoGeradorValido(int codigoMunicipioFatoGeradorValido)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.CodigoMunicipioFatoGerador = codigoMunicipioFatoGeradorValido);

            Assert.Null(exception);
        }

        [Fact]
        public void DeveImpedirVersaoAplicativoEmissaoVazia()
        {
            var versaoAplicativoEmissaoInvalida = string.Empty;
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.VersaoAplicativoEmissao = versaoAplicativoEmissaoInvalida);

            Assert.NotNull(exception);
            Assert.IsType<ErroValidacaoNFeException>(exception);
            Assert.Contains("Informe um valor maior ou igual a", exception.Message);
            Assert.Contains("e menor ou igual a", exception.Message);
        }

        [Fact]
        public void DeveImpedirDataHoraEntradaContingenciaMinima()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.DataHoraEntradaContingencia = DateTime.MinValue);

            Assert.NotNull(exception);
            Assert.IsType<ErroValidacaoNFeException>(exception);
            Assert.Contains("O valor informado não é valido", exception.Message);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("14 caracteres ")]
        public void DeveImpedirJustificativaEntradaContingenciaComTamanhoAbaixoDoLimite(string justificativaEntradaContingenciaInvalida)
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.JustificativaEntradaContingencia = justificativaEntradaContingenciaInvalida);

            Assert.NotNull(exception);
            Assert.IsType<ErroValidacaoNFeException>(exception);
            Assert.Contains("Informe um valor maior ou igual a", exception.Message);
            Assert.Contains("e menor ou igual a", exception.Message);
        }

        [Fact]
        public void DevePermitirAdicionarReferenciasDocumentoFiscais()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            var exception = Record.Exception(() => identificacao.ReferenciasDocumentoFiscais.Add(new ReferenciaDocumentoFiscalCte()));

            Assert.Null(exception);
            Assert.Equal(1, identificacao.ReferenciasDocumentoFiscais.Count);
        }

        private readonly RepositorioIdentificacaoDocumentoFiscal _repositorio;

        public IdentificacaoDocumentoFiscalTests()
        {
            _repositorio = new RepositorioIdentificacaoDocumentoFiscal();
        }

        [Theory]
        [InlineData("1", "IdentificacaoDocumentoFiscal1.xml")]
        [InlineData("2", "IdentificacaoDocumentoFiscal2.xml")]
        [InlineData("3", "IdentificacaoDocumentoFiscal3.xml")]
        [InlineData("4", "IdentificacaoDocumentoFiscal4.xml")]
        [InlineData("5", "IdentificacaoDocumentoFiscal5.xml")]
        public void DeveSerializarUmaIdentificacaoDocumentoFiscal(string chaveReferencia, string arquivoXml)
        {
            var referencia = _repositorio.Referencias[chaveReferencia];
            var nfe = new Mock<INFe>();
            nfe.Setup(n => n.DigitoVerificadorChaveAcesso).Returns(1);
            nfe.Setup(n => n.Identificacao.UnidadeFederativaEmitente).Returns(referencia.UnidadeFederativaEmitente);

            var resultado = new Serializador(referencia, nfe.Object).Serializar();
            
            var xml = new CarregadorXml(arquivoXml).Carregar();
            Assert.Equal(xml, resultado);
        }
    }
}
