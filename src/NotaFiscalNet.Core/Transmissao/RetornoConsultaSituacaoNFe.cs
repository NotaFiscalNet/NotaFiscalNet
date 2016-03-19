using System;
using System.Xml;
using System.Xml.Linq;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core.Transmissao
{
    public class RetornoConsultaSituacaoNFe
    {
        public string RawXml { get; private set; }

        public string Versao { get; private set; }

        public TipoAmbiente Ambiente { get; private set; }

        public string CodigoStatus { get; private set; }

        public string Motivo { get; private set; }

        public UfIBGE UF { get; private set; }

        public DateTime DataRecebimento { get; private set; }

        public string ChaveAcesso { get; private set; }

        public ProtocoloNFe ProtocoloStatusNFe { get; private set; }

        public RetornoCancelamentoNFe ProtocoloCancelamentoNFe { get; private set; }

        public ProtocoloEventoNFe ProtocoloEventoNFe { get; private set; }


        private RetornoConsultaSituacaoNFe(string xmlRetConsSitNFe)
        {
            RawXml = xmlRetConsSitNFe;
            PreencherResposta(XDocument.Parse(xmlRetConsSitNFe));
        }

        private void PreencherResposta(XDocument xDoc)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var retConsSitNFeEl = xDoc.Root;

            if (retConsSitNFeEl == null) throw new ArgumentException("xDoc está vazio.");

            if (retConsSitNFeEl.Name != (ns + "retConsSitNFe"))
                throw new Exception("O xml informado na resposta da consulta de situação de NFe não é válido.");

            Versao = retConsSitNFeEl.Attribute("versao").Value;
            retConsSitNFeEl.NFElementAsEnum<TipoAmbiente>("tpAmb", value => Ambiente = value);
            retConsSitNFeEl.NFElementAsString("cStat", value => CodigoStatus = value);
            retConsSitNFeEl.NFElementAsString("xMotivo", value => Motivo = value);
            retConsSitNFeEl.NFElementAsEnum<UfIBGE>("cUF", value => UF = value);
            retConsSitNFeEl.NFElementAsDateTime("dhRecbto", value => DataRecebimento = value);
            retConsSitNFeEl.NFElementAsString("chNFe", value => ChaveAcesso = value);

            var protNFeEl = retConsSitNFeEl.Element(ns + "protNFe");
            if (protNFeEl != null)
                ProtocoloStatusNFe = new ProtocoloNFe(protNFeEl);

            var retCancNFeEl = retConsSitNFeEl.Element(ns + "retCancNFe");
            if (retCancNFeEl != null)
                ProtocoloCancelamentoNFe = new RetornoCancelamentoNFe(retCancNFeEl);

            var procEventoNFeEl = retConsSitNFeEl.Element(ns + "procEventoNFe");
            if (procEventoNFeEl != null)
                ProtocoloEventoNFe = new ProtocoloEventoNFe(procEventoNFeEl);
        }

        public static RetornoConsultaSituacaoNFe Parse(string xmlRetConsSitNFe)
        {
            return new RetornoConsultaSituacaoNFe(xmlRetConsSitNFe);
        }

        public static RetornoConsultaSituacaoNFe ReadFrom(XmlNode retConsSitNFe)
        {
            if (retConsSitNFe == null) throw new ArgumentNullException("retConsSitNFe");

            var xml = retConsSitNFe.OuterXml;
            
            return Parse(xml);
        }
    }
}
