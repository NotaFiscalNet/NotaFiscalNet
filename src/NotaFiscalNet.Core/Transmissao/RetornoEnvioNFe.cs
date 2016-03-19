using System;
using System.Xml;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o Retorno do envio de lote de Notas Fiscais Eletrônicas.
    /// </summary>
    public class RetornoEnvioNFe
    {
        /// <summary>
        /// Inicializa em branco a resposta.
        /// </summary>
        public RetornoEnvioNFe()
        {
        }

        public RetornoEnvioNFe(string xml)
        {
            if (String.IsNullOrEmpty(xml))
                throw new ArgumentNullException(nameof(xml));

            RawXml = xml;
            PreencherResposta(XDocument.Parse(RawXml));
        }

        /// <summary>
        /// Inicializa a classe com o nó representando o xml retornado pela Sefaz.
        /// </summary>
        /// <param name="respostaEnvioNFe"></param>
        public RetornoEnvioNFe(XmlNode respostaEnvioNFe)
        {
            if (respostaEnvioNFe == null)
                throw new ArgumentOutOfRangeException(nameof(respostaEnvioNFe));

            RawXml = respostaEnvioNFe.OuterXml;
            PreencherResposta(XDocument.Parse(RawXml));
        }

        public string RawXml { get; set; }

        public string Versao { get; set; }

        public TipoAmbiente Ambiente { get; set; }

        public string CodigoStatus { get; set; }

        public string Motivo { get; set; }

        public UfIBGE UF { get; set; }

        public DateTime DataRecebimento { get; set; }

        public ReciboLoteNFe InformacoesReciboLote { get; set; }

        public ProtocoloNFe ProtocoloStatusNFe { get; set; }

        private void PreencherResposta(XDocument xDoc)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var retEnviNFeEl = xDoc.Root;

            Versao = retEnviNFeEl.Attribute("versao").Value;
            Ambiente = (TipoAmbiente)int.Parse(retEnviNFeEl.Element(ns + "tpAmb").Value);
            CodigoStatus = retEnviNFeEl.Element(ns + "cStat").Value;
            Motivo = retEnviNFeEl.Element(ns + "xMotivo").Value;
            UF = (UfIBGE)int.Parse(retEnviNFeEl.Element(ns + "cUF").Value);
            DataRecebimento = DateTime.Parse(retEnviNFeEl.Element(ns + "dhRecbto").Value);

            var infRecEl = retEnviNFeEl.Element(ns + "infRec");
            if (infRecEl != null)
            {
                InformacoesReciboLote = new ReciboLoteNFe
                {
                    NumeroRecibo = infRecEl.Element(ns + "nRec").Value,
                    TempoMedio = int.Parse(infRecEl.Element(ns + "tMed").Value)
                };
            }

            var protNFeEl = retEnviNFeEl.Element(ns + "protNFe");
            if (protNFeEl != null)
                ProtocoloStatusNFe = new ProtocoloNFe(protNFeEl);
        }
    }
}