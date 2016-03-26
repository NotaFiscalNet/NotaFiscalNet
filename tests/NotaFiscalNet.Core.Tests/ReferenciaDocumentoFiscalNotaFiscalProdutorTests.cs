using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Tests.Dados;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalNotaFiscalProdutorTests
    {
        private readonly RepositorioReferenciaDocumentoFiscalNotaFiscalProdutor _repositorio;

        public ReferenciaDocumentoFiscalNotaFiscalProdutorTests()
        {
            _repositorio = new RepositorioReferenciaDocumentoFiscalNotaFiscalProdutor();
        }

        [Theory]
        [InlineData("1", "ReferenciaDocumentoFiscalNotaFiscalProdutor1.xml")]
        [InlineData("2", "ReferenciaDocumentoFiscalNotaFiscalProdutor2.xml")]
        public void DeveSerializarUmaReferenciaDocumentoFiscalNotaFiscalProdutor(string chaveReferencia, string arquivoXml)
        {
            var referencia = _repositorio.Referencias[chaveReferencia];
            var resultado = new Serializador(referencia, null).Serializar();
            var xml = new CarregadorXml(arquivoXml).Carregar();
            Assert.Equal(xml, resultado);
        }
    }
}