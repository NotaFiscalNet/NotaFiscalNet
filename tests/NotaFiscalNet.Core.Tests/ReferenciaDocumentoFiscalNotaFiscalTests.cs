using NotaFiscalNet.Core.Tests.Comum;
using System;
using System.Collections.Generic;
using NotaFiscalNet.Core.Tests.Dados;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalNotaFiscalTests
    {
        [Fact]
        public void DeveCriarUmaReferenciaNaoModificada()
        {
            var referencia = new ReferenciaDocumentoFiscalNotaFiscal();
            Assert.False(referencia.Modificado);
        }

        [Fact]
        public void DeveModificarUmaReferencia()
        {
            var referencia = new ReferenciaDocumentoFiscalNotaFiscal()
            {
                CodigoModeloDocumentoFiscal = "01"
            };
            Assert.True(referencia.Modificado);
        }

        private readonly RepositorioReferenciaDocumentoFiscalNotaFiscal _repositorio;

        public ReferenciaDocumentoFiscalNotaFiscalTests()
        {
            _repositorio = new RepositorioReferenciaDocumentoFiscalNotaFiscal();
        }

        [Theory]
        [InlineData("1", "<refNF><cUF>12</cUF><AAMM>2010</AAMM><CNPJ>010010010000101</CNPJ><mod>01</mod><serie>0</serie><nNF>1</nNF></refNF>")]
        [InlineData("2", "<refNF><cUF>51</cUF><AAMM>1605</AAMM><CNPJ>020020020000202</CNPJ><mod>01</mod><serie>10</serie><nNF>10</nNF></refNF>")]
        public void DeveSerializarUmaReferenciaDocumentoFiscalNotaFiscal(string chaveReferencia, string xml)
        {
            var referencia = _repositorio.Referencias[chaveReferencia];
            var resultado = new Serializador(referencia, null).Serializar();
            Assert.Equal(xml, resultado);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void DeveImpedirModeloDocumentoFiscalInexistente(string codigoModeloFiscal)
        {
            var exception = Record.Exception(() => new ReferenciaDocumentoFiscalNotaFiscal()
            {
                CodigoModeloDocumentoFiscal = codigoModeloFiscal
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.Contains("O código do modelo de Documento Fiscal informado é inválido", exception.Message);
        }

        [Theory]
        [InlineData("01")]
        public void DevePermitirTodosModeloDocumentoFiscalExistente(string codigoModeloFiscal)
        {
            var exception = Record.Exception(() => new ReferenciaDocumentoFiscalNotaFiscal()
            {
                CodigoModeloDocumentoFiscal = codigoModeloFiscal
            });

            Assert.Null(exception);
        }
    }
}