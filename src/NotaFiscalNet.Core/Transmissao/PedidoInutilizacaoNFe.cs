using NotaFiscalNet.Core.Utils;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    public sealed class PedidoInutilizacaoNFe
    {
        private string _id = null;
        private TipoAmbiente _ambiente = TipoAmbiente.Producao;
        private UfIBGE _ufIBGE = UfIBGE.NaoEspecificado;
        private int _ano = 0;
        private string _cnpj = string.Empty;
        private string _mod = "55";
        private int _serie = 0;
        private int _numeracaoInicialNFe = 0;
        private int _numeracaoFinalNFe = 0;
        private string _justificativa = string.Empty;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PedidoInutilizacaoNFe"/>.
        /// </summary>
        public PedidoInutilizacaoNFe()
        {
        }

        /// <summary>
        /// Retorna ou define o xml carregado.
        /// </summary>
        private string Xml { get; set; }

        /// <summary>
        /// Retorna a chave identificadora usada na assinatura digital xml.
        /// </summary>
        public string Id
        {
            get
            {
                if (IsReadOnly)
                    return _id; // pega do campo _id pq foi carregado de um arquivo xml.

                ValidateFields();
                return MountId();
            }
            private set
            {
                // o valor é difine apenas quando é carregado de um arquivo xml.
                _id = value;
            }
        }

        /// <summary>
        /// Retorna ou define o valor indicando o tipo de ambiente para onde será enviado o pedido.
        /// </summary>
        public TipoAmbiente Ambiente
        {
            get { return _ambiente; }
            set
            {
                CheckReadOnly("Ambiente");
                _ambiente = value;
            }
        }

        /// <summary>
        /// Retorna o nome interno do serviço.
        /// </summary>
        public string Servico { get { return "INUTILIZAR"; } }

        /// <summary>
        /// Retorna ou define o código da UF IBGE do emitente.
        /// </summary>
        public UfIBGE UFIBGE
        {
            get { return _ufIBGE; }
            set
            {
                CheckReadOnly("UFIBGE");

                if (!Enum.IsDefined(typeof(UfIBGE), value))
                    throw new ArgumentException("O valor informado não é válido. Informe uma UF válida.");

                if (value == UfIBGE.NaoEspecificado)
                    throw new ArgumentException("O valor informado não é válido. Informe uma UF válida.");

                _ufIBGE = value;
            }
        }

        /// <summary>
        /// Retorna ou define o ano de inutilização da numeração. Informar apenas o ano com 2 dígitos.
        /// </summary>
        public int Ano
        {
            get { return _ano; }
            set
            {
                CheckReadOnly("Ano");
                if (value < 0 || value > 99)
                    throw new ArgumentOutOfRangeException("Ano", value, "Valor deve estar entre 0 e 99.");
                _ano = value;
            }
        }

        /// <summary>
        /// Retorna ou define o número do CNPJ do emitente. Informar apenas números.
        /// </summary>
        public string CNPJ
        {
            get { return _cnpj; }
            set
            {
                CheckReadOnly("CNPJ");
                if (!Regex.IsMatch(value, "^[0-9]{14}$"))
                    throw new ArgumentException("O CNPJ do emitente informado não é válido. O CNPJ deve conter 14 números, incluindo zeros não significativos.");
                _cnpj = value;
            }
        }

        /// <summary>
        /// Retorna o Modelo do Documento Fiscal que será inutilizado. Retorna sempre 55 (NF-e).
        /// </summary>
        public string ModeloDocumentoFiscal { get { return _mod; } private set { _mod = value; } }

        /// <summary>
        /// Retorna ou define a Série da NF-e.
        /// </summary>
        public int Serie
        {
            get { return _serie; }
            set
            {
                CheckReadOnly("Serie");
                if (value < 0 || value > 999)
                    throw new ArgumentOutOfRangeException("Serie", value, "A Série deve estar entre 0 e 999.");
                _serie = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Numeração Inicial da NF-e.
        /// </summary>
        public int NumeracaoInicialNF
        {
            get { return _numeracaoInicialNFe; }
            set
            {
                CheckReadOnly("NumeracaoInicialNF");
                if (value < 1 || value > 999999999)
                    throw new ArgumentOutOfRangeException("NumeracaoInicialNF", value, "A Numeração Inicial da NF deve estar entre 1 e 999999999.");
                _numeracaoInicialNFe = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Numeração Final da NF-e.
        /// </summary>
        public int NumeracaoFinalNF
        {
            get { return _numeracaoFinalNFe; }
            set
            {
                CheckReadOnly("NumeracaoFinalNF");
                if (value < 1 || value > 999999999)
                    throw new ArgumentOutOfRangeException("NumeracaoFinalNF", value, "A Numeração Final da NF deve estar entre 1 e 999999999.");

                _numeracaoFinalNFe = value;
            }
        }

        /// <summary>
        /// Retorna ou define a Justificativa para a inutilização.
        /// </summary>
        public string Justificativa
        {
            get { return _justificativa; }
            set
            {
                CheckReadOnly("Justificativa");

                if (value == null)
                    throw new ArgumentNullException("O campo Justificativa não pode ser null.");

                if (value.Length < 15 || value.Length > 255)
                    throw new ArgumentOutOfRangeException("Justificativa", value, "O Texto da justificativa deve conter no mínimo 15 e no máximo 255 caracteres.");

                _justificativa = value;
            }
        }

        /// <summary>
        /// Retorna o valor indicando se a classe está ou não em modo Somente-Leitura.
        /// </summary>
        /// <remarks>
        /// A classe entra em modo Somente-Leitura quando ela é carregada com base em um arquivo xml
        /// assinado digitalmente.
        /// </remarks>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Carrega a classe com base em um arquivo xml de pedido de inutilização.
        /// </summary>
        /// <remarks>
        /// Após a classe ter sido carregada com base em um arquivo xml, não será permitido alterar
        /// nenhum campo da classe.
        /// </remarks>
        /// <param name="caminho">Caminho do arquivo contendo o pedido de inutilização.</param>
        public void CarregarXml(string caminho)
        {
            using (XmlReader reader = XmlReader.Create(caminho))
            {
                reader.MoveToContent();

                // lê todo o xml, com exceção da declaração
                string xml = reader.ReadOuterXml();

                // valida o xml de acordo com seu schema

                // Início da busca pelos campos
                XPathDocument xdoc = new XPathDocument(reader);
                XPathNavigator nav = xdoc.CreateNavigator();

                XmlNamespaceManager ns = new XmlNamespaceManager(nav.NameTable);
                ns.AddNamespace("nfe", Constants.NamespacePortalFiscalNFe);

                XPathNavigator infInutNode = nav.SelectSingleNode("/nfe:inutNFe/nfe:infInut", ns);

                // @Id
                this.Id = infInutNode.SelectSingleNode("@Id", ns).Value;
                // tpAmb
                this.Ambiente = (TipoAmbiente)infInutNode.SelectSingleNode("nfe:tpAmb", ns).ValueAsInt;
                // cUF
                this.UFIBGE = (UfIBGE)infInutNode.SelectSingleNode("nfe:cUF", ns).ValueAsInt;
                // ano
                this.Ano = infInutNode.SelectSingleNode("nfe:ano", ns).ValueAsInt;
                // CNPJ
                this.CNPJ = infInutNode.SelectSingleNode("nfe:CNPJ", ns).Value;
                // mod
                this.ModeloDocumentoFiscal = infInutNode.SelectSingleNode("nfe:mod", ns).Value;
                // serie
                this.Serie = infInutNode.SelectSingleNode("nfe:serie", ns).ValueAsInt;
                // nNFIni
                this.NumeracaoInicialNF = infInutNode.SelectSingleNode("nfe:nNFIni", ns).ValueAsInt;
                // nNFFin
                this.NumeracaoFinalNF = infInutNode.SelectSingleNode("nfe:nNFFin", ns).ValueAsInt;
                // xJust
                this.Justificativa = infInutNode.SelectSingleNode("nfe:xJust", ns).Value;

                this.Xml = xml;
                this.IsReadOnly = true;
            }
        }

        /// <summary>
        /// Gera o xml de pedido de inutilização.
        /// </summary>
        /// <param name="incluirDeclaracaoXml">
        /// Valor indicando se deve ou não incluir no topo do xml a declaração xml padrão.
        /// </param>
        /// <returns>Retorna uma string xml contendo o pedido de inutilização.</returns>
        public string GerarXmlNaoAssinado(bool incluirDeclaracaoXml)
        {
            // Checa se os campos obrigatórios foram preenchidos.
            ValidateFields();

            StringBuilder output = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.OmitXmlDeclaration = !incluirDeclaracaoXml;

            using (XmlWriter writer = XmlWriter.Create(output, settings))
            {
                writer.WriteStartElement("inutNFe", Constants.NamespacePortalFiscalNFe);
                writer.WriteAttributeString("versao", Constants.VersaoLeiauteInutilizacao);

                writer.WriteStartElement("infInut");
                writer.WriteAttributeString("Id", MountId());

                writer.WriteElementString("tpAmb", ((int)this.Ambiente).ToString());
                writer.WriteElementString("xServ", this.Servico);
                writer.WriteElementString("cUF", ((int)this.UFIBGE).ToString());
                writer.WriteElementString("ano", this.Ano.ToString("00"));
                writer.WriteElementString("CNPJ", this.CNPJ);
                writer.WriteElementString("mod", this.ModeloDocumentoFiscal);
                writer.WriteElementString("serie", this.Serie.ToString());
                writer.WriteElementString("nNFIni", this.NumeracaoInicialNF.ToString());
                writer.WriteElementString("nNFFin", this.NumeracaoFinalNF.ToString());
                writer.WriteElementString("xJust", SerializationUtil.ToToken(this.Justificativa, 255));

                writer.WriteEndElement(); // fim do elemento 'intInut'
                writer.WriteEndElement(); // fim do elemento 'inutNFe'
            }

            if (incluirDeclaracaoXml)
                output.Replace("utf-16", "utf-8");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(output.ToString());

            // valida o xml baseado no schema

            return xmlDoc.OuterXml;
        }

        private string MountId()
        {
            // "ID" + cUF + CNPJ + mod + serie + nNFIni + nNFFin
            return string.Concat("ID", ((int)this.UFIBGE).ToString(), this.Ano.ToString("00"), this.CNPJ, this.ModeloDocumentoFiscal, this.Serie.ToString("000"), this.NumeracaoInicialNF.ToString("000000000"), this.NumeracaoFinalNF.ToString("000000000"));
        }

        /// <summary>
        /// Checa se é permitido realizar alterações na classe. Se não for, lança uma exception
        /// informando que não é permitido.
        /// </summary>
        /// <param name="field">Nome do campo que está sendo alterado.</param>
        private void CheckReadOnly(string field)
        {
            if (IsReadOnly)
                throw new InvalidOperationException(
                    $"O campo '{field}' não pode ser alterado porque a classe está em modo Somente-Leitura.");
        }

        private void ValidateFields()
        {
            if (this.UFIBGE == UfIBGE.NaoEspecificado)
                throw new ApplicationException("O campo UFIBGE não foi informado.");

            if (this.CNPJ == string.Empty)
                throw new ApplicationException("O campo CNPJ não foi informado.");

            if (this.NumeracaoInicialNF == 0)
                throw new ApplicationException("O campo NumeracaoInicialNF não foi informado.");

            if (this.NumeracaoFinalNF == 0)
                throw new ApplicationException("O campo NumeracaoFinalNF não foi informado.");

            if (this.Justificativa == string.Empty)
                throw new ApplicationException("O campo Justificativa não foi informado.");
        }
    }
}