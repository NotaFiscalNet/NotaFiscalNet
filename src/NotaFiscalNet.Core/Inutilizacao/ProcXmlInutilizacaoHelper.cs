using System.IO;
using System.Text;
using System.Xml;

namespace NotaFiscalNet.Core.Inutilizacao
{
    public static class ProcXmlInutilizacaoHelper
    {
        /// <summary>
        /// Cria o xml representando o pedido de inutilização de numeração de NFC-e processado.
        /// </summary>
        /// <param name="xmlEnvioInutilizacao">Xml enviado para a Sefaz.</param>
        /// <param name="xmlRetornoEnvioInutilizacao">Xml devolvido pela Sefaz.</param>
        /// <returns></returns>
        public static string ConstuirProcXmlInutilizacao(string xmlEnvioInutilizacao, string xmlRetornoEnvioInutilizacao)
        {
            string procXml;
            using (var ms = new MemoryStream())
            {
                var settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), OmitXmlDeclaration = true };
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    writer.WriteStartElement("ProcInutNFe", Constants.NamespacePortalFiscalNFe);
                    writer.WriteAttributeString("versao", Constants.VersaoLeiauteInutilizacaoNFCe);

                    writer.WriteRaw(xmlEnvioInutilizacao);
                    writer.WriteRaw(xmlRetornoEnvioInutilizacao);

                    writer.WriteEndElement(); // ProcInutNFe
                }

                procXml = Encoding.UTF8.GetString(ms.ToArray());
            }

            return procXml;
        }
    }
}