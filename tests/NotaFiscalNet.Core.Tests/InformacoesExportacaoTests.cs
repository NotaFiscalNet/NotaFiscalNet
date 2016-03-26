using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Tests.Dados;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class InformacoesExportacaoTests
    {
        private readonly RepositorioInformacoesExportacao _repositorio = 
            new RepositorioInformacoesExportacao();

        [Theory]
        [InlineData("1", "InformacoesExportacao1.xml")]
        [InlineData("2", "InformacoesExportacao2.xml")]
        public void DeveSerializarUmaIdentificacaoDocumentoFiscal(string chaveReferencia, string arquivoXml)
        {
            var referencia = _repositorio.Referencias[chaveReferencia];
            var resultado = new Serializador(referencia, null).Serializar();
            var xml = new CarregadorXml(arquivoXml).Carregar();
            Assert.Equal(xml, resultado);
        }
    }
}
