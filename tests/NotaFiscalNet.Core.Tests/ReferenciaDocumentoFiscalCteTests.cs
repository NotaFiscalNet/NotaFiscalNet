using System.Xml;
using NotaFiscalNet.Core.Tests.Comum;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalCteTests
    {
        [Fact]
        public void DeveCriarUmaReferenciaNaoModificada()
        {
            var referencia = new ReferenciaDocumentoFiscalCte();
            Assert.False(referencia.Modificado);
        }

        [Fact]
        public void DeveModificarUmaReferencia()
        {
            var referencia = new ReferenciaDocumentoFiscalCte()
            {
                ReferenciaCte = "32392"
            };
            Assert.True(referencia.Modificado);
        }

        [Theory]
        [InlineData(123, "ReferenciaDocumentoFiscalCte1.xml")]
        [InlineData(789, "ReferenciaDocumentoFiscalCte2.xml")]
        public void DeveSerializarUmaReferenciaFiscalEcf(string cte, string arquivoXml)
        {
            var referencia = new ReferenciaDocumentoFiscalCte()
            {
                ReferenciaCte = cte
            };

            var resultado = new Serializador(referencia, null).Serializar();
            var xml = new CarregadorXml(arquivoXml).Carregar();

            Assert.Equal(xml, resultado);
        }
    }
}