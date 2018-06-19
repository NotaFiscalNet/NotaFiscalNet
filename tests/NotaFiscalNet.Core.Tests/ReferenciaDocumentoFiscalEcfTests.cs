using NotaFiscalNet.Core.Tests.Comum;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalEcfTests
    {
        [Theory]
        [InlineData("2B", 1, 2, "ReferenciaDocumentoFiscalEcf1.xml")]
        [InlineData("2C", 332327, 2, "ReferenciaDocumentoFiscalEcf2.xml")]
        [InlineData("2D", 4, 49304, "ReferenciaDocumentoFiscalEcf3.xml")]
        public void DeveSerializarUmaReferenciaFiscalEcf(string modelo, int ecf, int coo, string arquivoXml)
        {
            var referencia = new ReferenciaDocumentoFiscalEcf()
            {
                CodigoModelo = modelo,
                NumeroEcf = ecf,
                NumeroContadorOrdemOperacao = coo
            };

            var resultado = new Serializador(referencia, null).Serializar();
            var xml = new CarregadorXml(arquivoXml).Carregar();
            Assert.Equal(xml, resultado);
        }
    }
}