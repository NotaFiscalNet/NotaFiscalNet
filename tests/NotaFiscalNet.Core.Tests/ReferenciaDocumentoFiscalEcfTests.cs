﻿using NotaFiscalNet.Core.Tests.Comum;
using System;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalEcfTests
    {
        [Fact]
        public void DeveCriarUmaReferenciaNaoModificada()
        {
            var referencia = new ReferenciaDocumentoFiscalEcf();
            Assert.False(referencia.Modificado);
        }

        [Fact]
        public void DeveModificarUmaReferencia()
        {
            var referencia = new ReferenciaDocumentoFiscalEcf()
            {
                CodigoModeloDocumentoFiscal = "2B",
                NumeroContadorOrdemOperacao = 1,
                NumeroEcf = 2
            };
            Assert.True(referencia.Modificado);
        }

        [Theory]
        [InlineData("2B", 1, 2, "ReferenciaDocumentoFiscalEcf1.xml")]
        [InlineData("2C", 332327, 2, "ReferenciaDocumentoFiscalEcf2.xml")]
        [InlineData("2D", 4, 49304, "ReferenciaDocumentoFiscalEcf3.xml")]
        public void DeveSerializarUmaReferenciaFiscalEcf(string modelo, int ecf, int coo, string arquivoXml)
        {
            var referencia = new ReferenciaDocumentoFiscalEcf()
            {
                CodigoModeloDocumentoFiscal = modelo,
                NumeroEcf = ecf,
                NumeroContadorOrdemOperacao = coo
            };

            var resultado = new Serializador(referencia, null).Serializar();
            var xml = new CarregadorXml(arquivoXml).Carregar();
            Assert.Equal(xml, resultado);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void DeveImpedirModeloDocumentoFiscalInexistente(string codigoModeloFiscal)
        {
            var exception = Record.Exception(() => new ReferenciaDocumentoFiscalEcf()
            {
                CodigoModeloDocumentoFiscal = codigoModeloFiscal
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.Contains("O código do modelo de Documento Fiscal informado é inválido", exception.Message);
        }

        [Theory]
        [InlineData("2B")]
        [InlineData("2C")]
        [InlineData("2D")]
        public void DevePermitirTodosModeloDocumentoFiscalExistente(string codigoModeloFiscal)
        {
            var exception = Record.Exception(() => new ReferenciaDocumentoFiscalEcf()
            {
                CodigoModeloDocumentoFiscal = codigoModeloFiscal
            });

            Assert.Null(exception);
        }
    }
}