using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Tests.Dados;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class AvulsaTests
    {
        private readonly RepositorioAvulsa _repositorio = new RepositorioAvulsa();

        [Theory]
        [InlineData("1", "Avulsa1.xml")]
        [InlineData("2", "Avulsa2.xml")]
        [InlineData("3", "Avulsa3.xml")]
        public void DeveSerializarUmaAvulsa(string chaveReferencia, string arquivoXml)
        {
            var referencia = _repositorio.Referencias[chaveReferencia];
            var resultado = new Serializador(referencia, null).Serializar();
            var xml = new CarregadorXml(arquivoXml).Carregar();
            Assert.Equal(xml, resultado);
        }
    }
}
