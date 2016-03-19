using NotaFiscalNet.Core.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Evento
{
    /// <summary>
    /// Representa o retorno do Envio de um Lote de Eventos de Cancelamento.
    /// </summary>
    public class RetornoLoteEnvioEventoCancelamento
    {
        public string Versao { get; private set; }

        public string IdLote { get; private set; }

        public TipoAmbiente Ambiente { get; private set; }

        public string VersaoAplicativo { get; private set; }

        public OrgaoIBGE Orgao { get; private set; }

        public string CodigoStatus { get; private set; }

        public string Motivo { get; private set; }

        public IEnumerable<RetornoEventoCancelamento> Eventos { get; private set; }

        /// <summary>
        /// Retorna o xml na íntegra.
        /// </summary>
        public string RawXml { get; private set; }

        public RetornoLoteEnvioEventoCancelamento(string xmlRetEnvEvento)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var xDoc = XDocument.Parse(xmlRetEnvEvento);

            var retEnvEventoEl = xDoc.Root;

            retEnvEventoEl.NFAttributeAsString("versao", value => Versao = value);
            retEnvEventoEl.NFElementAsString("idLote", value => IdLote = value);
            retEnvEventoEl.NFElementAsEnum<TipoAmbiente>("tpAmb", value => Ambiente = value);
            retEnvEventoEl.NFElementAsString("verAplic", value => VersaoAplicativo = value);
            retEnvEventoEl.NFElementAsEnum<OrgaoIBGE>("tpAmb", value => Orgao = value);
            retEnvEventoEl.NFElementAsString("cStat", value => CodigoStatus = value);
            retEnvEventoEl.NFElementAsString("xMotivo", value => Motivo = value);

            Eventos = retEnvEventoEl
                .Elements(ns + "retEvento")
                .Select(retEventoEl => new RetornoEventoCancelamento(retEventoEl))
                .ToArray();

            RawXml = xmlRetEnvEvento;
        }
    }
}