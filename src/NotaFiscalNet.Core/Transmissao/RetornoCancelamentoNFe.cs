using NotaFiscalNet.Core.Utils;
using System;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o tipo 'TRetCancNFe' do layout NFe.
    /// </summary>
    public class RetornoCancelamentoNFe
    {
        public string Versao { get; private set; }

        public string Id { get; private set; }

        public TipoAmbiente Ambiente { get; private set; }

        public string VersaoAplicacao { get; private set; }

        public string CodigoStatus { get; private set; }

        public string Motivo { get; private set; }

        public UfIBGE UF { get; private set; }

        public DateTime DataRecebimento { get; private set; }

        public string ChaveAcesso { get; private set; }

        public string NumeroProtocolo { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="retCancNFe">Representa o tipo complexo 'TRetCancNFe'.</param>
        internal RetornoCancelamentoNFe(XElement retCancNFeEl)
        {
            if (retCancNFeEl == null) throw new ArgumentException("retCancNFeEl está vazio.");

            var ns = Constants.XNamespacePortalFiscalNFe;

            retCancNFeEl.NFAttributeAsString("versao", value => Versao = value);

            var infCancEl = retCancNFeEl.Element(ns + "infCanc");
            infCancEl.NFAttributeAsString("Id", value => Id = value);
            infCancEl.NFElementAsEnum<TipoAmbiente>("tpAmb", value => Ambiente = value);
            infCancEl.NFElementAsString("verAplic", value => VersaoAplicacao = value);
            infCancEl.NFElementAsString("cStat", value => CodigoStatus = value);
            infCancEl.NFElementAsString("xMotivo", value => Motivo = value);
            infCancEl.NFElementAsEnum<UfIBGE>("cUF", value => UF = value);
            infCancEl.NFElementAsString("chNFe", value => ChaveAcesso = value);
            infCancEl.NFElementAsDateTime("dhRecbto", value => DataRecebimento = value);
            infCancEl.NFElementAsString("nProt", value => NumeroProtocolo = value);
        }
    }
}