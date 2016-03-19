using NotaFiscalNet.Core.Utils;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o Pedido de Cancelamento de NF-e.
    /// </summary>

    public sealed class PedidoCancelamentoNFe
    {
        private TipoAmbiente _ambiente = TipoAmbiente.Producao;
        private string _chaveAcesso = string.Empty;
        private string _numeroProtocoloStatusNFe = string.Empty;
        private string _justificativa = string.Empty;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PedidoCancelamentoNFe"/>.
        /// </summary>
        public PedidoCancelamentoNFe()
        {
        }

        /// <summary>
        /// Retorna o valor indicando se a classe está em modo Somente-Leitura.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Retorna o xml carregado de um arquivo.
        /// </summary>
        private string Xml { get; set; }

        /// <summary>
        /// Retorna ou define o valor da Chave de Acesso da NF-e (44 posições).
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Ocorre caso o valor informado seja null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Ocorre caso o valor informado para a Chave de Acesso não seja válido.
        /// </exception>
        public string ChaveAcesso
        {
            get { return _chaveAcesso; }
            set
            {
                CheckReadOnly();
                if (value == null)
                    throw new ArgumentNullException("ChaveAcesso", "O campo ChaveAcesso não pode conter null.");

                if (!Regex.IsMatch(value, @"^[0-9]{44}$"))
                    throw new ArgumentException("A Chave de Acesso informada não é válida. Verifique o valor informado.");

                _chaveAcesso = value;
            }
        }

        /// <summary>
        /// Retorna ou define o valor indicando o ambiente de transmissão. O padrão é Produção.
        /// </summary>
        public TipoAmbiente Ambiente
        {
            get { return _ambiente; }
            set
            {
                CheckReadOnly();
                _ambiente = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Número do Protocolo de Status da NF-e (15 posições).
        /// </summary>
        /// <remarks>
        /// O número do protocolo de status é retornado no processamento do lote de NF-e.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">
        /// Ocorre caso o valor informado seja null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Ocorre caso o valor informado para o Número do Protocolo de Status da NF-e não seja válido.
        /// </exception>
        public string NumeroProtocoloStatusNFe
        {
            get { return _numeroProtocoloStatusNFe; }
            set
            {
                CheckReadOnly();
                if (value == null)
                    throw new ArgumentNullException("NumeroProtocoloStatusNFe", "O campo 'NumeroProtocoloStatusNFe' não pode conter null.");

                if (!Regex.IsMatch(value, @"^[0-9]{15}$"))
                    throw new ArgumentException("O Número do Protocolo de Status da NF-e informada não é válido. Verifique o valor informado.");

                _numeroProtocoloStatusNFe = value;
            }
        }

        /// <summary>
        /// Retorna ou define a justificativa para o cancelamento da NF-e.
        /// </summary>
        /// <remarks>A justificativa deve conter no mínimo 15 e no máximo 255 caracteres.</remarks>
        /// <exception cref="System.ArgumentNullException">
        /// Ocorre caso o valor informado seja null.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Ocorre caso o valor informado esteja fora da faixa permitida de caracteres.
        /// </exception>
        public string Justificativa
        {
            get { return _justificativa; }
            set
            {
                CheckReadOnly();
                if (value == null)
                    throw new ArgumentNullException("Justificativa", "O campo Justificativa não pode ser null.");

                if (value.Length < 15 || value.Length > 255)
                    throw new ArgumentOutOfRangeException("Justificativa", value, "A Justificativa deve conter no mínimo 15 e no máximo 255 caracteres.");

                _justificativa = value;
            }
        }

        /// <summary>
        /// Carrega a classe com base em um arquivo xml de pedido de cancelamento.
        /// </summary>
        /// <remarks>
        /// Após a classe ter sido carregada com base em um arquivo xml, não será permitido alterar
        /// nenhum campo da classe.
        /// </remarks>
        /// <param name="caminho">Caminho do arquivo contendo o pedido de cancelemento.</param>
        public void CarregarXml(string caminho)
        {
            using (XmlReader reader = XmlReader.Create(caminho))
            {
                reader.MoveToContent();

                // lê todo o xml, com exceção da declaração
                string xml = reader.ReadOuterXml();

                // valida o xml de acordo com seu schema

                /// Início da busca pelos campos

                XPathDocument xdoc = new XPathDocument(reader);
                XPathNavigator nav = xdoc.CreateNavigator();

                XmlNamespaceManager ns = new XmlNamespaceManager(nav.NameTable);
                ns.AddNamespace("nfe", Constants.NamespacePortalFiscalNFe);

                XPathNavigator infCancNFeNode = nav.SelectSingleNode("/nfe:cancNFe/nfe:infCanc", ns);

                // tpAmb
                this.Ambiente = (TipoAmbiente)infCancNFeNode.SelectSingleNode("nfe:tpAmb", ns).ValueAsInt;
                // chNFe
                this.ChaveAcesso = infCancNFeNode.SelectSingleNode("nfe:chNFe", ns).Value;
                // nProt
                this.NumeroProtocoloStatusNFe = infCancNFeNode.SelectSingleNode("nfe:nProt", ns).Value;
                // xJust
                this.Justificativa = infCancNFeNode.SelectSingleNode("nfe:xJust", ns).Value;

                this.Xml = xml;
                this.IsReadOnly = true;
            }
        }

        /// <summary>
        /// Limpa todos os campos.
        /// </summary>
        private void ClearAll()
        {
            this.ChaveAcesso = string.Empty;
            this.Ambiente = TipoAmbiente.Producao;
            this.NumeroProtocoloStatusNFe = string.Empty;
            this.Justificativa = string.Empty;
        }

        public string GerarXmlNaoAssinado(bool includeXmlDeclaration)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.OmitXmlDeclaration = !includeXmlDeclaration;

            StringBuilder buffer = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(buffer, settings))
            {
                writer.WriteStartElement("cancNFe", Constants.NamespacePortalFiscalNFe);
                writer.WriteAttributeString("versao", Constants.VersaoLeiauteCancelamento);

                writer.WriteStartElement("infCanc");
                writer.WriteAttributeString("Id", "ID" + this.ChaveAcesso);
                writer.WriteElementString("tpAmb", ((int)this.Ambiente).ToString());
                writer.WriteElementString("xServ", "CANCELAR");
                writer.WriteElementString("chNFe", this.ChaveAcesso);
                writer.WriteElementString("nProt", this.NumeroProtocoloStatusNFe);
                writer.WriteElementString("xJust", SerializationUtil.ToToken(this.Justificativa, 255));

                writer.WriteEndElement(); // fim do elemento 'infCanc'

                writer.WriteEndElement(); // fim do elemento 'cancNFe'
            }
            buffer.Replace("utf-16", "utf-8");
            return buffer.ToString();
        }

        private void CheckReadOnly()
        {
            if (this.IsReadOnly)
                throw new InvalidOperationException("Não é permite realizar alterações no pedido de cancelamento porque a classe foi carregada com base em um arquivo xml assinado digitalmente.");
        }
    }
}