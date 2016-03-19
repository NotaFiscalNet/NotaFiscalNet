using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o retorno do pedido de Inutilização de Nota Fiscais Eletrônicas.
    /// </summary>

    public sealed class RetornoPedidoInutilizacaoNFe
    {
        public RetornoPedidoInutilizacaoNFe(string xmlRetorno)
        {
            this.RetornoXml = xmlRetorno;

            using (StringReader reader = new StringReader(xmlRetorno))
            {
                XPathDocument xpathDoc = new XPathDocument(reader);
                XPathNavigator navigator = xpathDoc.CreateNavigator();

                // Escopo de namespace xml usado na busca XPath
                XmlNamespaceManager ns = new XmlNamespaceManager(navigator.NameTable);
                ns.AddNamespace("nfe", Constants.NamespacePortalFiscalNFe);

                XPathNavigator retInutNFeNode = navigator.SelectSingleNode("/nfe:retInutNFe", ns);

                // versão do leiaute
                this.VersaoLeiaute = retInutNFeNode.SelectSingleNode("@versao", ns).Value;

                // infInut
                XPathNavigator infInutNode = navigator.SelectSingleNode("/nfe:retInutNFe/nfe:infInut", ns);

                // tpAmb
                this.Ambiente = (TipoAmbiente)infInutNode.SelectSingleNode("nfe:tpAmb", ns).ValueAsInt;

                // verAplic
                this.VersaoAplicacao = infInutNode.SelectSingleNode("nfe:verAplic", ns).Value;

                // cStat
                this.Status = infInutNode.SelectSingleNode("nfe:cStat", ns).Value;

                // xMotivo
                this.Motivo = infInutNode.SelectSingleNode("nfe:xMotivo", ns).Value;

                // cUF
                this.UFIBGE = (UfIBGE)infInutNode.SelectSingleNode("nfe:cUF", ns).ValueAsInt;

                XPathNavigator node;

                // ano
                node = infInutNode.SelectSingleNode("nfe:ano", ns);
                if (node != null)
                    this.Ano = node.ValueAsInt;

                // CNPJ
                node = infInutNode.SelectSingleNode("nfe:CNPJ", ns);
                if (node != null)
                    this.CNPJ = node.Value;

                // mod
                node = infInutNode.SelectSingleNode("nfe:mod", ns);
                if (node != null)
                    this.ModeloDocumentoFiscal = node.Value;

                // serie
                node = infInutNode.SelectSingleNode("nfe:serie", ns);
                if (node != null)
                    this.Serie = node.ValueAsInt;

                // nNFIni
                node = infInutNode.SelectSingleNode("nfe:nNFIni", ns);
                if (node != null)
                    this.NumeracaoInicialNF = node.ValueAsInt;

                // nNFFin
                node = infInutNode.SelectSingleNode("nfe:nNFFin", ns);
                if (node != null)
                    this.NumeracaoFinalNF = node.ValueAsInt;

                // dhRecbto
                node = infInutNode.SelectSingleNode("nfe:dhRecbto", ns);
                if (node != null)
                    this.RecebidoEm = node.ValueAsDateTime;

                // nProt
                node = infInutNode.SelectSingleNode("nfe:nProt", ns);
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
        /// Retorna o ano de inutilização da numeração.
        /// </summary>
        public int Ano { get; private set; }

        /// <summary>
        /// Retorna o número do CNPJ do emitente.
        /// </summary>
        public string CNPJ { get; private set; }

        /// <summary>
        /// Retorna o Modelo do Documento Fiscal que será inutilizado.
        /// </summary>
        public string ModeloDocumentoFiscal { get; private set; }

        /// <summary>
        /// Retorna ou define a Série da NF-e.
        /// </summary>
        public int Serie { get; private set; }

        /// <summary>
        /// Retorna ou define a Numeração Inicial da NF-e.
        /// </summary>
        public int NumeracaoInicialNF { get; private set; }

        /// <summary>
        /// Retorna ou define a Numeração Final da NF-e.
        /// </summary>
        public int NumeracaoFinalNF { get; private set; }

        /// <summary>
        /// Retorna a Data e Hora de recibimento por parte da Sefaz.
        /// </summary>
        public DateTime RecebidoEm { get; private set; }

        /// <summary>
        /// Retorna o Número do Protocolo de Status da NF-e.
        /// </summary>
        public string NumeroProtocoloStatusNFe { get; private set; }

        /// <summary>
        /// Salva o retorno do Pedido de Inutilização de Notas Fiscais Eletrônicas em um arquivo xml.
        /// </summary>
        /// <remarks>O arquivo terá o seguinte nome: [UFIBGE+Ano+ModeloDocumentoFiscal+Serie+NumeracaoInicialNF+NumeracaoFinalNF]-ped-inu.xml.</remarks>
        /// <param name="caminho">Caminho onde o arquivo será salvo.</param>
        /// <returns>Retorna o caminho completo do arquivo xml gravado.</returns>
        public string SalvarXml(string caminho)
        {
            string filename = string.Concat((int)this.UFIBGE, this.Ano.ToString("00"), this.CNPJ, this.ModeloDocumentoFiscal, this.Serie.ToString("000"), this.NumeracaoInicialNF.ToString("000000000"), this.NumeracaoFinalNF.ToString("000000000"), "-ped.inu.xml");
            string path = Path.Combine(caminho, filename);

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