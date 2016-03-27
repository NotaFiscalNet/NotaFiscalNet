using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Validacao;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalNfeTests
    {
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