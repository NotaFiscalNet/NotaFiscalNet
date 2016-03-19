using System.Xml.Linq;
using NotaFiscalNet.Core.Evento;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa o tipo complexto 'TProcEvento'.
    /// </summary>
    public class ProtocoloEventoNFe
    {

        public string Versao { get; private set; }

        public EventoNFe Evento { get; private set; }

        public RetEvento RetEvento { get; private set; }

        internal ProtocoloEventoNFe(XElement procEventoNFeEl)
        {
            var eventoEl = procEventoNFeEl.Element(Constants.XNamespacePortalFiscalNFe + "evento");
            Evento = new EventoNFeFactory().Create(eventoEl);

            var retEventoEl = procEventoNFeEl.Element(Constants.XNamespacePortalFiscalNFe + "retEvento");
            if (retEventoEl != null)
                RetEvento = new RetEvento(retEventoEl);
        }
    }
}
