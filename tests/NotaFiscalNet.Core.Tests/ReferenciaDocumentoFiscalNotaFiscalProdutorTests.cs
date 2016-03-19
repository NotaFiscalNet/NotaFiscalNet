using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Tests.Dados;
using System;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalNotaFiscalProdutorTests
    {
        [Fact]
        public void DeveCriarUmaReferenciaNaoModificada()
        {
            var referencia = new ReferenciaDocumentoFiscalNotaFiscalProdutor();
            Assert.False(referencia.Modificado);
        }

        [Fact]
        public void DeveModificarUmaReferencia()
        {
            var referencia = new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                CodigoModeloDocumentoFiscal = "04"
            };
            Assert.True(referencia.Modificado);
        }

        private readonly RepositorioReferenciaDocumentoFiscalNotaFiscalProdutor _repositorio;

        public ReferenciaDocumentoFiscalNotaFiscalProdutorTests()
        {
            _repositorio = new RepositorioReferenciaDocumentoFiscalNotaFiscalProdutor();
        }

        [Theory]
        [InlineData("Referencia1", "<refNFP><cUF>12</cUF><AAMM>2010</AAMM><CNPJ>010010010000101</CNPJ><IE>20</IE><mod>01</mod><serie>0</serie><nNF>1</nNF></refNFP>")]
        [InlineData("Referencia2", "<refNFP><cUF>51</cUF><AAMM>1605</AAMM><CPF>00100100101</CPF><IE>10</IE><mod>04</mod><serie>10</serie><nNF>10</nNF></refNFP>")]
        public void DeveSerializarUmaReferenciaDocumentoFiscalNotaFiscalProdutor(string chaveReferencia, string xml)
        {
            var referencia = _repositorio.Referencias[chaveReferencia];
            var resultado = new Serializador(referencia, null).Executar();
            Assert.Equal(xml, resultado);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void DeveImpedirModeloDocumentoFiscalInexistente(string codigoModeloFiscal)
        {
            var exception = Record.Exception(() => new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                CodigoModeloDocumentoFiscal = codigoModeloFiscal
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.Contains("O código do modelo de Documento Fiscal informado é inválido", exception.Message);
        }

        [Theory]
        [InlineData("01")]
        [InlineData("04")]
        public void DevePermitirTodosModeloDocumentoFiscalExistente(string codigoModeloFiscal)
        {
            var exception = Record.Exception(() => new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                CodigoModeloDocumentoFiscal = codigoModeloFiscal
            });

            Assert.Null(exception);
        }

        [Fact]
        public void DevePermitirInscricaoEstadualIsento()
        {
            var referencia = new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                InscricaoEstadual = "ISENTO"
            };

            Assert.Equal("ISENTO", referencia.InscricaoEstadual);
        }

        [Theory]
        [InlineData("20")]
        [InlineData("202")]
        [InlineData("2332")]
        [InlineData("12345678901234")]
        public void DevePermitirInscricaoEstatualNumericaDe2A14Caracteres(string inscricaoEstadual)
        {
            var exception = Record.Exception(() => new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                InscricaoEstadual = inscricaoEstadual
            });

            Assert.Null(exception);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("123456789012345")]
        public void DeveImpedirInscricaoEstadualForaDoTamanho(string inscricaoEstadual)
        {
            var exception = Record.Exception(() => new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            {
                InscricaoEstadual = inscricaoEstadual
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.Contains("O valor informado não é válido.", exception.Message);
        }
    }
}