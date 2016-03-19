using NotaFiscalNet.Core.Tests.Comum;
using System;
using System.Linq;
using Xunit;

namespace NotaFiscalNet.Core.Tests
{
    public class ReferenciaDocumentoFiscalCollectionTests
    {
        [Fact]
        public void DeveCriarUmaReferenciaNaoModificada()
        {
            var referencia = new ReferenciaDocumentoFiscalCollection();
            Assert.False(referencia.Modificado);
        }

        [Fact]
        public void DeveModificarUmaColecao()
        {
            var referencia = new ReferenciaDocumentoFiscalCollection()
            {
                new ReferenciaDocumentoFiscalNfe()
                {
                    ChaveAcessoNFe = "42100484684182000157550010000000020108042108"
                }
            };
            Assert.True(referencia.Modificado);
        }

        [Fact]
        public void DeveSerializarUmaReferenciaFiscalCollection()
        {
            var colecao = new ReferenciaDocumentoFiscalCollection()
            {
                new ReferenciaDocumentoFiscalNfe()
                {
                    ChaveAcessoNFe = "42100484684182000157550010000000020108042108"
                }
            };
            var xml = "<NFref><refNFe>42100484684182000157550010000000020108042108</refNFe></NFref>";

            var resultado = new Serializador(colecao, null).Executar();
            Assert.Equal(xml, resultado);
        }

        [Fact]
        public void NaoDeveSerializarUmaColecaoVazia()
        {
            var colecao = new ReferenciaDocumentoFiscalCollection();
            var xml = string.Empty;

            var resultado = new Serializador(colecao, null).Executar();
            Assert.Equal(xml, resultado);
        }

        [Fact]
        public void NaoDeveSerializarUmaColecaoNaoModificada()
        {
            var colecao = new ReferenciaDocumentoFiscalCollection() { new ReferenciaDocumentoFiscalNfe() };
            var xml = string.Empty;

            var resultado = new Serializador(colecao, null).Executar();
            Assert.Equal(xml, resultado);
        }

        [Fact]
        public void DeveImpedirAdicionarMaisQue500Items()
        {
            var colecao = new ReferenciaDocumentoFiscalCollection();
            var exception = Record.Exception(() => Enumerable.Range(1, 501).ToList().ForEach((numero) =>
            {
                colecao.Add(new ReferenciaDocumentoFiscalNfe());
            }));

            Assert.NotNull(exception);
            Assert.IsType<ApplicationException>(exception);
            Assert.Contains("A capacidade máxima deste campo é de", exception.Message);
        }

        [Fact]
        public void DevePermitirTodosTiposDeReferenciaExistente()
        {
            var colecao = new ReferenciaDocumentoFiscalCollection()
            {
                new ReferenciaDocumentoFiscalNfe(),
                new ReferenciaDocumentoFiscalEcf(),
                new ReferenciaDocumentoFiscalCte(),
                new ReferenciaDocumentoFiscalNotaFiscal(),
                new ReferenciaDocumentoFiscalNotaFiscalProdutor()
            };

            Assert.Equal(5, colecao.Count);
        }
    }
}