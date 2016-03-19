using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o Recibo de Entrega de um Lote de Notas Fiscais Eletrônicas.
    /// </summary>
    
    
    
    public sealed class ReciboEntregaLoteNFe 
    {
        #region Constructors

        internal ReciboEntregaLoteNFe(TipoAmbiente ambiente, string versaoAplicacao, string status, string motivo, UfIBGE ufIBGE)
        {
            this.Ambiente = ambiente;
            this.VersaoAplicacao = versaoAplicacao;
            this.Status = status;
            this.UFIBGE = ufIBGE;
            this.NumeroRecibo = 0L;
            this.RecebidoEm = DateTime.MinValue;
            this.TempoMedioResposta = 0;
        }

        internal ReciboEntregaLoteNFe(TipoAmbiente ambiente, string versaoAplicacao, string status, string motivo, UfIBGE ufIBGE, long numeroRecibo, DateTime recebidoEm, int tempoMedioResposta)
        {
            this.Ambiente = ambiente;
            this.VersaoAplicacao = versaoAplicacao;
            this.Status = status;
            this.UFIBGE = ufIBGE;
            this.NumeroRecibo = numeroRecibo;
            this.RecebidoEm = recebidoEm;
            this.TempoMedioResposta = tempoMedioResposta;
        }

        public ReciboEntregaLoteNFe(string xmlRetornoEnvioLoteNFe)
        {
            using ( StringReader reader = new StringReader(xmlRetornoEnvioLoteNFe) )
            {
                XPathDocument xdoc = new XPathDocument(reader);
                XPathNavigator navigator = xdoc.CreateNavigator();

                XmlNamespaceManager ns = new XmlNamespaceManager(navigator.NameTable);
                ns.AddNamespace("nfe", Constants.NamespacePortalFiscalNFe);

                XPathNavigator rootNode = navigator.SelectSingleNode("/nfe:retEnviNFe", ns);
                XPathNavigator node;

                // versão do leiaute de retorno
                node = rootNode.SelectSingleNode("@versao", ns);
                this.VersaoLeiaute = GetNodeValue(node);

                // tpAmb
                node = rootNode.SelectSingleNode("nfe:tpAmb", ns);
                string tpAmb = GetNodeValue(node);
                this.Ambiente = (TipoAmbiente)int.Parse(tpAmb);

                // verAplic
                node = rootNode.SelectSingleNode("nfe:verAplic", ns);
                this.VersaoAplicacao = GetNodeValue(node);

                // cStat
                node = rootNode.SelectSingleNode("nfe:cStat", ns);
                this.Status = GetNodeValue(node);

                // xMotivo
                node = rootNode.SelectSingleNode("nfe:xMotivo", ns);
                this.Motivo = GetNodeValue(node);

                // cUF
                node = rootNode.SelectSingleNode("nfe:cUF", ns);
                string cUF = GetNodeValue(node);
                this.UFIBGE = (UfIBGE)int.Parse(cUF);

                // dhRecbto
                node = rootNode.SelectSingleNode("nfe:dhRecbto", ns);
                if (node != null)
                    this.RecebidoEm = node.ValueAsDateTime;

                // infRec
                XPathNavigator infRecNode = rootNode.SelectSingleNode("nfe:infRec", ns);
                if ( infRecNode != null )
                {
                    // nRec
                    node = infRecNode.SelectSingleNode("nfe:nRec", ns);
                    if ( node != null )
                        this.NumeroRecibo = node.ValueAsLong;

                    // tMed
                    node = infRecNode.SelectSingleNode("nfe:tMed", ns);
                    if ( node != null )
                        this.TempoMedioResposta = node.ValueAsInt;
                }

            }
        }

        #endregion Constructors

        #region Properties

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
        /// Retorna o número do recibo de entreda do lote.
        /// </summary>
        public long NumeroRecibo { get; private set; }
        /// <summary>
        /// Retorna a Data e Hora de recebimento do lote pela Sefaz.
        /// </summary>
        public DateTime RecebidoEm { get; private set; }
        /// <summary>
        /// Retorna o tempo médio de resposta (em segundos), referente ao processamento dos lotes.
        /// </summary>
        public int TempoMedioResposta { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Salva o recibo de entrega do Lote de Notas Fiscais Eletrônicas em um arquivo xml.
        /// </summary>
        /// <remarks>O arquivo terá o seguinte nome: [NúmeroRecibo]-rec.xml.</remarks>
        /// <param name="caminho">Caminho onde o arquivo será salvo.</param>
        public void SalvarXml(string caminho)
        {
            string path = Path.Combine(caminho, this.NumeroRecibo.ToString() + "-rec.xml");

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;

            using ( XmlWriter writer = XmlWriter.Create(path, settings) )
            {
                writer.WriteStartElement("retEnviNFe");
                writer.WriteAttributeString("versao", this.VersaoLeiaute);

                writer.WriteElementString("tpAmb", ( (int)this.Ambiente ).ToString());
                writer.WriteElementString("verAplic", this.VersaoAplicacao);
                writer.WriteElementString("cStat", this.Status);
                writer.WriteElementString("xMotivo", this.Motivo);
                writer.WriteElementString("cUF", ( (int)this.UFIBGE ).ToString());

                if ( this.NumeroRecibo != 0L )
                {
                    writer.WriteStartElement("infRec");
                    writer.WriteElementString("nRec", this.NumeroRecibo.ToString());
                    writer.WriteElementString("dhRecbto", this.RecebidoEm.ToString("s"));
                    writer.WriteElementString("tMed", this.TempoMedioResposta.ToString());
                    writer.WriteEndElement(); // fim do elemento 'infRec'
                }

                writer.WriteEndElement(); // fim do elemento 'retEnviNFe'
            }
        }

        private string GetNodeValue(XPathNavigator nav)
        {
            if ( nav == null )
                return string.Empty;
            return nav.Value ?? string.Empty;
        }

        #endregion Methods
    }
}
