using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Validacao;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class CompraTests
    {
        [Fact]
        public void DeveCriarUmaReferenciaNaoModificada()
        {
            var referencia = new Compra();
            Assert.False(referencia.Modificado);
        }

        [Fact]
        public void DeveModificarUmaReferencia()
        {
            var referencia = new Compra()
            {
                NotaEmpenho = "32392"
            };
            Assert.True(referencia.Modificado);
        }

        [Theory]
        [InlineData("123", null, null, "Compra1.xml")]
        [InlineData(null, "123", null, "Compra2.xml")]
        [InlineData(null, null, "123", "Compra3.xml")]
        [InlineData("123", "456", "789", "Compra4.xml")]
        public void DeveSerializarUmaCompra(string notaEmpenho, string pedido, string contrato, string arquivoXml)
        {
            var referencia = new Compra()
            {
                NotaEmpenho = notaEmpenho,
                Pedido = pedido,
                Contrato = contrato
            };

            var resultado = new Serializador(referencia, null).Serializar();
            var xml = new CarregadorXml(arquivoXml).Carregar();

            Assert.Equal(xml, resultado);
        }

    }
}