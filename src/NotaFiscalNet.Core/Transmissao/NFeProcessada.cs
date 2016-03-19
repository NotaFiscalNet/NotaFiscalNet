using System;
using System.Xml.Linq;

namespace NotaFiscalNet.Core.Transmissao
{
    /// <summary>
    /// Representa uma NF-e/NFC-e processada (procNFe).
    /// </summary>
    public class NFeProcessada
    {
        private static readonly XNamespace ns = Constants.XNamespacePortalFiscalNFe;

        /// <summary>
        /// Inicializa a classe com base no xml da NF-e/NFC-e processada (procNFe).
        /// </summary>
        /// <param name="procNFeDocument">Documento xml.</param>
        public NFeProcessada(XDocument procNFeDocument)
        {
            if (procNFeDocument == null)
                throw new ArgumentNullException(nameof(procNFeDocument));

            LoadProcNFe(procNFeDocument.Root);
        }

        private void LoadProcNFe(XElement nfeProcEl)
        {
            if (nfeProcEl.Name != (ns + "nfeProc"))
                throw new ApplicationException("O XML informado não é de uma NF-e/NFC-e processada.");

            var nfeEl = nfeProcEl.Element(ns + "NFe");
            var protNFeEl = nfeProcEl.Element(ns + "protNFe");
            
            NFe = NFe.Parse(nfeEl);
            Protocolo = new ProtocoloNFe(protNFeEl);
        }

        /// <summary>
        /// Retorna a referência para objeto representando a NF-e/NFC-e.
        /// </summary>
        public NFe NFe { get; private set; }

        /// <summary>
        /// Retorna a referência para o objeto representando o protocolo de processamento da NF-e.
        /// </summary>
        public ProtocoloNFe Protocolo { get; private set; }

        /// <summary>
        /// Cria uma nova instância da classe NFeProcessada com base no xml (proc) da nota.
        /// </summary>
        /// <param name="text">Xml da nota processada.</param>
        /// <returns></returns>
        public static NFeProcessada Parse(string text)
        {
            var xdoc = XDocument.Parse(text);
            return new NFeProcessada(xdoc);
        }
    }
}
