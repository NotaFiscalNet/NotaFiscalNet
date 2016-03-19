using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    public sealed class RetornoConsultaSituacaoAtualNFe
    {
        internal RetornoConsultaSituacaoAtualNFe(string xmlRetorno)
        {
            this.RetornoXml = xmlRetorno;

            using (StringReader reader = new StringReader(xmlRetorno))
            {
                XPathDocument xpathDoc = new XPathDocument(reader);
                XPathNavigator navigator = xpathDoc.CreateNavigator();

                // Escopo de namespace xml usado na busca XPath
                XmlNamespaceManager ns = new XmlNamespaceManager(navigator.NameTable);
                ns.AddNamespace("nfe", Constants.NamespacePortalFiscalNFe);

                XPathNavigator retConsSitNFeNode = navigator.SelectSingleNode("/nfe:retConsSitNFe", ns);

                // versão do leiaute
                this.VersaoLeiaute = retConsSitNFeNode.SelectSingleNode("@versao", ns).Value;

                // infCanc
                XPathNavigator infProtNode = navigator.SelectSingleNode("/nfe:retConsSitNFe/nfe:infProt", ns);

                // tpAmb
                this.Ambiente = (TipoAmbiente)infProtNode.SelectSingleNode("nfe:tpAmb", ns).ValueAsInt;

                // verAplic
                this.VersaoAplicacao = infProtNode.SelectSingleNode("nfe:verAplic", ns).Value;

                // cStat
                this.Status = infProtNode.SelectSingleNode("nfe:cStat", ns).Value;

                // xMotivo
                this.Motivo = infProtNode.SelectSingleNode("nfe:xMotivo", ns).Value;

                // cUF
                this.UFIBGE = (UfIBGE)infProtNode.SelectSingleNode("nfe:cUF", ns).ValueAsInt;

                XPathNavigator node;

                // chNFe
                node = infProtNode.SelectSingleNode("nfe:chNFe", ns);
                if (node != null)
                    this.ChaveAcessoNFe = node.Value;

                // dhRecbto
                node = infProtNode.SelectSingleNode("nfe:dhRecbto", ns);
                if (node != null)
                    this.RecebidoEm = node.ValueAsDateTime;

                // nProt
                node = infProtNode.SelectSingleNode("nfe:nProt", ns);
                if (node != null)
                    this.NumeroProtocoloStatusNFe = node.Value;

                // digVal
                node = infProtNode.SelectSingleNode("nfe:digVal", ns);
                if (node != null)
                    this.DigestValue = node.Value;
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
        /// Retorna a versão do leiaute de retorno.
        /// </summary>
        public string VersaoLeiaute { get; private set; }

        /// <summary>
        /// Retorna a versão da aplicação.
        /// </summary>
        public string VersaoAplicacao { get; private set; }

        /// <summary>
        /// Retorna o código do status da consulta.
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
        /// Retorna a Chave de Acesso da NF-e.
        /// </summary>
        public string ChaveAcessoNFe { get; private set; }

        /// <summary>
        /// Retorna a Data e Hora de recibimento por parte da Sefaz.
        /// </summary>
        public DateTime RecebidoEm { get; private set; }

        /// <summary>
        /// Retorna o Número do Protocolo de Status da NF-e.
        /// </summary>
        public string NumeroProtocoloStatusNFe { get; private set; }

        /// <summary>
        /// Retorna o DigestValue da NF-e processada. Utilizado para conferir a integridade da NF-e original.
        /// </summary>
        public string DigestValue { get; private set; }

        /// <summary>
        /// Salva o recibo de entrega do Lote de Notas Fiscais Eletrônicas em um arquivo xml.
        /// </summary>
        /// <remarks>O arquivo terá o seguinte nome: [UFIBGE+Ano+ModeloDocumentoFiscal+Serie+NumeracaoInicialNF+NumeracaoFinalNF]-ped-inu.xml.</remarks>
        /// <param name="caminho">Caminho onde o arquivo será salvo.</param>
        /// <returns>Retorna o caminho completo do arquivo xml gravado.</returns>
        public string SalvarXml(string caminho)
        {
            string filename = string.Concat(this.ChaveAcessoNFe, "-sit.xml");
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