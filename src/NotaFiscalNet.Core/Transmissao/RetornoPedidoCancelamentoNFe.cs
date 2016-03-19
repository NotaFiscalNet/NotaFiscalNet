using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o Retorno do Pedido de Cancelamento de Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class RetornoPedidoCancelamentoNFe
    {
        /// <summary>
        /// Inicializa uma instância da classe com base no xml retornado como resposta do pedido de
        /// cancelamento de NF-e.
        /// </summary>
        /// <param name="xml">Xml retornado pelo WebService.</param>
        public RetornoPedidoCancelamentoNFe(string xml)
        {
            this.RetornoXml = xml;

            using (StringReader reader = new StringReader(xml))
            {
                XPathDocument xpathDoc = new XPathDocument(reader);
                XPathNavigator navigator = xpathDoc.CreateNavigator();

                // Escopo de namespace xml usado na busca XPath
                XmlNamespaceManager ns = new XmlNamespaceManager(navigator.NameTable);
                ns.AddNamespace("nfe", Constants.NamespacePortalFiscalNFe);

                XPathNavigator retCancNFeNode = navigator.SelectSingleNode("/nfe:retCancNFe", ns);

                // versão do leiaute
                this.VersaoLeiaute = retCancNFeNode.SelectSingleNode("@versao", ns).Value;

                // infCanc
                XPathNavigator infCancNode = navigator.SelectSingleNode("/nfe:retCancNFe/nfe:infCanc", ns);

                // tpAmb
                this.Ambiente = (TipoAmbiente)infCancNode.SelectSingleNode("nfe:tpAmb", ns).ValueAsInt;

                // verAplic
                this.VersaoAplicacao = infCancNode.SelectSingleNode("nfe:verAplic", ns).Value;

                // cStat
                this.Status = infCancNode.SelectSingleNode("nfe:cStat", ns).Value;

                // xMotivo
                this.Motivo = infCancNode.SelectSingleNode("nfe:xMotivo", ns).Value;

                // cUF
                this.UFIBGE = (UfIBGE)infCancNode.SelectSingleNode("nfe:cUF", ns).ValueAsInt;

                XPathNavigator node;

                // chNFe
                node = infCancNode.SelectSingleNode("nfe:chNFe", ns);
                if (node != null)
                    this.ChaveAcessoNFe = node.Value;

                // dhRecbto
                node = infCancNode.SelectSingleNode("nfe:dhRecbto", ns);
                if (node != null)
                    this.RecebidoEm = node.ValueAsDateTime;

                // nProt
                node = infCancNode.SelectSingleNode("nfe:nProt", ns);
                if (node != null)
                    this.NumeroProtocoloStatusNFe = node.Value;
            }
        }

        /// <summary>
        /// Retorna o xml devolvido pelo WebService da Sefaz de envio.
        /// </summary>
        public string RetornoXml { get; private set; }

        /// <summary>
        /// Retorna o valor indicando o tipo de ambiente que o retorno se refere.
        /// </summary>
        public TipoAmbiente Ambiente { get; private set; }

        /// <summary>
        /// Retorna a versão do leiaute de retorno do recibo.
        /// </summary>
        public string VersaoLeiaute { get; private set; }

        /// <summary>
        /// Retorna a versão da aplicação que processa o lote.
        /// </summary>
        public string VersaoAplicacao { get; private set; }

        /// <summary>
        /// Retorna o código do status de envio do lote.
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// Retorna a descrição referente ao código do status de envio do lote.
        /// </summary>
        public string Motivo { get; private set; }

        /// <summary>
        /// Retorna a código da UF IBGE onde o lote foi entregue.
        /// </summary>
        public UfIBGE UFIBGE { get; private set; }

        /// <summary>
        /// Retorna a Chave de Acesso da Nota Fiscal Eletrônica.
        /// </summary>
        public string ChaveAcessoNFe { get; private set; }

        /// <summary>
        /// Retorna a Data e Hora de recibimento do lote pela Sefaz.
        /// </summary>
        public DateTime RecebidoEm { get; private set; }

        /// <summary>
        /// Retorna o Número do Protocolo de Status da NF-e.
        /// </summary>
        public string NumeroProtocoloStatusNFe { get; private set; }

        /// <summary>
        /// Salva o retorno do pedido de Cancelamento de Notas Fiscais Eletrônicas em um arquivo xml.
        /// </summary>
        /// <remarks>O arquivo terá o seguinte nome: [Ano+Cnpj+ModeloDocumentoFiscal+Serie+NumeracaoInicialNF+NumeracaoFinalNF]-inu.xml.</remarks>
        /// <param name="caminho">Caminho onde o arquivo será salvo.</param>
        /// <returns>Retorna o caminho completo do arquivo xml gravado.</returns>
        public string SalvarXml(string caminho)
        {
            string path = Path.Combine(caminho, this.NumeroProtocoloStatusNFe + "-can.xml");

            using (StringReader sr = new StringReader(this.RetornoXml))
            using (XmlReader reader = XmlReader.Create(sr))
            {
                reader.MoveToContent();
                string outerXml = reader.ReadOuterXml(); // pega o elemento raiz, desconsidera a declaração, se houver.

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;

                using (XmlWriter writer = XmlWriter.Create(path, settings))
                    writer.WriteRaw(outerXml);
            }

            return path;
        }
    }
}