using System.Xml;
using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Validacao;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalCteTests
    {
        [Theory]
        [InlineData("123", "ReferenciaDocumentoFiscalCte1.xml")]
        [InlineData("789", "ReferenciaDocumentoFiscalCte2.xml")]
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