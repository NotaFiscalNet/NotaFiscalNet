using NotaFiscalNet.Core.Tests.Comum;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalNfeTests
    {
        [Fact]
        public void DeveCriarUmaReferenciaNaoModificada()
        {
            var referencia = new ReferenciaDocumentoFiscalNfe();
            Assert.False(referencia.Modificado);
        }

        [Fact]
        public void DeveModificarUmaReferencia()
        {
            var referencia = new ReferenciaDocumentoFiscalNfe()
            {
                ChaveAcessoNFe = "42100484684182000157550010000000020108042108"
            };
            Assert.True(referencia.Modificado);
        }

        [Theory]
        [InlineData("42100484684182000157550010000000020108042108", "ReferenciaDocumentoFiscalNfe1.xml")]
        public void DeveSerializarUmaReferenciaFiscalEcf(string chaveAcesso, string arquivoXml)
        {
            var referencia = new ReferenciaDocumentoFiscalNfe()
            {
                ChaveAcessoNFe = chaveAcesso
            };

            var resultado = new Serializador(referencia, null).Serializar();
            var xml = new CarregadorXml(arquivoXml).Carregar();
            Assert.Equal(xml, resultado);
        }
    }
}