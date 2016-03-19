using System;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Transmissao
{
    public class ProtocoloNFe
    {
        public ProtocoloNFe(XElement protNFeEl)
        {
            if (protNFeEl == null) throw new ArgumentNullException(nameof(protNFeEl));

            var ns = Constants.XNamespacePortalFiscalNFe;

            var infProtEl = protNFeEl.Element(ns + "infProt");

            if (infProtEl.Attribute("Id") != null)
                Id = infProtEl.Attribute("Id").Value;

            Versao = protNFeEl.Attribute("versao").Value;
            Ambiente = (TipoAmbiente)int.Parse(infProtEl.Element(ns + "tpAmb").Value);
            VersaoAplicativo = infProtEl.Element(ns + "verAplic").Value;
            ChaveAcessoNFCe = infProtEl.Element(ns + "chNFe").Value;
            DataRecebimento = DateTime.Parse(infProtEl.Element(ns + "dhRecbto").Value);

            if (infProtEl.Element(ns + "nProt") != null)
                Numero = infProtEl.Element(ns + "nProt").Value;

            if (infProtEl.Element(ns + "digVal") != null)
                DigitoValidador = infProtEl.Element(ns + "digVal").Value;

            CodigoStatus = infProtEl.Element(ns + "cStat").Value;
            Motivo = infProtEl.Element(ns + "xMotivo").Value;
        }

        public string Id { get; set; }

        public string Versao { get; set; }

        public TipoAmbiente Ambiente { get; set; }

        public string VersaoAplicativo { get; set; }

        public string ChaveAcessoNFCe { get; set; }

        public DateTime DataRecebimento { get; set; }

        /// <summary>
        /// Retorna ou define o Número do Protocolo de Status.
        /// </summary>
        public string Numero { get; set; }

        public string DigitoValidador { get; set; }

        public string CodigoStatus { get; set; }

        public string Motivo { get; set; }
    }
}