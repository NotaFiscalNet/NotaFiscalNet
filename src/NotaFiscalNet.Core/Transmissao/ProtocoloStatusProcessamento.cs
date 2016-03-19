using System;
using System.Xml;
using System.Xml.XPath;

namespace NotaFiscalNet.Core.Transmissao
{
    public sealed class ProtocoloStatusProcessamento
    {
        internal ProtocoloStatusProcessamento(XPathNavigator protNFeNode, XmlNamespaceManager ns)
        {
            XPathNavigator node;

            // infProt
            XPathNavigator infProtNode = protNFeNode.SelectSingleNode("nfe:infProt", ns);

            // tpAmb
            this.Ambiente = (TipoAmbiente)infProtNode.SelectSingleNode("nfe:tpAmb", ns).ValueAsInt;

            // verAplic
            this.VersaoAplicacao = infProtNode.SelectSingleNode("nfe:verAplic", ns).Value;

            // chNFe
            this.ChaveAcessoNFe = infProtNode.SelectSingleNode("nfe:chNFe", ns).Value;

            // dhRecbto
            this.RecebidoEm = infProtNode.SelectSingleNode("nfe:dhRecbto", ns).ValueAsDateTime;

            // nProt
            node = infProtNode.SelectSingleNode("nfe:nProt", ns);
            if (node != null)
                this.NumeroProtocolo = node.Value;

            // digVal
            node = infProtNode.SelectSingleNode("nfe:digVal", ns);
            if (node != null)
                this.DigestValueNFe = node.Value;

            // cStat
            this.Status = infProtNode.SelectSingleNode("nfe:cStat", ns).Value;

            // xMotivo
            this.Motivo = infProtNode.SelectSingleNode("nfe:xMotivo", ns).Value;
        }

        /// <summary>
        /// Retorna o valor indicando o tipo de ambiente que o retorno se refere.
        /// </summary>
        public TipoAmbiente Ambiente { get; private set; }

        /// <summary>
        /// Retorna a versão da aplicação que processa a NF-e.
        /// </summary>
        public string VersaoAplicacao { get; private set; }

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
        public string NumeroProtocolo { get; private set; }

        /// <summary>
        /// Retorna o DigestValue da NF-e processada. Utilizado para conferir a integridade da NF-e original.
        /// </summary>
        public string DigestValueNFe { get; private set; }

        /// <summary>
        /// Retorna o código do status de envio do lote.
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// Retorna a descrição referente ao código do status de envio do lote.
        /// </summary>
        public string Motivo { get; private set; }
    }
}