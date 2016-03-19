using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o retorno da Consulta de Status de Serviço.
    /// </summary>
    
    
    
    public sealed class RetornoConsultaStatusServico 
    {
        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="RetornoConsultaStatusServico"/> com base no xml devolvido pelo serviço de Consulta de Status de Serviço.
        /// </summary>
        /// <param name="xml">String xml devolvido pelo WebService.</param>
        /// <param name="horarioPedido">Data/Hora em que a solicitação foi enviada à Sefaz.</param>
        internal RetornoConsultaStatusServico(string xml, DateTime horarioPedido)
        {
            this.RetornoXml = xml;
            this.HorarioPedido = horarioPedido;

            using ( StringReader reader = new StringReader(xml) )
            {
                XPathDocument xdoc = new XPathDocument(reader);
                XPathNavigator navigator = xdoc.CreateNavigator();

                XmlNamespaceManager ns = new XmlNamespaceManager(navigator.NameTable);
                ns.AddNamespace("nfe", Constants.NamespacePortalFiscalNFe);

                XPathNavigator retConsStatServNode = navigator.SelectSingleNode("/nfe:retConsStatServ", ns);

                // versão do leiaute de retorno
                this.VersaoLeiaute = retConsStatServNode.SelectSingleNode("@versao", ns).Value;

                // tpAmb
                this.Ambiente = (TipoAmbiente)retConsStatServNode.SelectSingleNode("nfe:tpAmb", ns).ValueAsInt;

                // verAplic
                this.VersaoAplicacao = retConsStatServNode.SelectSingleNode("nfe:verAplic", ns).Value;

                // cStat
                this.Status = retConsStatServNode.SelectSingleNode("nfe:cStat", ns).Value;

                // xMotivo
                this.Motivo = retConsStatServNode.SelectSingleNode("nfe:xMotivo", ns).Value;

                // cUF
                this.UFIBGE = (UfIBGE)retConsStatServNode.SelectSingleNode("nfe:cUF", ns).ValueAsInt;

                // dhRecbto
                this.RecebidoEm = retConsStatServNode.SelectSingleNode("nfe:dhRecbto", ns).ValueAsDateTime;
                
                // tMed
                XPathNavigator node = retConsStatServNode.SelectSingleNode("nfe:tMed", ns);
                if ( node != null )
                    this.TempoMedioResposta = node.ValueAsInt;

                // dhRetorno
                node = retConsStatServNode.SelectSingleNode("nfe:dhRetorno", ns);
                if ( node != null )
                    this.DataRetorno = node.ValueAsDateTime;

                // xObs
                node = retConsStatServNode.SelectSingleNode("nfe:dhRetorno", ns);
                if ( node != null )
                    this.Observacoes = node.Value;
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Retorna o xml devolvido pelo WebService da Sefaz de envio.
        /// </summary>
        public string RetornoXml { get; private set; }

        /// <summary>
        /// Retorna a Data/Hora em que o pedido (chamada no WebService) foi feita<br/>
        /// Também usado para gerar o nome do arquivo.
        /// </summary>
        public DateTime HorarioPedido { get; set; }

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
        /// Retorna a Data e Hora de recibimento do lote pela Sefaz.
        /// </summary>
        public DateTime RecebidoEm { get; private set; }
        /// <summary>
        /// Retorna o tempo médio de resposta (em segundos), referente ao processamento dos lotes.
        /// </summary>
        public int TempoMedioResposta { get; private set; }
        /// <summary>
        /// Retorna a Data e Hora de retorno.
        /// </summary>
        public DateTime DataRetorno { get; private set; }
        /// <summary>
        /// Retorna as observações informadas pela Sefaz de envio.
        /// </summary>
        public string Observacoes { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Salva o retorno da Consulta de Status de Serviço em um arquivo xml.
        /// </summary>
        /// <remarks>O arquivo terá o seguinte nome: [AAAAMMDDTHHMMSS]-sta.xml.</remarks>
        /// <param name="caminho">Caminho onde o arquivo será salvo.</param>
        /// <returns>Retorna o caminho completo do arquivo xml gravado.</returns>
        public string SalvarXml(string caminho)
        {
            string path = Path.Combine(caminho, this.HorarioPedido.ToString("yyyMMddTHHmmss") + "-sta.xml");

            using ( StringReader sr = new StringReader(this.RetornoXml) )
            using ( XmlReader reader = XmlReader.Create(sr) )
            {
                reader.MoveToContent();
                string outerXml = reader.ReadOuterXml(); // pega o elemento raiz, desconsidera a declaração, se houver.

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;

                using ( XmlWriter writer = XmlWriter.Create(path, settings) )
                    writer.WriteRaw(outerXml);
            }

            return path;
        }

        #endregion Methods
    }
}
