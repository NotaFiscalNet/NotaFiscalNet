using NotaFiscalNet.Core.Utils;
using System;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Evento
{
    /// <summary>
    /// Representa o detalhamento do evento
    /// </summary>
    public class RetornoEventoCancelamento
    {
        public string Versao { get; private set; }

        public string Id { get; private set; }

        public TipoAmbiente Ambiente { get; private set; }

        public string VersaoAplicativo { get; private set; }

        public OrgaoIBGE Orgao { get; private set; }

        public string CodigoStatus { get; private set; }

        public string Motivo { get; private set; }

        public string ChaveAcessoNFe { get; private set; }

        public TipoEventoNFe? Tipo { get; private set; }

        public string Descricao { get; private set; }

        public int? NumeroSequencial { get; private set; }

        public string CnpjDestinatario { get; private set; }

        public string CpfDestinatario { get; private set; }

        public string EmailDestinatario { get; private set; }

        public DateTime DataRegistroEvento { get; private set; }

        public string NumeroProtocolo { get; private set; }

        internal RetornoEventoCancelamento(XElement retEventoEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;

            Versao = retEventoEl.NFAttributeAsString("versao");

            var infEventoEl = retEventoEl.Element(ns + "infEvento");
            infEventoEl.NFAttributeAsString("Id", value => Id = value);
            infEventoEl.NFElementAsEnum<TipoAmbiente>("tpAmb", value => Ambiente = value);
            infEventoEl.NFElementAsString("verAplic", value => VersaoAplicativo = value);
            infEventoEl.NFElementAsEnum<OrgaoIBGE>("cOrgao", value => Orgao = value);
            infEventoEl.NFElementAsString("cStat", value => CodigoStatus = value);
            infEventoEl.NFElementAsString("xMotivo", value => Motivo = value);
            infEventoEl.NFElementAsString("chNFe", value => ChaveAcessoNFe = value);
            infEventoEl.NFElementAsEnum<TipoEventoNFe>("tpEvento", value => Tipo = value);
            infEventoEl.NFElementAsString("xEvento", value => Descricao = value);
            infEventoEl.NFElementAsInt32("nSeqEvento", value => NumeroSequencial = value);
            infEventoEl.NFElementAsString("CNPJDest", value => CnpjDestinatario = value);
            infEventoEl.NFElementAsString("CPFDest", value => CpfDestinatario = value);
            infEventoEl.NFElementAsString("emailDest", value => EmailDestinatario = value);
            infEventoEl.NFElementAsDateTime("dhRegEvento", value => DataRegistroEvento = value);
            infEventoEl.NFElementAsString("nProt", value => NumeroProtocolo = value);
        }
    }
}