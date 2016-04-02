using Moq;
using NotaFiscalNet.Core.Tests.Comum;
using NotaFiscalNet.Core.Tests.Dados;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class IdentificacaoDocumentoFiscalTests
    {
        private readonly RepositorioIdentificacaoDocumentoFiscal _repositorio;

        public IdentificacaoDocumentoFiscalTests()
        {
            _repositorio = new RepositorioIdentificacaoDocumentoFiscal();
        }

        [Fact]
        public void DeveCriarIdenficacaoDocumentoComValoresPadroes()
        {
            var identificacao = new IdentificacaoDocumentoFiscal();
            Assert.InRange(identificacao.CodigoNumerico, 10000000, 99999999);
            Assert.Equal(IndicadorFormaPagamento.AVista, identificacao.FormaPagamento);
            Assert.Equal(TipoModalidadeDocumentoFiscal.Nfe, identificacao.ModalidadeDocumentoFiscal);
            Assert.Equal(TipoNotaFiscal.Saida, identificacao.TipoNotaFiscal);
            Assert.Equal(TipoFormatoImpressaoDanfe.Retrato, identificacao.TipoImpressao);
            Assert.Equal(TipoEmissaoNFe.Normal, identificacao.TipoEmissao);
            Assert.Equal(TipoAmbiente.Producao, identificacao.Ambiente);
            Assert.Equal(TipoFinalidade.Normal, identificacao.Finalidade);
            Assert.Equal(TipoProcessoEmissaoNFe.AplicativoContribuinte, identificacao.TipoProcessoEmissao);
        }

        [Theory]
        [InlineData("1", "IdentificacaoDocumentoFiscal1.xml")]
        [InlineData("2", "IdentificacaoDocumentoFiscal2.xml")]
        [InlineData("3", "IdentificacaoDocumentoFiscal3.xml")]
        [InlineData("4", "IdentificacaoDocumentoFiscal4.xml")]
        [InlineData("5", "IdentificacaoDocumentoFiscal5.xml")]
        public void DeveSerializarUmaIdentificacaoDocumentoFiscal(string chaveReferencia, string arquivoXml)
        {
            var referencia = _repositorio.Referencias[chaveReferencia];
            var nfe = new Mock<INFe>();
            nfe.Setup(n => n.DigitoVerificadorChaveAcesso).Returns(1);
            nfe.Setup(n => n.Identificacao.UnidadeFederativaEmitente).Returns(referencia.UnidadeFederativaEmitente);

            var resultado = new Serializador(referencia, nfe.Object).Serializar();

            var xml = new CarregadorXml(arquivoXml).Carregar();
            Assert.Equal(xml, resultado);
        }
    }
}