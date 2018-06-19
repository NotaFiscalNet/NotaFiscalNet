using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NotaFiscalNet.Core.Tests.Comum
{
    public class CarregadorXml
    {
        private readonly string _arquivoXml;

        public CarregadorXml(string arquivoXml)
        {
            _arquivoXml = arquivoXml;
        }

        public string Carregar()
        {
            var pathXml = ObtemPathArquivoXml();
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(pathXml);

            return xmlDocument.OuterXml;
        }

        private string ObtemPathArquivoXml()
        {
            return Path.Combine(ObtemPathArquivosXml(), _arquivoXml);
        }

        private string ObtemPathArquivosXml()
        {
	        return Path.GetDirectoryName(
		               Uri.UnescapeDataString(
			               new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + $"{Path.DirectorySeparatorChar}Xmls";
        }
    }
}
