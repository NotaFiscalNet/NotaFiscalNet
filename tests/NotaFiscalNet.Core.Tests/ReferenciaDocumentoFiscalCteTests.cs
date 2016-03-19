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
        [InlineData(123, "<refCTe>123</refCTe>")]
        [InlineData(456, "<refCTe>456</refCTe>")]
        [InlineData(789, "<refCTe>789</refCTe>")]
        public void DeveSerializarUmaReferenciaFiscalEcf(string cte, string xml)
        {
            var referencia = new ReferenciaDocumentoFiscalCte()
            {
                ReferenciaCte = cte
            };

            var resultado = new Serializador(referencia, null).Executar();
            Assert.Equal(xml, resultado);
        }
    }
}