using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o Retorno da Consulta de Recibo de Entrega de Lote de Notas Fiscais Eletrônicas.
    /// </summary>
    
    
    
    public sealed class RetornoConsultaRecibo 
    {
        #region Constructors

        public RetornoConsultaRecibo(string xmlRetornoConsultaRecibo)
        {
            this.Xml = xmlRetornoConsultaRecibo;

            using ( StringReader reader = new StringReader(xmlRetornoConsultaRecibo) )
            {
                XPathDocument xdoc = new XPathDocument(reader);
                XPathNavigator navigator = xdoc.CreateNavigator();

                XmlNamespaceManager ns = new XmlNamespaceManager(navigator.NameTable);
                ns.AddNamespace("nfe", Constants.NamespacePortalFiscalNFe);

                XPathNavigator rootNode = navigator.SelectSingleNode("/nfe:retConsReciNFe", ns);

                // versão do leiaute de retorno
                this.VersaoLeiaute = rootNode.SelectSingleNode("@versao", ns).Value;

                // tpAmb
                this.Ambiente = (TipoAmbiente)rootNode.SelectSingleNode("nfe:tpAmb", ns).ValueAsInt;

                // verAplic
                this.VersaoAplicacao = rootNode.SelectSingleNode("nfe:verAplic", ns).Value;

                // nRec
                this.NumeroRecibo = rootNode.SelectSingleNode("nfe:nRec", ns).ValueAsLong;

                // cStat
                this.Status = rootNode.SelectSingleNode("nfe:cStat", ns).Value;

                // xMotivo
                this.Motivo = rootNode.SelectSingleNode("nfe:xMotivo", ns).Value;

                // cUF
                this.UFIBGE = (UfIBGE)rootNode.SelectSingleNode("nfe:cUF", ns).ValueAsInt;

                this.Protocolos = new ProtocoloStatusProcessamentoCollection();

                // protNFe
                XPathNodeIterator protNFeIterator = rootNode.Select("nfe:protNFe", ns);
                if ( protNFeIterator != null )
                {
                    foreach ( XPathNavigator nav in protNFeIterator )
                    {
                        ProtocoloStatusProcessamento protocolo = new ProtocoloStatusProcessamento(nav, ns);
                        this.Protocolos.Add(protocolo);
                    }
                }
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Retorna ou define o xml no qual a instância representa.
        /// </summary>
        private string Xml { get; set; }

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
        /// Retorna o número do recibo consultado.
        /// </summary>
        public long NumeroRecibo { get; private set; }
        /// <summary>
        /// Retorna a lista de Protocolos referente as Notas Fiscais Eletrônicas contidas no lote.
        /// </summary>
        public ProtocoloStatusProcessamentoCollection Protocolos { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Salva o recibo de entrega do Lote de Notas Fiscais Eletrônicas em um arquivo xml.
        /// </summary>
        /// <remarks>O arquivo terá o seguinte nome: [NúmeroRecibo]-rec.xml.</remarks>
        /// <param name="caminho">Caminho onde o arquivo será salvo.</param>
        public void SalvarXml(string caminho)
        {
            //string path = Path.Combine(caminho, this.NumeroRecibo.ToString() + "-rec.xml");

            // TODO: Terminar
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
